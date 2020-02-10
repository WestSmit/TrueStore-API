using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BLL.Interfaces;
using BLL.Models;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {


        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IProductService _produuctService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IProductService productServices)
        {
            _logger = logger;
            _produuctService = productServices;
        }

        [HttpGet]
        public IEnumerable<ProductModel> Get()
        {
            return _produuctService.GetProducts() ;
        }
    }
}
