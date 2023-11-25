using Microsoft.EntityFrameworkCore;
using OrderManagement.Data;
using OrderManagement.Data.Entities.OrderManagement;
using OrderManagement.Infrastructure.AutoMapping;
using OrderManagement.Repositories.Abstractions;
using OrderManagement.Repositories.Repositories;
using OrderManagement.Services.Abstractions;
using OrderManagement.Services.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IBaseRepository<Order>, BaseRepository<Order>>();
builder.Services.AddTransient<IBaseRepository<OrderItem>, BaseRepository<OrderItem>>();
builder.Services.AddTransient<IBaseRepository<Provider>, BaseRepository<Provider>>();

builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<IOrderItemService, OrderItemService>();
builder.Services.AddTransient<IProviderService, ProviderService>();

builder.Services.AddAutoMapper(typeof(BaseMapper));


var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var servicesProvider = scope.ServiceProvider;

    SeedData.Initialize(servicesProvider);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Order}/{action=Index}/{id?}");

app.Run();
