using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Market.Types
{
    public class UserInputType : InputObjectGraphType
    {
        public UserInputType()
        {
            Name = "UserInputType";
            Field<NonNullGraphType<IntGraphType>>("UserId");
            Field<NonNullGraphType<StringGraphType>>("FullName");
        }
    }
}
