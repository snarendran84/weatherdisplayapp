using weatherdisplaytest.GlobalWeather;
using System.Net;

namespace weatherdisplaytest.Core.Interfaces
{
    public interface IServiceHandlerFactory
    {
        GlobalWeatherSoapChannel CreateGlobalWeatherChannel();
        WebClient CreateOpenWebMapApi(string CityName,out string uri);
    }
}
