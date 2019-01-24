using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Market.Types
{
    public class ReviewInputType : InputObjectGraphType
    {
        public ReviewInputType()
        {
            Name = "ReviewInputType";
            Field<IntGraphType>("ReviewId");
            Field<NonNullGraphType<StringGraphType>>("Text");
            Field<NonNullGraphType<IntGraphType>>("Star");
            Field<NonNullGraphType<IntGraphType>>("UserId");
            Field<NonNullGraphType<IntGraphType>>("ProductId");

        }
    }
}
