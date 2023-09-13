using System.Diagnostics;

namespace _1_Blog_CQRS_Less.Helpers
{
    public class PerfHelper
    {
        private readonly ILogger<PerfHelper> _logger;

        public PerfHelper(ILogger<PerfHelper> logger)
        {
            _logger = logger;
        }

        public T MeasurePerformances<T>(Func<T> func)
        {
            Stopwatch sw = Stopwatch.StartNew();
            try
            {
                return func();
            }
            finally
            {
                sw.Stop();
                if (sw.ElapsedMilliseconds > 500)
                    _logger.LogWarning("{methodName} took {ms}ms", func.Method.Name, sw.ElapsedMilliseconds);
            }
        }
    }
}
