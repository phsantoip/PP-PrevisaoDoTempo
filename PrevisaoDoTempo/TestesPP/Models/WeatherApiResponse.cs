using System;
using System.Collections.Generic;
using System.Text;

namespace TestesPP.Models
{
    using System.Collections.Generic;

    public class WeatherApiResponse
    {
        public Forecast forecast { get; set; }
    }

    public class Forecast
    {
        public List<ForecastDay> forecastday { get; set; }
    }

    public class ForecastDay
    {
        public Day day { get; set; }
    }

    public class Day
    {
        public double maxtemp_c { get; set; }
        public double mintemp_c { get; set; }
        public Condition condition { get; set; }
    }

    public class Condition
    {
        public string text { get; set; }
        public string icon { get; set; }
    }

}
