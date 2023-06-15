using LinqToDB.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.AzureAppServices;
using Microsoft.IdentityModel.Tokens;
using Prostech.WMS.API.Controllers;
using Prostech.WMS.API.Middleware;
using Prostech.WMS.API.Models;
using Prostech.WMS.BLL;
using Prostech.WMS.BLL.AutoMapper;
using Prostech.WMS.BLL.Interface;
using Prostech.WMS.DAL.DBContext;
using Prostech.WMS.DAL.DBContext.Interface;
using Prostech.WMS.DAL.Models;
using Prostech.WMS.DAL.Repositories.WMS;
using Prostech.WMS.DAL.Repositories.WMS.Base;
using Prostech.WMS.DAL.Repositories.WMS.Interface;
using System.Text;

try
{
    var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
    .Build();

    string envInUsed = configuration.GetSection("AppSettings:ASPNETCORE_ENVIRONMENT").Value;


    var builder = WebApplication.CreateBuilder(new WebApplicationOptions
    {
        Args = args,
        EnvironmentName = envInUsed
    });

    builder.Logging.ClearProviders();
    builder.Logging.AddConsole();
    builder.Logging.AddDebug();
    builder.Logging.AddAzureWebAppDiagnostics();

    builder.Services.Configure<AzureFileLoggerOptions>(options =>
    {
        options.FileName = "my-azure-diagnostics-"+ DateTime.Now;
        options.FileSizeLimit = 10 * 1024;
        options.RetainedFileCountLimit = 5;
    });

    //Config to use Appsetting objects in appsettings json
    var appConfigSection = builder.Configuration.GetSection("AppSettings");
    builder.Services.Configure<AppSettings>(appConfigSection);

    //Get urls for allow CORS
    string[] apiAllowOrigin = builder.Configuration.GetSection("AppSettings:AllowOrigin").Value.Split(',');


    //services cors
    builder.Services.AddCors(p => p.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials()
        .WithOrigins(apiAllowOrigin);
    }));

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddControllers().AddNewtonsoftJson(options =>
    {
        options.UseMemberCasing();
        //options.SerializerSettings.Converters.Add(new TrimmingConverter());
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });

    //Get db connection string from appsetting
    string wmsDbConnStr = builder.Configuration.GetSection("AppSettings:DatabaseConnection:WMS").Value;

    //DBContext
    builder.Services.AddScoped<IWMSContext>(x =>
    new WMSContext(new DbContextOptionsBuilder<WMSContext>().UseNpgsql(wmsDbConnStr).Options));

    //Service
    builder.Services.AddScoped<IProductItemService, ProductItemService>();
    builder.Services.AddScoped<IProductService, ProductService>();
    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddScoped<IUserAccountService, UserAccountService>();
    builder.Services.AddScoped<IActionHistoryService, ActionHistoryService>();

    //Repository
    builder.Services.AddScoped(typeof(IWMSGenericRepository<>), typeof(WMSGenericRepository<>));

    builder.Services.AddScoped<IProductItemRepository, ProductItemRepository>();
    builder.Services.AddScoped<IProductRepository, ProductRepository>();
    builder.Services.AddScoped<IActionHistoryRepository, ActionHistoryRepository>();
    builder.Services.AddScoped<IBrandRepository, BrandRepository>();
    builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
    builder.Services.AddScoped<IProductItemStatusRepository, ProductItemStatusRepository>();
    builder.Services.AddScoped<IUserAccountRepository, UserAccountRepository>();

    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

    // JWT Authentication
    var jwtSettings = builder.Configuration.GetSection("JwtSettings");
    var key = Encoding.ASCII.GetBytes(jwtSettings.GetValue<string>("Secret"));

    //Auto Mapper
    builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false; // Only use in development environment
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false, // Set to true if you have a specific issuer
            ValidateAudience = false, // Set to true if you have a specific audience
        };
    });

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseCors("CorsPolicy");

    app.UseAuthorization();

    app.UseAuthentication();

    app.UseMiddleware<ResponseWrappingMiddleware>();

    app.UseMiddleware<SecurityMiddleware>();

    app.MapControllers();

    app.Run();

}
catch
{
    throw;
}