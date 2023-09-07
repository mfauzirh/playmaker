using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Playmaker.Data;
using Playmaker.Dtos;
using Playmaker.Middleware;
using Playmaker.Repositories;
using Playmaker.Services;

var builder = WebApplication.CreateBuilder(args);

// Adding Logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Make Url All Lowercase
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Add controller based API
builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = c =>
    {
        var errors = string.Join(' ', c.ModelState.Values
            .Where(v => v.Errors.Count > 0)
            .SelectMany(v => v.Errors)
            .Select(v => v.ErrorMessage));

        return new BadRequestObjectResult(new Response<object>
        {
            Success = false,
            Errors = errors
        });
    };
});

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection")));

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IAuthServices, AuthService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ErrorHandlerMiddlware>();

app.MapControllers();

app.Run();
