using weatherdisplaytest.Core.Interfaces;
using weatherdisplaytest.GlobalWeather;
using System.ServiceModel;
using System.Net;

namespace weatherdisplaytest.Core.Implementations
{
    public class ServiceHandlerFactory : IServiceHandlerFactory
    {
        readonly string owmUri = @"http://api.openweathermap.org/data/2.5/weather?q={0}&units=metric&AppID=6f8a397e7569df7ea3a5724e3753c53a";

        public string OwmUri => owmUri;


        public GlobalWeatherSoapChannel CreateGlobalWeatherChannel()
        {
            return new ChannelFactory<GlobalWeatherSoapChannel>("GlobalWeatherSoap").CreateChannel();
        }

        public WebClient CreateOpenWebMapApi(string CityName, out string uri)
        {
            uri = string.Format(OwmUri, CityName);
            return new WebClient();
        }
    }
}