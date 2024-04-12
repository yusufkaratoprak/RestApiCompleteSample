using euro.next.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace euro.next.Application
{
	public interface IWeatherService
	{
		Task<IEnumerable<WeatherForecast>> GetWeeklyForecastAsync(DateTime startDate);
		Task AddForecastAsync(WeatherForecast forecast);
	}
}
