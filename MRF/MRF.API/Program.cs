using Microsoft.EntityFrameworkCore;
using MRF.DataAccess.Data;
using MRF.DataAccess.Repository;
using MRF.DataAccess.Repository.IRepository;
using MRF.Utility;
using NLog;
using NLog.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.FileProviders;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
var builder = WebApplication.CreateBuilder(args);

var environment = builder.Environment.EnvironmentName;
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
builder.Services.AddTransient<IHTMLtoPDF, HTMLtoPDF>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddDbContext<MRFDBContext>(options =>
        options.UseMySql(config.GetConnectionString("DbConnectionString"), ServerVersion.AutoDetect(config.GetConnectionString("DbConnectionString")))
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging());

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);//We set Time here 
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                  
        });
   
});


builder.Services.AddSwaggerGen(
    c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "swagger azure ad", Version = "v1" });
        c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
        {
            Description = "Oauth2.0",
            Name = "Oauth2.0",
            Type = SecuritySchemeType.OAuth2,
            Flows = new OpenApiOAuthFlows
            {
                AuthorizationCode = new OpenApiOAuthFlow
                {
                    AuthorizationUrl = new Uri(builder.Configuration["SwaggerAzureAD:AuthorizationUrl"]),
                    TokenUrl = new Uri(builder.Configuration["SwaggerAzureAD:TokenUrl"]),
                    Scopes = new Dictionary<string, string>
               {
                   { builder.Configuration["SwaggerAzureAD:Scope"],"Access API as User" }
               }
                }
            }
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
           new OpenApiSecurityScheme
           {
               Reference=new OpenApiReference{Type=ReferenceType.SecurityScheme,Id="oauth2"}

           },new[] {builder.Configuration["SwaggerAzureAD:Scope"] }
            }
        });
    }
    );


var app = builder.Build();

app.UseStaticFiles();

/*If you want to serve files from a specific directory, you can use:*/
var staticFilesPath = Path.Combine(builder.Configuration["FileUploadSettings:FallbackPath"], "Resume");
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(staticFilesPath),
    RequestPath = "/Resume"
});

// If you want to serve files from a specific directory, you can use:
var staticFilesPath1 = Path.Combine(builder.Configuration["FileUploadSettings:FallbackPath"], "Assignment");
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(staticFilesPath1),
    RequestPath = "/Assignment"
});

//var staticFilesPath2 = Path.Combine(builder.Configuration["FileUploadSettings:FallbackPath"], "Feedback");
//app.UseStaticFiles(new StaticFileOptions
//{
//    FileProvider = new PhysicalFileProvider(staticFilesPath2),
//    RequestPath = "/Feedback"
//});



// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
    app.UseSwaggerUI(c => {
    c.OAuthClientId(builder.Configuration["SwaggerAzureAD:ClientId"]);
    c.OAuthUsePkce();
    c.OAuthScopeSeparator(" ");
}
    ); 
//}
app.UseCors();
app.UseMiddleware<ExceptionMiddleware>();
app.UseSession();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
