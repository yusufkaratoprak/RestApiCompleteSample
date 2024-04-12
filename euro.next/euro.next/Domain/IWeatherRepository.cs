namespace euro.next.Domain
{
	public interface IWeatherRepository
	{
		Task<IEnumerable<WeatherForecast>> GetWeeklyForecastAsync(DateTime startDate);
		Task AddForecastAsync(WeatherForecast forecast);
	}
}
