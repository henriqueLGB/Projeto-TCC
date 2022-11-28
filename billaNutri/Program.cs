using billaNutri.Models.Banco_de_Dados;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<Context>(options =>
    //options.UseSqlServer("Server=DESKTOP-D1N4KG6\\SQLEXPRESS;DataBase=NutriTCC;Uid=sa;pwd=Profissional@123"));
    options.UseSqlServer("workstation id=NutriTCC.mssql.somee.com;packet size=4096;user id=BAUCIUS_SQLLogin_2;pwd=pbrldjkthm;data source=NutriTCC.mssql.somee.com;persist security info=False;initial catalog=NutriTCC"));

//Identity
builder.Services.AddAuthentication("Identity.Login")
    .AddCookie("Identity.Login", config =>
    {
        config.Cookie.Name = "Identity.Login";
        config.LoginPath = "/Index";
        config.AccessDeniedPath = "/Home";
        config.ExpireTimeSpan = TimeSpan.FromHours(1);

    });

// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
