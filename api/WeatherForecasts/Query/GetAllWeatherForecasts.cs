using Core.Common;
using MediatR;

namespace Api.WeatherForecasts.Query;

public record GetAllWeatherForecasts : IQuery<WeatherForecast[]>;

public record GetWeatherForecast(int Id) : IQuery<WeatherForecast>;

public static class Helper
{
    private static readonly string[] Summaries = { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };

    public static WeatherForecast Create(int index)
    {
        return new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        };
    }
}

public class GetAllWeatherForecastsHandler : IRequestHandler<GetAllWeatherForecasts, WeatherForecast[]>
{
    public Task<WeatherForecast[]> Handle(GetAllWeatherForecasts request, CancellationToken cancellationToken)
    {
        return Task.FromResult(Enumerable.Range(1, 5).Select(Helper.Create).ToArray());
    }
}

public class GetWeatherForecastHandler : IRequestHandler<GetWeatherForecast, WeatherForecast>
{
    public Task<WeatherForecast> Handle(GetWeatherForecast request, CancellationToken cancellationToken)
    {
        return Task.FromResult(Helper.Create(request.Id));
    }
}