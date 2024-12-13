using Polly;
using Microsoft.EntityFrameworkCore;
using Payment_Service.Application.Interfaces;
using Payment_Service.Application.Services.Contracts;
using Payment_Service.Application.Services.Implementations;
using Payment_Service.Infrastructure.Data;
using Payment_Service.Infrastructure.Repositories;
using Payment_Service.Infrastructure.UnitOfWork;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

//builder.Services.AddDbContext<PaymentDbContext>(options => options.UseInMemoryDatabase("MyDatabase"));

builder.Services.AddDbContext<PaymentDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var policy = Policy.Handle<SqlException>()
    .WaitAndRetryAsync(30, attempt => TimeSpan.FromSeconds(5), 
        (exception, timeSpan, retryCount, context) =>
        {
            Console.WriteLine($"Retry {retryCount} failed, waiting {timeSpan.TotalSeconds} seconds.");
        });

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<PaymentDbContext>();
    try
    {
        await policy.ExecuteAsync(async () =>
        {
            await dbContext.Database.MigrateAsync();
            Console.WriteLine("Database connection succeeded and migration done.");
        });
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Could not connect to database {ex.Message}");
        throw;
    }
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


