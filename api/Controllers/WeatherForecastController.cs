using Api.WeatherForecasts.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IMediator _mediator;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet("all", Name = "GetWeatherForecast")]
    public Task<WeatherForecast[]> Get()
    {
        return _mediator.Send(new GetAllWeatherForecasts());
    }

    [HttpGet("one/{id}", Name = "GetWeatherForecastOne")]
    public Task<WeatherForecast> GetOne(int id)
    {
        return _mediator.Send(new GetWeatherForecast(id));
    }
}