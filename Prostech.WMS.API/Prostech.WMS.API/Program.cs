using LinqToDB.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging.AzureAppServices;
using Microsoft.IdentityModel.Tokens;
using Prostech.WMS.API.Controllers;
using Prostech.WMS.API.Middleware;
using Prostech.WMS.API.Models;
using Prostech.WMS.BLL;
using Prostech.WMS.BLL.AutoMapper;
using Prostech.WMS.BLL.Helpers.ValueChecker;
using Prostech.WMS.BLL.Interface;
using Prostech.WMS.DAL.DBContext;
using Prostech.WMS.DAL.DBContext.Interface;
using Prostech.WMS.DAL.Models;
using Prostech.WMS.DAL.Repositories.WMS;
using Prostech.WMS.DAL.Repositories.WMS.Base;
using Prostech.WMS.DAL.Repositories.WMS.Interface;
using System.Net.Sockets;
using System.Net;
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

    //Application Insights Logging
    
    string azureApplicationInsightsLogging = builder.Configuration.GetSection("AzureLoggingConnectionString").Value;
    if (ValueCheckerHelper.IsNotNullOrEmpty(azureApplicationInsightsLogging))
    {
        builder.Logging.AddApplicationInsights(
            configureTelemetryConfiguration: (config) => config.ConnectionString = builder.Configuration.GetSection("AzureLoggingConnectionString").Value,
            configureApplicationInsightsLoggerOptions: (options) => { }
        );
    }

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
    builder.Services.AddMemoryCache();
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

    await OpenSocketAsync();

    app.Run();

}
catch
{
    throw;
}

static async Task OpenSocketAsync()
{
    // Set the IP address and port number to listen on
    IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
    int port = 4341;

    // Create a TCP/IP socket
    Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

    try
    {
        // Bind the socket to the IP address and port
        listener.Bind(new IPEndPoint(ipAddress, port));

        // Start listening for incoming connections
        listener.Listen(10);

        // Accept incoming connections in a loop
        while (true)
        {
            // Accept the connection and create a new socket for communication
            Socket handler = await listener.AcceptAsync();

            // Handle the connection in a separate thread
            ThreadPool.QueueUserWorkItem(HandleClient, handler);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: {0}", ex.Message);
        throw new Exception(ex.ToString());
    }
}

static void HandleClient(object state)
{
    Socket handler = (Socket)state;

    try
    {
        // Handle the communication with the client socket here
        // For example, you can read/write data using the handler socket
        // Remember to close the socket when done

        // Close the client socket
        handler.Shutdown(SocketShutdown.Both);
        handler.Close();
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: {0}", ex.Message);
        throw new Exception(ex.ToString());
    }
}