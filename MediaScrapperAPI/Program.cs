using Common.Extensions;
using MediaScrapper.Configuration;
using MediaScrapper.Endpoints;
using MediaScrapper.Extensions;
using System.Net;

var builder = WebApplication.CreateBuilder(args);


var appConfig = builder.Configuration.Get<AppConfig>() ?? new AppConfig();

if (Environment.GetEnvironmentVariable(Constants.ASPNETCORE_ENVIRONMENT) != Environments.Development && appConfig.UseLegitSslCertificate)
{
    builder.WebHost.UseKestrel(options =>
    {
        options.Listen(IPAddress.Any, 443, listenOptions =>
        {
            listenOptions.UseHttps(Environment.GetEnvironmentVariable("CERTIFICATE_PATH") ?? throw new Exception("missing path for SSL certificate"),
                                   Environment.GetEnvironmentVariable("CERTIFICATE_PASSWORD"));
        });
    });
}


builder.Services.AddServices(appConfig);

builder.Services.AddCors();

builder.Services.AddAppAuthorizationPolicies(appConfig);

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
