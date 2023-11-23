using Microsoft.EntityFrameworkCore;
using SiteDeCadastro.Data;
using SiteDeCadastro.Helper;
using SiteDeCadastro.Repositorio;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddEntityFrameworkSqlServer().AddDbContext<BancoContext>();
builder.Services.AddScoped<IContatoRepositorio, ContatoRepositorio>();
builder.Services.AddScoped<IUserRepositorio, UserRepositorio>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ISessionUser, SessionUser>();
builder.Services.AddScoped<IEmail, Email>();

builder.Services.AddSession(o => //adiciona a sessaos ao projeto
{ 
    o.Cookie.HttpOnly = true; // impede que o cookie seja acessado atráves de JS.
    o.Cookie.IsEssential = true; // Indica que o cookie é necessario para o aplicativo funcionar corretamente.
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();//chama as sessoes dentro do projeto

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
