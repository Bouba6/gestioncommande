using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using gestioncommande.Data.Fixtures;
using Microsoft.AspNetCore.Authentication.Cookies;
using gestioncommande.Services.Impl;
using gestioncommande.Helpers;
using gestioncommande.Controllers;
var builder = WebApplication.CreateBuilder(args);
string connectionString = builder.Configuration.GetConnectionString("PostgresConnextion")!;
builder.Services.AddDbContext<ApplicationDbContext>(options =>
   options.UseNpgsql(connectionString));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<CommandeService>();
builder.Services.AddScoped<ProduitService>();
builder.Services.AddScoped<ClientService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// builder.WebHost.ConfigureKestrel(serverOptions =>
// {
//     serverOptions.Listen(System.Net.IPAddress.Any, 8080); // Port spécifié à 8080
// });


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
{
    option.LoginPath = "/Connexion/Login";
    option.ExpireTimeSpan = TimeSpan.FromMinutes(40);
    option.LogoutPath = "/Connexion/Logout";
});

builder.Services.AddHttpContextAccessor();


var app = builder.Build();

// using (var scope = app.Services.CreateScope())
// {
//     var services = scope.ServiceProvider;
//     var context = services.GetRequiredService<ApplicationDbContext>();
//     Fixtures.Initialize(services, context);
// }

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.Use(async (context, next) =>
{
    await next();

    if (context.Response.StatusCode == 404)
    {
        context.Response.Redirect("/Securite/Error404");
    }
});

// app.Use(async (context, next) =>
// {
//     var panier = context.Session.GetObjectFromJson<Panier>("panier") ?? new Panier();
//     context.Items["Panier"] = panier;
//     await next();
// });


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Connexion}/{action=Login}/{id?}");

app.Run();
