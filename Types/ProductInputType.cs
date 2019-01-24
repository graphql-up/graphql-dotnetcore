using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Market.Types
{
    public class ProductInputType : InputObjectGraphType
    {
        public ProductInputType()
        {
            Name = "ProductInputType";
            Field<IntGraphType>("ProductId");
            Field<NonNullGraphType<StringGraphType>>("Name");
            Field<NonNullGraphType<StringGraphType>>("Description");
            Field<NonNullGraphType<IntGraphType>>("Price");
            Field<NonNullGraphType<StringGraphType>>("ImagePath");
            Field<NonNullGraphType<IntGraphType>>("CategoryId");
        }
    }
}
