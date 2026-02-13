using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Payments.Api.Dtos;
using Payments.Api.Services;

public class IndexModel : PageModel
{
    private readonly IPaymentService _service;

    public IndexModel(IPaymentService service) => _service = service;

    [BindProperty]
    public CreatePaymentDto Payment { get; set; } = new();

    public string? Message { get; set; }

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        // Use the secret key defined in your service
        await _service.CreatePaymentAsync(Payment, "my-secret-key-123");

        Message = "Payment successfully submitted!";
        return Page();
    }
}