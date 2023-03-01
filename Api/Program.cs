using Api.Extensions;
using FluentValidation.AspNetCore;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureDb(builder.Configuration);
builder.Services.ConfigureRepositoryManager();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.ConfigureServices();
builder.Services.ConfigureValidators();
builder.Services.AddControllers()
    .AddFluentValidation(options => {
        options.RegisterValidatorsFromAssemblyContaining<Program>();
    });
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();