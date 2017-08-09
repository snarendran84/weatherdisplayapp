using weatherdisplaytest.Core.Interfaces;
using System;

namespace weatherdisplaytest.Core.Implementations
{
    public class WeatherService : IWeatherService
    {
        private IServiceHandlerFactory serviceHandler;

        public WeatherService(IServiceHandlerFactory _serviceHandler)
        {
            serviceHandler = _serviceHandler;
        }

        public string GetCitiesByCountry(string countryName)
        {
            var cities = "Data not found";

            if (!String.IsNullOrEmpty(countryName))
            {
                using (var service = serviceHandler.CreateGlobalWeatherChannel())
                {
                    cities = service.GetCitiesByCountry(countryName);
                }
            }
            return cities;
        }

        public string GetWeatherDataForCity(string cityName)
        {
            var weatherData = "";
            if (!String.IsNullOrEmpty(cityName))
            {
                string uri;
                using (var service = serviceHandler.CreateOpenWebMapApi(cityName, out uri))
                {
                    try {
                        weatherData = service.DownloadString(uri);
                    }
                    catch(Exception ex)
                    {
                        //TODO: Handle exception
                        return weatherData;
                    }
                }
            }
            return weatherData;
        }
    }
}