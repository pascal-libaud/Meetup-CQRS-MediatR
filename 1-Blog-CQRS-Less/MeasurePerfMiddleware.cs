using _1_Blog_CQRS_Less.Helpers;

namespace _1_Blog_CQRS_Less;

public class MeasurePerfMiddleware
{
    private readonly RequestDelegate _next;
    private readonly PerfHelper _perfHelper;

    public MeasurePerfMiddleware(RequestDelegate next, PerfHelper perfHelper)
    {
        _next = next;
        _perfHelper = perfHelper;
    }

    public async Task Invoke(HttpContext context)
    {
        await _perfHelper.MeasurePerformances(async () =>
        {
            await _next(context);
        });
    }

}

public static class MeasurePerfMiddlewareExtensions
{
    public static IApplicationBuilder UseMeasurePerf(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<MeasurePerfMiddleware>();
    }
}