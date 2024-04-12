using euro.next.Application;
using euro.next.Domain;
using Microsoft.AspNetCore.Mvc;

namespace euro.next.API
{
	[ApiController]
	[Route("[controller]")]
	public class WeatherController : ControllerBase
	{
		private readonly IWeatherService _weatherService;

		public WeatherController(IWeatherService weatherService)
		{
			_weatherService = weatherService;
		}

		[HttpGet("weekly")]
		public async Task<IActionResult> GetWeeklyForecast(DateTime startDate)
		{
			// Validate startDate is not in the past
			if (startDate < DateTime.Today)
				return BadRequest("Input forecasts cannot be in the past.");
			try
			{
				var forecast = await _weatherService.GetWeeklyForecastAsync(startDate);
				return Ok(forecast);
			}
			catch (Exception ex)
			{
				return StatusCode(500, "An error occurred while retrieving forecast model.");
			}
		}


		[HttpPost("add")]
		public async Task<IActionResult> AddForecast([FromBody] WeatherForecast forecast)
		{
			try
			{
				await _weatherService.AddForecastAsync(forecast);
				return Ok();
			}
			catch (Exception ex)
			{
				return StatusCode(500, "An error occurred while adding the forecast into db.");
			}

		}
	}
}
