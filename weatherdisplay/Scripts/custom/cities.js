
function cityModel(name, country) {
    return {
        name: ko.observable(name),
        country: ko.observable(country),

        }
    };

WeatherViewModel = function () {
    var self = this;
    this.cities = ko.observableArray([]);
    this.weatherData = ko.observable('');
    this.getWeatherData = function (city) {
        this.weatherData = city;

        var url = '/api/weather/currentconditions/' + city.name();

        $.getJSON(encodeURI(url), function (data) {
            this.weatherData = ko.observable('');
            this.weatherData = data;
            ko.cleanNode($("#weatherDisplay")[0]);
            ko.applyBindings(this, document.getElementById("weatherDisplay"));
        })
        .done(function () {
            $("#weatherData").dialog({
                modal: true,
                closeOnEscape: true,
                position: { my: 'center', at: 'center', of: window }
            });
        });
    }
}

$('#weatherDataClose').on('click', function (event) {
    $("#weatherData").dialog('close');
});

CloseWeatherDialog= function (){
    $("#weatherData").dialog('close');
}

$(function () {
    $("#getCities").click(function () {
        weatherViewModel = new WeatherViewModel();

        var url = '/api/weather/cities/' + $('#countryName').val(); 

        $.getJSON(encodeURI(url), function (data) {

            for (var i = 0; i < data.length; i++) {
                weatherViewModel.cities.push(new cityModel(data[i].Name, data[i].Country));
            }
            ko.cleanNode($("#cityList")[0]);
            ko.applyBindings(weatherViewModel, document.getElementById("cityList"));
        })
        .done(function () {
            $("#weatherDis").show();
            });

        return false;
    });
});