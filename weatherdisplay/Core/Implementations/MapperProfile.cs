using AutoMapper;
using weatherdisplaytest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace weatherdisplaytest.Core.Implementations
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<string, List<CityWeather>>()
                .BeforeMap((src, dst) =>
                {
                    foreach (var table in (XDocument.Parse(Regex.Replace(src, @"\t|\n|\r", "")).Descendants("Table")))
                    {
                        var countryName = table.Descendants("Country").SingleOrDefault().Value;
                        var city = new CityWeather { Name = table.Descendants("City").SingleOrDefault().Value, Country = countryName };
                        dst.Add(city);                        
                    }
                })
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<RootObject, CityWeather>()
                .ForMember(dst => dst.Location, opt => opt.MapFrom(src => "Lat: " + src.coord.lat + " Lon: " + src.coord.lon))
                .ForMember(dst => dst.Time, opt => opt.MapFrom(src => new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(src.dt).ToLocalTime()))
                .ForMember(dst => dst.Wind, opt => opt.MapFrom(src => src.wind.speed))
                .ForMember(dst => dst.Visibility, opt => opt.MapFrom(src => src.visibility))
                .ForMember(dst => dst.SkyConditions, opt => opt.MapFrom(src => src.weather.ElementAtOrDefault(0).description))
                .ForMember(dst => dst.Temperature, opt => opt.MapFrom(src => src.main.temp))
                .ForMember(dst => dst.DewPoint, opt => opt.UseValue("Not Available"))
                .ForMember(dst => dst.RelativeHumidity, opt => opt.MapFrom(src => src.main.humidity))
                .ForMember(dst => dst.Pressure, opt => opt.MapFrom(src => src.main.pressure))
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.name))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}