using HTMbackend;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//using var db = new BloggingContext();
using var db = new HTMbackend.HTM.HtmContext();


// Add services to the container.
builder.Services.AddScoped<HTMbackend.HTM.HtmContext, HTMbackend.HTM.HtmContext>();

//services.AddScoped<IBloggerRepository, BloggerRepository>;

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
