using ETicketing.API.DTOs;

namespace ETicketing.API.Services
{
    public interface IBookingService
    {
        Task<CheckoutResponseDto> CheckoutAsync(CheckoutRequestDto request);
    }
}