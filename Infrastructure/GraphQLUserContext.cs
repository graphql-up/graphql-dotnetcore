using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Market.Infrastructure
{
    public class GraphQLUserContext
    {
        public ClaimsPrincipal User { get; set; }
    }
}
