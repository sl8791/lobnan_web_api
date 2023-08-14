using Microsoft.AspNetCore.Http.Features;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using Innovi.Services.Interfaces;
using Innovi.Services.Repository;
using Innovi.Data;
using Microsoft.EntityFrameworkCore;
using Innovi.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
GlobalConfiguration(builder);

builder.Services.AddScoped<ICategorieRepository, CategoriesRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IPromotionRepository, PromotionRepository>();
builder.Services.AddScoped<ISwipeBannerRepository, SwipeBannerRepository>();
builder.Services.AddScoped<IMerchantRepository, MerchantRepository>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<IGovernorateRepository, GovernorateRepository>();
builder.Services.AddScoped<IBranchRepository, BranchRepository>();
builder.Services.AddScoped<IProductImageRepository, ProductImageRepository>();
builder.Services.AddScoped<IProductQuantityRepository, ProductQuantityRepository>();



builder.Services.Configure<FormOptions>(o =>
{
    o.ValueLengthLimit = int.MaxValue;
    o.MultipartBodyLengthLimit = int.MaxValue;
    o.MemoryBufferThreshold = int.MaxValue;
});


builder.Services.AddControllers().AddJsonOptions(options =>
           options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));





// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Lobnan v1 ", Version = "v1" });
});


var app = builder.Build();
app.UseDeveloperExceptionPage();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.DisplayRequestDuration();
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Lobnan v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static void GlobalConfiguration(WebApplicationBuilder builder)
{
    ConfigurationManager configuration = builder.Configuration;
    builder.Services.AddDbContext<EcommerceConetxt>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'EcommerceConetxt' not found."));
        options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    });


    builder.Services.Configure<FormOptions>(o =>
    {
        o.ValueLengthLimit = int.MaxValue;
        o.MultipartBodyLengthLimit = int.MaxValue;
        o.MemoryBufferThreshold = int.MaxValue;
    });
    builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });
    builder.Services.AddCors(o =>
        o.AddPolicy("MyPolicy", builder =>
        {
            builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
        }));
    builder.Services.AddRouting(r => r.SuppressCheckForUnhandledSecurityMetadata = true);
    builder.Services.AddAutoMapper(typeof(Program));

    builder.Services.AddAuthentication();
    builder.Services.AddAuthorization();

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
}