using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OrderManagement.Application.Commands;
using OrderManagement.Application.Validators;
using OrderManagement.Domain.Repositories;
using OrderManagement.Infrastructure.Data;
using OrderManagement.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Add MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateOrderCommand).Assembly));

// Add FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateOrderCommandValidator>();

// Add DbContext
builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Repository
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

// Add Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Order Management API",
        Version = "v1",
        Description = "An API to manage customer orders",
        Contact = new OpenApiContact
        {
            Name = "Support Team",
            Email = "support@ordermanagement.com"
        }
    });

    // Use fully qualified object names
    c.CustomSchemaIds(type => type.FullName);

    // Include XML comments if they exist
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

var app = builder.Build();

// Configure the HTTP request pipeline
// Always enable Swagger in Docker
app.UseSwagger(c =>
{
    c.SerializeAsV2 = false; // Use OpenAPI 3.0
});
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Order Management API V1");
    c.RoutePrefix = "swagger";
    c.DocumentTitle = "Order Management API Documentation";
    c.DefaultModelsExpandDepth(2);
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Apply migrations
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<OrderDbContext>();
    dbContext.Database.Migrate();
}

app.Run(); 