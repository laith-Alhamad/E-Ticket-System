using ETicketing.API.Data;
using ETicketing.API.Models;

namespace ETicketing.API.Services
{
    public class LedgerService:ILedgerService
    {
        private readonly AppDbContext _context;

        public LedgerService(AppDbContext context)
        {
            _context=context;
        }

        public async Task CreateEntriesAsync(Transaction transaction)
        {
            var debitEntry = new LedgerEntry
            {
                TransactionId=transaction.Id,
                EntryType="Debit",
                AccountName="Cash/Payment Gateway",
                Amount=transaction.Amount,
                Description=$"Payment received for transaction {transaction.TransactionReference}",
                CreatedAt=DateTime.UtcNow
            };

            var creditEntry = new LedgerEntry
            {
                TransactionId=transaction.Id,
                EntryType="Credit",
                AccountName="Ticket Sales Revenue",
                Amount=transaction.Amount,
                Description=$"Revenue recognized for transaction {transaction.TransactionReference}",
                CreatedAt=DateTime.UtcNow
            };

            await _context.LedgerEntries.AddRangeAsync(debitEntry,creditEntry);
        }
    }
}