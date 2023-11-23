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
        //In the old version, locally configs were in appsettings.json and it made a big problem on each deploy. I had to change lines because
        //the cloud servers use Environment variables. So I had to use Env. Variables on my local setup too. These lines are from old days to comply
        //with cloud
        //From here
        //ValidIssuer = Environment.GetEnvironmentVariable("JwtIssuer"),
        //ValidAudience = Environment.GetEnvironmentVariable("JwtAudience"),
        //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JwtKey"))),
        //Up to here


        //Now in local setup I am using Env. Variables too.
        //ValidIssuer = builder.Configuration["Jwt:Issuer"],    //Use these lines if the config parameters must be read from appsettings.json
        ValidIssuer = Environment.GetEnvironmentVariable("ASPNETCORE_JwtIssuer"),
        //ValidAudience = builder.Configuration["Jwt:Audience"],    //Use these lines if the config parameters must be read from appsettings.json
        ValidAudience = builder.Configuration[Environment.GetEnvironmentVariable("ASPNETCORE_JwtAudience")],
        //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),    //Use these lines if the config parameters must be read from appsettings.json
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("ASPNETCORE_JwtKey"))),

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
