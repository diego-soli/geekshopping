using GeekShopping.Web.Services;
using GeekShopping.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

//IConfiguration configuration = builder.Configuration; // nao é necessario.

// Add services to the container.

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddHttpClient<IProductService,ProductService>(c => c.BaseAddress = new Uri(builder.Configuration["ServiceUrls:ProductAPI"]));
builder.Services.AddHttpClient<ICartService,CartService>(c => c.BaseAddress = new Uri(builder.Configuration["ServiceUrls:CartAPI"]));
builder.Services.AddHttpClient<ICouponService, CouponService>(c => c.BaseAddress = new Uri(builder.Configuration["ServiceUrls:CouponAPI"]));
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(options => 
{
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "oidc";

})
    .AddCookie("Cookies", c => c.ExpireTimeSpan = TimeSpan.FromMinutes(10))
    .AddOpenIdConnect("oidc",options =>
    {
        options.Authority = builder.Configuration["ServiceUrls:IdentityServer"];
        options.GetClaimsFromUserInfoEndpoint = true;
        options.ClientId = "geek_shopping";
        options.ClientSecret = "my_super_secret"; //pode ser pego de appsettings ou de outro arquivo, agora esta vindo de IdentityConfigurarion
        options.ResponseType = "code";
        options.ClaimActions.MapJsonKey("role", "role", "role");
        options.ClaimActions.MapJsonKey("sub", "sub", "sub");
        options.TokenValidationParameters.NameClaimType = "name";
        options.TokenValidationParameters.RoleClaimType = "role";
        options.Scope.Add("geek_shopping");
        options.SaveTokens = true;
    });



//adicionar o padrao para Brasil, ver como funcionaria se o app fosse para outra localidade.
//var cultureInfo = new CultureInfo("pt-BR",true);

//cultureInfo.NumberFormat.NumberDecimalSeparator = ".";

//cultureInfo.NumberFormat.CurrencyDecimalSeparator = ".";

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-BR");

CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("pt-BR");

//builder.Services.Configure<RequestLocalizationOptions>(options =>
//{
//    options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(cultureInfo);
//    options.SupportedCultures = new List<CultureInfo> { new CultureInfo("pt-BR") };
//});

var app = builder.Build();

//adicionei para ver se funciona
////var supportedCultures = new[] { new CultureInfo("pt-BR") };
////app.UseRequestLocalization(new RequestLocalizationOptions
////{
////    DefaultRequestCulture = new RequestCulture("pt-BR"),
////    SupportedCultures = supportedCultures,
////    SupportedUICultures = supportedCultures
////});


app.UseRequestLocalization();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
