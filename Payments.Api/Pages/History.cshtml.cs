using Microsoft.AspNetCore.Mvc.RazorPages;
using Payments.Api.Models;
using Payments.Api.Services;

public class HistoryModel : PageModel
{
    private readonly IPaymentService _service;

    public HistoryModel(IPaymentService service)
    {
        _service = service;
    }

    public List<Payment> Payments { get; set; } = new();

    public async Task OnGetAsync()
    {
        // Fetching the first 50 records
        Payments = await _service.GetHistoryAsync(0, 50);
    }
}