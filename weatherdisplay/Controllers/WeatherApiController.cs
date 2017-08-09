using AutoMapper;
using weatherdisplaytest.Core.Interfaces;
using weatherdisplaytest.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web.Http;

namespace weatherdisplaytest.Controllers
{
    [RoutePrefix("api/weather")]
    public class WeatherController : ApiController
    {
        private IWeatherService weatherService;

        private IMapper mapper;

        public WeatherController(IWeatherService weatherService, IMapper mapper)
        {
            this.weatherService = weatherService;
            this.mapper = mapper;
        }

        [Route("cities/{countryName}")]
        public List<CityWeather> GetCities(string countryName)
        {
            var cities = weatherService.GetCitiesByCountry(countryName);
            return mapper.Map<string, List<CityWeather>>(cities);
        }

        [Route("currentconditions/{cityName}")]
        public CityWeather GetWeatherForCity(string cityName)
        {
            var weatherData = weatherService.GetWeatherDataForCity(cityName);
            return mapper.Map<RootObject, CityWeather>(JsonConvert.DeserializeObject<RootObject>(weatherData));
        }
    }
}