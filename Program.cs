using BlazorLocalizationDemo.Components;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Register Localization Services
// This enables IStringLocalizer<T> and resource (.resx) lookup
builder.Services.AddLocalization(options =>
{
    options.ResourcesPath = "Resources";    // Folder where .resx filesare located
});

var app = builder.Build();

// Supported Cultures Configuration
var supportedCultures = new[] 
{
    new CultureInfo("en"),
    new CultureInfo("es"),
    new CultureInfo("de")
};

var localizationOptions = new RequestLocalizationOptions()
{
    // Default culture if no cookie or header found
    DefaultRequestCulture = new RequestCulture("en"),

    // Culture used for formatting (dates, numbers, etc.)
    SupportedCultures = supportedCultures,

    // Culture used for UI strings (.resx)
    SupportedUICultures = supportedCultures
};

// Use Cookie as the culture provider
// Reads and writes the .AspNetCore.Culture cookie
localizationOptions.RequestCultureProviders = new[]
{
    new CookieRequestCultureProvider() 
};

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

// Enable localization for each request
// IMPORTANT: This must be placed BEFORE endpoints
app.UseRequestLocalization(localizationOptions);

app.UseStaticFiles();
app.UseAntiforgery();


// Endpoint that updates the culture cookie and redirects back
app.MapGroup("/culture")
    .MapGet("/set", (HttpContext http, string culture, string? returnUrl) =>
    {
        if (string.IsNullOrEmpty(culture))
        {
            culture = "en";
        }
        var cookieValue = CookieRequestCultureProvider.MakeCookieValue(
            new RequestCulture(culture));

        http.Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            cookieValue,
            new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddDays(30),
                IsEssential = true,
                SameSite = SameSiteMode.Lax,
                Path = "/"
            });
        // Redirect back to the page that triggered the culture change
        return Results.Redirect(returnUrl ?? "/");
    });

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
