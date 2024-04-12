using euro.next.Application;
using euro.next.Domain;
using euro.next.Infrastructure;
using Microsoft.Extensions.Configuration;
using Moq;
using Npgsql;
using Xunit;


namespace euro.next.test
{
	public class WeatherRepositoryTest
	{

		private Mock<IConfiguration> CreateConfigurationMock()
		{
			var connectionString = "Host=127.0.0.1;Port=5432;Database=euronextdb;Username=euronextuser;Password=euronext123;SslMode=Disable;";
			var mockConfigSection = new Mock<IConfigurationSection>();
			mockConfigSection.SetupGet(x => x.Value).Returns(connectionString);

			var mockConfiguration = new Mock<IConfiguration>();
			mockConfiguration.Setup(x => x.GetSection(It.Is<string>(s => s == "ConnectionStrings:EuronextDb")))
							 .Returns(mockConfigSection.Object);

			return mockConfiguration;
		}





		[Fact]
		public async Task Happy_GetWeeklyForecastAsync_ShouldReturnForecasts_WhenStartDateIsInFuture()
		{
			// Arrange
			var startDate = DateTime.Today.AddDays(1); // Start date is in the future
			var mockConfiguration = CreateConfigurationMock();
			var repository = new WeatherRepository(mockConfiguration.Object);

			// Act
			var forecasts = await repository.GetWeeklyForecastAsync(startDate);

			// Assert
			Assert.NotNull(forecasts);
			Assert.All(forecasts, forecast => Assert.True(forecast.Date >= startDate));
		}

		[Fact]
		public async Task Happy_GetWeeklyForecastAsync_ShouldReturnValidDescriptions_WhenCalled()
		{
			// Arrange
			var startDate = DateTime.Today.AddDays(1); // Start date is in the future
			var mockConfiguration = CreateConfigurationMock();
			var repository = new WeatherRepository(mockConfiguration.Object);

			// Act
			var forecasts = await repository.GetWeeklyForecastAsync(startDate);

			// Assert
			Assert.NotNull(forecasts);
			Assert.All(forecasts, forecast =>
			{
				Assert.Contains(forecast.Result, new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" });
			});
		}

		[Fact]
		public async Task UnHappy_AddForecastAsync_ShouldInsertForecast_WhenForecastIsValid()
		{
			// Arrange
			var forecast = new WeatherForecast { Date = DateTime.Today, Temp = 25, Result = "Warm" };
			var mockConfiguration = CreateConfigurationMock();
			var repository = new WeatherRepository(mockConfiguration.Object);

			// Act
			// Asserting that no exceptions are thrown during the insert
			Exception ex = await Record.ExceptionAsync(() => repository.AddForecastAsync(forecast));
			Assert.Null(ex);
		}



	}
}