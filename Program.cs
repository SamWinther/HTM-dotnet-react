using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.Identity.Web;
using HTMbackend;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//using var db = new BloggingContext();
using var db = new HTMbackend.HTM.HtmContext();
//db.Database.EnsureCreated();


// Add services to the container.
builder.Services.AddScoped<HTMbackend.HTM.HtmContext, HTMbackend.HTM.HtmContext>();


builder.Services.AddCors();
builder.Services.AddControllers();
//builder.Services.AddAuthentication();
//JWT added code start
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = Environment.GetEnvironmentVariable("JwtIssuer"),
        ValidAudience = Environment.GetEnvironmentVariable("JwtAudience"),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JwtKey"))),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});
builder.Services.AddAuthorization();
// Add configuration from appsettings.json
//builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
//    .AddEnvironmentVariables();
//JWT added code end
builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();

app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials
app.UseAuthentication();


//JWT Added start
app.UseAuthorization();
IConfiguration configuration = app.Configuration;
IWebHostEnvironment environment = app.Environment;
//JWT Added end


app.UseHttpsRedirection();


app.MapControllers();
app.Run();
