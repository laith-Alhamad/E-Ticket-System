using ETicketing.API.DTOs;
using ETicketing.API.Models;

namespace ETicketing.API.Payments
{
    public interface IPaymentHandler
    {
        string PaymentMethod { get; }
        Task<PaymentResultDto> ProcessPaymentAsync(Order order);
    }
}