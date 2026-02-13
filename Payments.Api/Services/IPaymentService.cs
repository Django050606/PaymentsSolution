using Payments.Api.Dtos;
using Payments.Api.Models;

namespace Payments.Api.Services
{
    // [cite: 91]
    public interface IPaymentService
    {
        Task<Payment> CreatePaymentAsync(CreatePaymentDto dto, string apiKey);
        Task<List<Payment>> GetHistoryAsync(int skip = 0, int take = 10);
        Task<object> GetStatsAsync();
    }
}