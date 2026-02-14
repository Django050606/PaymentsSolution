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
        if (!ModelState.IsValid)
        {
            // This will print the exact reason for the 400 error in your Output window
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            foreach (var error in errors)
            {
                Console.WriteLine($"Validation Error: {error}");
            }
            return Page(); // This returns the page so you can see the red error text
        }

        await _service.CreatePaymentAsync(Payment, "my-secret-key-123");
        Message = "Payment successfully submitted!";
        return Page();
    }
}