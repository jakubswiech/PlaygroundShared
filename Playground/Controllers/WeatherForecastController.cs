﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PlaygroundShared.Domain.Domain;
using PlaygroundShared.Domain.DomainEvents;

namespace Playground.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IDomainEventsManager _domainEventsManager;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IDomainEventsManager domainEventsManager)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _domainEventsManager = domainEventsManager ?? throw new ArgumentNullException(nameof(domainEventsManager));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var rng = new Random();
            var results = Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)],
                    Id = AggregateId.Generate()
                })
                .ToArray();

            foreach (var result in results)
            {
                _domainEventsManager.Publish(result);   
            }

            return Ok(results);
        }
    }
}