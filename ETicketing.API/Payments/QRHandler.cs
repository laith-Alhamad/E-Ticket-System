using ETicketing.API.DTOs;
using ETicketing.API.Models;

namespace ETicketing.API.Payments
{
    public class QRHandler:IPaymentHandler
    {
        public string PaymentMethod => "QR";

        public async Task<PaymentResultDto> ProcessPaymentAsync(Order order)
        {
            await Task.Delay(8000); // simulate pending state

            return new PaymentResultDto
            {
                Status="Success",
                Message="QR payment confirmed successfully after delay.",
                ConfirmedAt=DateTime.UtcNow
            };
        }
    }
}