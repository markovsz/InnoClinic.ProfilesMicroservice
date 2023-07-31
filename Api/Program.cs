using Api.Extensions;
using Api.Middlewares;
using FluentValidation.AspNetCore;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureDb(builder.Configuration);
builder.Services.ConfigureRepositoryManager();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.ConfigureMassTransit(builder.Configuration);
builder.Services.ConfigureServices();
builder.Services.ConfigureValidators();
builder.Services.ConfigureAuthentication(builder.Configuration);
builder.Services.ConfigureFilterAttributes();
builder.Services.AddControllers()
    .AddFluentValidation(options => {
        options.RegisterValidatorsFromAssemblyContaining<Program>();
    });
builder.Services.AddCors();
builder.Services.ConfigureSwagger();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionsHandler>();

app.UseCors(e =>
{
    e.AllowAnyHeader();
    e.AllowAnyMethod();
    e.AllowAnyOrigin();
});

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();