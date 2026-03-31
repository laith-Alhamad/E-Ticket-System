using ETicketing.API.Models;

namespace ETicketing.API.Services
{
    public interface ILedgerService
    {
        Task CreateEntriesAsync(Transaction transaction);
    }
}