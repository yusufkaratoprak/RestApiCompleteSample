using euro.next.API;
using euro.next.Application;
using euro.next.Domain;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace euro.next.test
{
	public class WeatherControllerTest
	{
		private readonly Mock<IWeatherService> _mockWeatherService;
		private readonly WeatherController _controller;

		public WeatherControllerTest()
		{
			_mockWeatherService = new Mock<IWeatherService>();
			_controller = new WeatherController(_mockWeatherService.Object);
		}

		[Fact]
		public async Task Happy_GetWeeklyForecast_ReturnsOk_WhenStartDateIsValid()
		{
			// Arrange
			var startDate = DateTime.Today.AddDays(1);
			var mockForecast = new List<WeatherForecast>(); // Mock data to be returned
			_mockWeatherService.Setup(service => service.GetWeeklyForecastAsync(startDate))
							   .ReturnsAsync(mockForecast);

			// Act
			var result = await _controller.GetWeeklyForecast(startDate);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			Assert.Equal(200, okResult.StatusCode);
			Assert.Equal(mockForecast, okResult.Value);
		}

		[Fact]
		public async Task UnHappy_GetWeeklyForecast_ReturnsBadRequest_WhenStartDateIsInPast()
		{
			// Arrange
			var startDate = DateTime.Today.AddDays(-1);

			// Act
			var result = await _controller.GetWeeklyForecast(startDate);

			// Assert
			var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
			Assert.Equal(400, badRequestResult.StatusCode);
			Assert.Equal("Input forecasts cannot be in the past.", badRequestResult.Value);
		}

		[Fact]
		public async Task UnHappy_GetWeeklyForecast_ReturnsInternalServerError_WhenExceptionOccurs()
		{
			// Arrange
			var startDate = DateTime.Today.AddDays(1);
			_mockWeatherService.Setup(service => service.GetWeeklyForecastAsync(startDate))
							   .ThrowsAsync(new Exception("Test exception"));

			// Act
			var result = await _controller.GetWeeklyForecast(startDate);

			// Assert
			var objectResult = Assert.IsType<ObjectResult>(result);
			Assert.Equal(500, objectResult.StatusCode);
			Assert.Equal("An error occurred while retrieving forecast model.", objectResult.Value);
		}

		[Fact]
		public async Task Happy_AddForecast_ReturnsOk_WhenForecastIsAdded()
		{
			// Arrange
			var forecast = new WeatherForecast(); // A valid forecast object
			_mockWeatherService.Setup(service => service.AddForecastAsync(forecast))
							   .Returns(Task.CompletedTask);

			// Act
			var result = await _controller.AddForecast(forecast);

			// Assert
			var okResult = Assert.IsType<OkResult>(result);
			Assert.Equal(200, okResult.StatusCode);
		}

		[Fact]
		public async Task UnHappy_AddForecast_ReturnsOk_WhenForecastIsAdded()
		{
			// Arrange
			var forecast = new WeatherForecast(); // A valid forecast object
			_mockWeatherService.Setup(service => service.AddForecastAsync(forecast))
								.ThrowsAsync(new Exception("Test exception"));

			// Act
			var result = await _controller.AddForecast(forecast);

			// Assert
			var objectResult = Assert.IsType<ObjectResult>(result);
			Assert.Equal(500, objectResult.StatusCode);
			Assert.Equal("An error occurred while adding the forecast into db.", objectResult.Value);
		}
	}
}