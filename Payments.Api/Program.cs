using Microsoft.EntityFrameworkCore;
using Payments.Api.Data;
using Payments.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Register Services
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddRazorPages();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddControllers();

// Add these lines to enable the API documentation (Swagger)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 2. Enable Swagger (This fixes the 404)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Payments API V1");
        c.RoutePrefix = "swagger"; // This makes it live at /swagger
    });
}

// 3. Auto-create Database
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

app.UseRouting();
app.MapControllers();
app.MapRazorPages();

// No default redirect to Swagger; let Razor Pages handle the site root

app.Run();