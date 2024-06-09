using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MvcUsers.Data;
using MvcUsers.Services;

var builder = WebApplication.CreateBuilder(args);
var connString = builder.Configuration["ConnectionStrings:DefaultConnection"];

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddEntityFrameworkSqlServer().AddDbContext<AppDbContext>( opts => opts.UseSqlServer(connString));

builder.Services.AddScoped<UsuarioService>();

builder.Services.AddSession(opts =>
{
    opts.IdleTimeout = TimeSpan.FromMinutes(30);
    opts.Cookie.HttpOnly = true;
    opts.Cookie.IsEssential = true;
});

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

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
