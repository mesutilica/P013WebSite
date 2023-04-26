using P013WebSite.Data;
using Microsoft.AspNetCore.Authentication.Cookies; // Admin panelde oturum iþlemi için

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DatabaseContext>(); // burada veritabaný iþlemleri için kullandýðýmýz contexti tanýtýyoruz
// veritabaný oluþturmak için üst menüden Tools > Nuget Package Manager > Package manger console menüsünden komut yazma ekranýný açýyoruz.
// Bu ekrana add-migration InitialCreate yazýp enter a basýyoruz.
// Migrations klasörü ve initialcreate classý oluþtuktan sonra update-database yazýp enter a basýyoruz
// Done mesajý geldiyse iþlem baþarýlýdýr.

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x =>
{
    x.LoginPath = "/Admin/Login"; // login sisteminin varsayýlan login giriþ adresini kendi adresimizle deðiþtiriyoruz
    x.Cookie.Name = "AdminLogin"; // oturum için oluþacak cookie nin ismini belirledik
}); // Admin panelde authorize attribüte ü ile güvenlik saðlayabilmek için

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
            name: "admin",
            pattern: "{area:exists}/{controller=Main}/{action=Index}/{id?}"
          );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
