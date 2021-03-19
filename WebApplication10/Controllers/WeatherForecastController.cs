using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using DAL.Models;

namespace MeterApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MeterController : ControllerBase
    {
        private readonly ILogger<MeterController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public MeterController(ILogger<MeterController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        //[HttpGet]
        //public IEnumerable<WeatherForecast> Get()
        //{
        //    _unitOfWork.Houses.Add(new House
        //    {
        //        FiasGuid =  "dsdsfdsfds"
        //    });

        //    var rng = new Random();
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateTime.Now.AddDays(index),
        //        TemperatureC = rng.Next(-20, 55),
        //        Summary = Summaries[rng.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}
    }
}
