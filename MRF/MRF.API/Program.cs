using Microsoft.EntityFrameworkCore;
using MRF.DataAccess.Data;
using MRF.DataAccess.Repository;
using MRF.DataAccess.Repository.IRepository;
using MRF.Utility;
using NLog;
using NLog.Web;
using Microsoft.Identity.Web;


var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
var builder = WebApplication.CreateBuilder(args);

var environment = "Production";
var config = new ConfigurationBuilder()
       .AddJsonFile("appsettings." + environment + ".json", optional: false)
       .Build();
// Add services to the container.
builder.Logging.ClearProviders();
builder.Host.UseNLog();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddSingleton<ILoggerService, LoggerService>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MRFDBContext>(options =>
        options.UseMySql(config.GetConnectionString("DbConnectionString"), ServerVersion.AutoDetect(config.GetConnectionString("DbConnectionString")))
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging());

builder.Services.AddMicrosoftIdentityWebAppAuthentication(config);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", builder =>
    {
        builder
            .AllowAnyOrigin() // Allow requests from any origin (for development, consider tightening this)
            .AllowAnyMethod() // Allow any HTTP methods
            .AllowAnyHeader(); // Allow any headers
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseCors("AllowReactApp");
app.Run();
