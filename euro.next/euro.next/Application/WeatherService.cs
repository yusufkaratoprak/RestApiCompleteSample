using euro.next.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace euro.next.Application
{
	public class WeatherService : IWeatherService
	{
		private readonly IWeatherRepository _weatherRepository;

		public WeatherService(IWeatherRepository weatherRepository)
		{
			_weatherRepository = weatherRepository;
		}

		public async Task<IEnumerable<WeatherForecast>> GetWeeklyForecastAsync(DateTime startDate)
		{
			return await _weatherRepository.GetWeeklyForecastAsync(startDate);
		}

		public async Task AddForecastAsync(WeatherForecast forecast)
		{
			await _weatherRepository.AddForecastAsync(forecast);
		}
	}
}
