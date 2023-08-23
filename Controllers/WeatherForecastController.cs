using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System;
using System.Text.Json;


namespace HTMbackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        //[HttpGet(Name = "GetWeatherForecast")]
        //public IEnumerable<WeatherForecast> Get()
        public string Get()
        {
            return "{ value: 1}";
        }
    }
}