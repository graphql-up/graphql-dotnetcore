using System;
using Microsoft.AspNetCore.Http;

namespace Market.Infrastructure
{
    public class GraphQLSettings
    {
        public PathString Path { get; set; } = "/api/graphql";
        public Func<HttpContext, object> BuildUserContext { get; set; }
        public bool EnableMetrics { get; set; }
    }
}
