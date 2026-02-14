using Microsoft.AspNetCore.Mvc.RazorPages;
using Payments.Api.Services;

public class StatsModel : PageModel
{
    private readonly IPaymentService _service;

    public StatsModel(IPaymentService service)
    {
        _service = service;
    }

    public dynamic StatsData { get; set; }

    public async Task OnGetAsync()
    {
        StatsData = await _service.GetStatsAsync();
    }
}