using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using HTMbackend;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

//using var db = new BloggingContext();
using var db = new HTMbackend.HTM.HtmContext();
//db.Database.EnsureCreated();


// Add services to the container.
builder.Services.AddScoped<HTMbackend.HTM.HtmContext, HTMbackend.HTM.HtmContext>();


builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddAuthentication();
builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();

app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials

app.UseAuthorization();
app.UseHttpsRedirection();


app.MapControllers();
app.Run();
