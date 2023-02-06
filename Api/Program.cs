using Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureDb(builder.Configuration);
builder.Services.AddControllers();
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