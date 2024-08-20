using Microsoft.EntityFrameworkCore;
using NewLayeredArchitecture.Infra.Context;
using NewLayeredArchitecture.Infra.Repositories;
using NewLayeredArchitecture.Application.Services;
using NewLayeredArchitecture.Domain.Repositories;
using NewLayeredArchitecture.Application.Notifications;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

builder.Services.AddScoped<OrderAppService>();

builder.Services.AddSingleton<INotificationHandler, NotificationHandler>();

// Adicionar controllers com views
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configurar o pipeline de requisições HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();