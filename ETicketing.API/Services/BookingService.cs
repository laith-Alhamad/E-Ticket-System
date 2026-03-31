using ETicketing.API.Data;
using ETicketing.API.DTOs;
using ETicketing.API.Models;
using ETicketing.API.Payments;
using Microsoft.EntityFrameworkCore;

namespace ETicketing.API.Services
{
    public class BookingService:IBookingService
    {
        private readonly AppDbContext _context;
        private readonly IEnumerable<IPaymentHandler> _paymentHandlers;
        private readonly ILedgerService _ledgerService;

        public BookingService(
            AppDbContext context,
            IEnumerable<IPaymentHandler> paymentHandlers,
            ILedgerService ledgerService)
        {
            _context=context;
            _paymentHandlers=paymentHandlers;
            _ledgerService=ledgerService;
        }

        public async Task<CheckoutResponseDto> CheckoutAsync(CheckoutRequestDto request)
        {
            var paymentHandler = _paymentHandlers
                .FirstOrDefault(x => x.PaymentMethod.Equals(request.PaymentMethod,StringComparison.OrdinalIgnoreCase));

            if(paymentHandler==null)
            {
                throw new Exception("Unsupported payment method.");
            }

            await using var dbTransaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var ticket = await _context.Tickets
                    .FirstOrDefaultAsync(x => x.Id==request.TicketId&&x.IsActive);

                if(ticket==null)
                {
                    throw new Exception("Ticket not found or inactive.");
                }

                if(ticket.RemainingQuota<request.Quantity)
                {
                    throw new Exception("Not enough ticket quota available.");
                }

                var totalAmount = ticket.Price*request.Quantity;

                var order = new Order
                {
                    TicketId=ticket.Id,
                    Quantity=request.Quantity,
                    UnitPrice=ticket.Price,
                    TotalAmount=totalAmount,
                    Status="Pending",
                    PaymentMethod=request.PaymentMethod,
                    CreatedAt=DateTime.UtcNow
                };

                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();

                var transaction = new Transaction
                {
                    OrderId=order.Id,
                    PaymentMethod=request.PaymentMethod,
                    Status="Pending",
                    Amount=totalAmount,
                    TransactionReference=Guid.NewGuid().ToString(),
                    CreatedAt=DateTime.UtcNow
                };

                await _context.Transactions.AddAsync(transaction);
                await _context.SaveChangesAsync();

                var paymentResult = await paymentHandler.ProcessPaymentAsync(order);

                transaction.Status=paymentResult.Status;
                transaction.ConfirmedAt=paymentResult.ConfirmedAt;

                if(paymentResult.Status=="Success")
                {
                    ticket.RemainingQuota-=request.Quantity;
                    ticket.UpdatedAt=DateTime.UtcNow;

                    order.Status="Paid";
                    order.UpdatedAt=DateTime.UtcNow;

                    await _context.SaveChangesAsync();

                    await _ledgerService.CreateEntriesAsync(transaction);
                    await _context.SaveChangesAsync();


                }
                else
                {
                    order.Status="Failed";
                    order.UpdatedAt=DateTime.UtcNow;
                    transaction.FailureReason=paymentResult.Message;

                    await _context.SaveChangesAsync();
                }

                await dbTransaction.CommitAsync();

                return new CheckoutResponseDto
                {
                    OrderId=order.Id,
                    TransactionId=transaction.Id,
                    TransactionReference=transaction.TransactionReference,
                    OrderStatus=order.Status,
                    PaymentStatus=transaction.Status,
                    Amount=transaction.Amount,
                    Timestamp=transaction.ConfirmedAt??DateTime.UtcNow,
                    Message=paymentResult.Message
                };
            }
            catch(DbUpdateConcurrencyException)
            {
                await dbTransaction.RollbackAsync();
                throw new Exception("Concurrency conflict occurred. Someone else may have booked the last ticket.");
            }
            catch
            {
                await dbTransaction.RollbackAsync();
                throw;
            }
        }
    }
}