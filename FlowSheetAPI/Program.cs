using FlowSheetAPI.Repository;
using FlowSheetAPI.Repository.Implementation;
using FlowSheetAPI.Repository.Interfaces;
using FlowSheetAPI.Services.Implementation;
using FlowSheetAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Okta.AspNetCore;
using Serilog;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Bind Services
builder.Services.AddScoped<ICacheService, CacheService>();
builder.Services.AddScoped<ILookupService, LookupService>();
builder.Services.AddScoped<IFlowsheetService, FlowsheetService>();
builder.Services.AddScoped<IEhrUserService, EhrUserService>();

// Bind Repositories
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(ILookupRepository<>), typeof(LookupRepository<>));

// add sql
builder.Services.AddDbContext<FlowSheetDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add DbContext using PostGre Server Provider
builder.Services.AddDbContext<FlowSheetDbContext>(ServiceLifetime.Scoped);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"))
        .EnableTokenAcquisitionToCallDownstreamApi()
            .AddMicrosoftGraph(builder.Configuration.GetSection("MicrosoftGraph"))
            .AddInMemoryTokenCaches()
            .AddDownstreamApi("DownstreamApi", builder.Configuration.GetSection("DownstreamApi"))
            .AddInMemoryTokenCaches();

//// Load Serilog configuration from appsettings.json
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

// Add Serilog to the logging pipeline
var blobConfiguration = builder.Configuration.GetConnectionString("AzureblobConnectionString");
const string storageFileName = "{yyyy}/{MM}/{dd}/chFlowSheet_log.txt";

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .Enrich.FromLogContext()
    .MinimumLevel.Debug() // Adjust based on your needs
    .WriteTo.AzureBlobStorage(
        connectionString: blobConfiguration, // Ensure this is your Azure Blob Storage connection string
        storageFileName: storageFileName)
    .CreateLogger();

builder.Logging.ClearProviders(); // Clear existing providers if necessary
builder.Logging.AddSerilog(logger);

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });

    options.AddPolicy("AllowAnyOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });

    options.AddPolicy("AllowLocalHostOrigins",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

builder.Services.AddAuthorization(options =>
{
    options.DefaultPolicy = new AuthorizationPolicyBuilder(OktaDefaults.ApiAuthenticationScheme).RequireAuthenticatedUser().Build();
});

// Add Distributed Memory Cache
builder.Services.AddDistributedMemoryCache();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllersWithViews().AddNewtonsoftJson();

// Add HttpContextAccessor
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors();
app.UseCors("AllowAnyOrigins");
app.UseCors("AllowLocalHostOrigins");

app.MapControllers();

app.UseDefaultFiles();
app.UseStaticFiles();

app.Run();
