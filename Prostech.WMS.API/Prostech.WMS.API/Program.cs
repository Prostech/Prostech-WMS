using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Prostech.WMS.API.Models;
using Prostech.WMS.BLL;
using Prostech.WMS.BLL.Interface;
using Prostech.WMS.DAL.DBContext;
using Prostech.WMS.DAL.DBContext.Interface;
using Prostech.WMS.DAL.Models;
using Prostech.WMS.DAL.Repositories.WMS;
using Prostech.WMS.DAL.Repositories.WMS.Base;
using Prostech.WMS.DAL.Repositories.WMS.Interface;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
    .Build();

string envInUsed = configuration.GetSection("AppSettings:ASPNETCORE_ENVIRONMENT").Value;


var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    EnvironmentName = envInUsed
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

//Repository
builder.Services.AddScoped(typeof(IWMSGenericRepository<>), typeof(WMSGenericRepository<>));

builder.Services.AddScoped<IProductItemRepository, ProductItemRepository>();

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

app.MapControllers();

app.Run();
