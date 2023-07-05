using XBInsaat.Data.Datacontext;
using System.Configuration;
using XBInsaat.Mvc.ServiceExtentions;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using XBInsaat.Services.Services.Implementations.User;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using System.Reflection;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);


#region Localizer
builder.Services.AddSingleton<LanguageService>();
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddMvc().AddViewLocalization().AddDataAnnotationsLocalization(options =>
    options.DataAnnotationLocalizerProvider = (type, factory) =>
    {
        var assemblyName = new AssemblyName(typeof(SharedResource).GetTypeInfo().Assembly.FullName);
        return factory.Create(nameof(SharedResource), assemblyName.Name);
    });

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportCultures = new List<CultureInfo>
    {
        new CultureInfo("az-Latn-AZ"),
        new CultureInfo("en-US"),
        new CultureInfo("ru-RU"),

    };
    options.DefaultRequestCulture = new RequestCulture(culture: "az-Latn-AZ", uiCulture: "az-Latn-AZ");
    options.SupportedCultures = supportCultures;
    options.SupportedUICultures = supportCultures;
    options.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());
});
#endregion

// Add services to the container.
builder.Services.AddControllersWithViews();



//builder.Services.AddControllers().AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<PosterCreateDto>());

builder.Services.AddDbContext<DataContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddIdentityServiceExtention();
builder.Services.AddServiceScopeExtention();

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

app.AddExceptionHandlerService();

app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
