using GlobalInsightsApi_Assessment.Models;
using GlobalInsightsApi_Assessment.Models.Aggregation;
using System.Collections.Concurrent;
using System.Threading;

namespace GlobalInsightsApi_Assessment.Services
{
    public class StatisticsService : IStatisticsService
    {
        private class Stats
        {
            public int SuccessfulCalls;
            public int FailedCalls;
            public long TotalResponseTime;
            public int TotalCalls;
        }

        // Concurrent dictionary: key = "weather"|"news"|"github"
        private readonly ConcurrentDictionary<string, Stats> _stats
            = new(StringComparer.OrdinalIgnoreCase);

        public void Record(string apiName, long responseTimeMs, bool isSuccess)
        {
            var s = _stats.GetOrAdd(apiName, _ => new Stats());

            Interlocked.Increment(ref s.TotalCalls);
            if (isSuccess)
            {
                Interlocked.Increment(ref s.SuccessfulCalls);
                Interlocked.Add(ref s.TotalResponseTime, responseTimeMs);
            }
            else
            {
                Interlocked.Increment(ref s.FailedCalls);
            }
        }

        public ApiStatsDto GetStats(string apiName)
        {
            if (!_stats.TryGetValue(apiName, out var s))
            {
                return new ApiStatsDto
                {
                    ApiName = apiName,
                    SuccessfulCalls = 0,
                    FailedCalls = 0,
                    AverageResponseTime = 0
                };
            }

            return new ApiStatsDto
            {
                ApiName = apiName,
                SuccessfulCalls = s.SuccessfulCalls,
                FailedCalls = s.FailedCalls,
                AverageResponseTime = s.SuccessfulCalls == 0 ? 0 : (double)s.TotalResponseTime / s.SuccessfulCalls
            };
        }
    }
}
