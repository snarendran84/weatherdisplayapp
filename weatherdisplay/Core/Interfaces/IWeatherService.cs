namespace weatherdisplaytest.Core.Interfaces
{
    public interface IWeatherService
    {
        string GetCitiesByCountry(string countryName);

        string GetWeatherDataForCity(string cityName);
    }
}
