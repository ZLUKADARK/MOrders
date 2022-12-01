using Microsoft.EntityFrameworkCore;
using MOrders.BLL.Interfaces;
using MOrders.BLL.Services;
using MOrders.DAL.Data;
using MOrders.DAL.Entities;
using MOrders.DAL.Interfaces;
using MOrders.DAL.Models;
using MOrders.DAL.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

string connection = builder.Configuration.GetConnectionString("MOrdersDBContext");
builder.Services.AddDbContext<MOrdersContext>(option => option.UseSqlServer(connection));

builder.Services.AddTransient<IOrderServices, OrderServices>();
builder.Services.AddTransient<IRepository<Provider>, ProviderRepository>();
builder.Services.AddTransient<IRepository<OrderItem, OrderFilter>, OrderItemRepository>();
builder.Services.AddTransient<IRepository<Order>, OrderRepository>();
builder.Services.AddTransient<IDistinctRepository<DistinctValues>, DistinctValuesRepositoriy> ();

var app = builder.Build();

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
