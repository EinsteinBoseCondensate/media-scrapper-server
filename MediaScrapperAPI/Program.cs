using MediaScrapper.Configuration;
using MediaScrapper.Endpoints;
using MediaScrapper.Extensions;

var builder = WebApplication.CreateBuilder(args);

var appConfig = builder.Configuration.Get<AppConfig>();

builder.Services.AddServices(appConfig);

builder.Services.AddCors();

builder.Services.AddAppAuthorizationPolicies();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors(policy =>
{
    policy.AllowAnyHeader()
    .AllowAnyOrigin();
});

app.UseStaticFiles();
app.MapFallbackToFile("index.html");
app.MapAllEndpoints();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.Run();
