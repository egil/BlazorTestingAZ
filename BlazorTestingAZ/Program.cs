using BlazorTestingAZ.Components;
using BlazorTestingAZ.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<WeatherForecastRepo>();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

// Makes the web app entry point public to
// allow BlazorApplicationFactory/WebApplicationFactory
// to reference it.
public partial class Program { }