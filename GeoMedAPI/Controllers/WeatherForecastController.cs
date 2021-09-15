using GeoMed.Main.DTO.Patients;
using GM.QueueService.QueueDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeoMedAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger _logger;

      // private readonly IQueueService<QueueMessage> QueueService;

        public WeatherForecastController(ILoggerFactory logger)
        {
            _logger = logger.CreateLogger($"MyWeatherLogger{DateTime.Now.Date}");
           // QueueService = queueService;
        }

        //[Authorize]
        [HttpGet]
        public  IEnumerable<WeatherForecast> Get()
        {

             //QueueService.Consume(new QueueMessage {
             //  Data = JsonConvert.SerializeObject( new GetPatientDto()
             //  {
             //      Address = "aAAAA",
             //      Age  = 21 ,
             //      Gender = "dfd",
             //      Id = 2 ,
             //      LastInComeDate = DateTime.Now,
             //      PatientName = "sdfksdslf",
             //  }),
             //  Type = typeof(GetPatientDto)
             //});
            var rng = new Random();
            _logger.LogInformation("Authorized successfully");

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();

        }
    }
}
