using ETicketing.API.DTOs;
using ETicketing.API.Models;

namespace ETicketing.API.Payments
{
    public class CreditCardHandler:IPaymentHandler
    {
        public string PaymentMethod => "CreditCard";

        public async Task<PaymentResultDto> ProcessPaymentAsync(Order order)
        {
            await Task.Delay(300); // simulation

            return new PaymentResultDto
            {
                Status="Success",
                Message="Credit card payment completed successfully.",
                ConfirmedAt=DateTime.UtcNow
            };
        }
    }
}