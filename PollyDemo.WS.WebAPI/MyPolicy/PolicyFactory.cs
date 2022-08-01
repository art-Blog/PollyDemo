using System;
using Polly;
using Polly.RateLimit;

namespace PollyDemo.WS.WebAPI.MyPolicy
{
    public static class PolicyFactory
    {
        public static RateLimitPolicy RateLimitPolicy()
        {
            return Policy.RateLimit(3, TimeSpan.FromSeconds(1));
        }
    }
}