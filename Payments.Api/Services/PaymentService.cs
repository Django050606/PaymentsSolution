using Microsoft.EntityFrameworkCore;
using Payments.Api.Data;
using Payments.Api.Dtos;
using Payments.Api.Models;

namespace Payments.Api.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly AppDbContext _context;
        private const string SECRET_API_KEY = "my-secret-key-123"; // Simulating config

        public PaymentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Payment> CreatePaymentAsync(CreatePaymentDto dto, string apiKey)
        {
            // 1. Business Logic: Map DTO to Entity
            var payment = new Payment
            {
                Id = Guid.NewGuid(),
                WalletNumber = dto.WalletNumber,
                Account = dto.Account,
                Amount = dto.Amount,
                Currency = dto.Currency,
                Email = dto.Email,
                Phone = dto.Phone,
                Comment = dto.Comment,
                CreatedAt = DateTime.UtcNow
            };

            // 2. Security Check (Legitimacy) [cite: 53, 59]
            if (apiKey != SECRET_API_KEY)
            {
                payment.Status = PaymentStatus.Rejected; // Log as rejected
                // We still save it to show in history as "Rejected" [cite: 70]
            }
            else
            {
                payment.Status = PaymentStatus.Created;
            }

            // 3. Save to DB [cite: 66]
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            return payment;
        }

        public async Task<List<Payment>> GetHistoryAsync(int skip = 0, int take = 10)
        {
            // [cite: 35] Sort by Date Descending
            return await _context.Payments
                .OrderByDescending(p => p.CreatedAt)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<object> GetStatsAsync()
        {
            // [cite: 40] Aggregation by day
            var dailyStats = await _context.Payments
                .GroupBy(p => p.CreatedAt.Date)
                .Select(g => new
                {
                    Date = g.Key,
                    Count = g.Count(),
                    TotalAmount = g.Sum(p => p.Amount)
                })
                .ToListAsync();

            // [cite: 38, 39] Overall stats
            return new
            {
                TotalPayments = await _context.Payments.CountAsync(),
                TotalVolume = await _context.Payments.SumAsync(p => p.Amount),
                DailyBreakdown = dailyStats
            };
        }
    }
}