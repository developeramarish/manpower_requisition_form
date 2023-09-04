using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
var config = new ConfigurationBuilder()
       .AddJsonFile("appsettings." + environment + ".json", optional: false)
       .Build();
WebProtocolSettings settings_Web = new WebProtocolSettings();
config.GetSection("WebProtocolSettings").Bind(settings_Web);


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOcelot();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.UseOcelot();
app.Run();

public class WebProtocolSettings
{
    public string Url { get; set; }
    public int Port { get; set; }
}