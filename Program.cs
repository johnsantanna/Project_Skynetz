using Microsoft.EntityFrameworkCore;
using Project_Skynetz.Data;
using Project_Skynetz.ModelBinders;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// cultureInfo
var cultureInfo = new CultureInfo("pt-BR");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

// ModelBinder customizado para aceitar virgula em decimais
builder.Services.AddControllersWithViews(options =>
{
    options.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider());
});

builder.Services.AddDbContext<SkynetzContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// força cultureInfo para todas as req
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(cultureInfo),
    SupportedCultures = new[] { cultureInfo },
    SupportedUICultures = new[] { cultureInfo }
});

// pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
