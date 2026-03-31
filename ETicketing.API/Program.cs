using ETicketing.API.Data;
using Microsoft.EntityFrameworkCore;
using ETicketing.API.Payments;
using ETicketing.API.Services;

var builder = WebApplication.CreateBuilder(args);

// --- Build ---
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IBookingService,BookingService>();
builder.Services.AddScoped<ILedgerService,LedgerService>();
builder.Services.AddScoped<IPaymentHandler,CreditCardHandler>();
builder.Services.AddScoped<IPaymentHandler,QRHandler>();

//  Add builder for CROS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy => policy.WithOrigins("http://localhost:5173","https://localhost:5173")
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

// --- DbContext ---

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// --- builder ---
var app = builder.Build();

// --- Pipeline (Middlewares) ---
if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//  Active App.UseCors
app.UseCors("AllowReactApp");

app.UseAuthorization();

app.MapControllers();

using(var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

app.Run();