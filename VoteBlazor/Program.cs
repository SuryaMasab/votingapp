using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using VoteBlazor.Data;
using VoteBlazor.Services;

var builder = WebApplication.CreateBuilder(args);


 
var baseUrl = builder.Configuration.GetSection("ApiUrl").GetValue<string>("Url");
if (string.IsNullOrEmpty(baseUrl))
{
    throw new Exception("ApiUrl is not configured.");
}

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(baseUrl) // Set your base address here
});
 
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<IVoterService, VoterService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
