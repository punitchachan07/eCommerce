using eCommerce.Infrastructure;
using eCommerce.Core;
using eCommerce.API.Middlewares;
using eCommerce.Core.Mappers;
using eCommerce.Core.Validators;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructureServices();
builder.Services.AddCoreServices();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(ApplicationUserMappingProfile).Assembly);

// Register FluentValidation auto-validation and discover validators in the assembly

// Register FluentValidation auto-validation and discover validators in the assembly
builder.Services.AddFluentValidationAutoValidation();

var app = builder.Build();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.UseExceptionHandlingMiddleware();

app.Run();