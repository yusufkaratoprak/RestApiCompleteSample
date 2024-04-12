using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using euro.next.Domain;
using Npgsql;

namespace euro.next.Infrastructure
{
	public class WeatherRepository : IWeatherRepository
	{
		private  string _connectionString;

		public WeatherRepository(IConfiguration configuration)
		{
			_connectionString = configuration.GetSection("ConnectionStrings:EuronextDb").Value ?? throw new ArgumentNullException(nameof(configuration));
		}

		public async Task<IEnumerable<WeatherForecast>> GetWeeklyForecastAsync(DateTime startDate)
		{
			
			var rng = new Random();
			var forecasts = Enumerable.Range(1, 7).Select(index =>
			{
				var temp = rng.Next(-60, 61);
				string result = temp switch
				{
					<= -40 => "Freezing",
					> -40 and <= -30 => "Bracing",
					> -30 and <= -20 => "Chilly",
					> -20 and <= -10 => "Cool",
					> -10 and <= 0 => "Mild",
					> 0 and <= 10 => "Warm",
					> 10 and <= 20 => "Balmy",
					> 20 and <= 30 => "Hot",
					> 30 and <= 40 => "Sweltering",
					> 40 => "Scorching"
				};
				return new WeatherForecast
				{
					Date = startDate.AddDays(index),
					Temp = temp,
					Result = result
				};
			});
			return await Task.FromResult(forecasts);
		}


		public async Task AddForecastAsync(WeatherForecast forecast)
		{

			using (var connection = new NpgsqlConnection(_connectionString))
			{
				await connection.OpenAsync();

				using (var command = new NpgsqlCommand("INSERT INTO weather_forecasts (date, temp, result) VALUES (@date, @temp, @result)", connection))
				{
					command.Parameters.AddWithValue("@date", forecast.Date);
					command.Parameters.AddWithValue("@temp", forecast.Temp);
					command.Parameters.AddWithValue("@result", forecast.Result);

					await command.ExecuteNonQueryAsync();
				}
			}
		}
	}
}
