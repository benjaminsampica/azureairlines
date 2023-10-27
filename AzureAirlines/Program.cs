using AzureAirlines;
using AzureAirlines.Components;
using AzureAirlines.Components.Pages;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var folder = Environment.SpecialFolder.LocalApplicationData;
var path = Environment.GetFolderPath(folder);
var dbPath = Path.Join(path, "AzureAirlines.db");
builder.Configuration.AddInMemoryCollection(new List<KeyValuePair<string, string?>>
{
    new("ConnectionStrings:AzureAirlines", "DataSource=" + dbPath)
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("AzureAirlines"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddHttpClient("GitHub", client =>
{
    client.BaseAddress = new("https://api.github.com/graphql");
    client.DefaultRequestHeaders.Add("User-Agent", "Azure-Airlines");
    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {GitHubApi.ApiToken}");
});

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

using var scope = app.Services.CreateScope();

var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
dbContext.Database.EnsureCreated();

app.Run();
