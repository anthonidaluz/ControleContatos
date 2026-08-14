using ControleContatos.Data;
using ControleContatos.Repositorio;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// =========================================================================
// CONFIGURAÇÃO DO BANCO DE DADOS (Entity Framework Core)
// =========================================================================
builder.Services.AddDbContext<BancoContext>(options =>
{
    // O nome "DataBase" tem que ser igual ao que está no appsettings.json
    string connectionString = builder.Configuration.GetConnectionString("DataBase");
    options.UseSqlServer(connectionString);

});

builder.Services.AddScoped<IContatoRepositorio, ContatoRepositorio>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();