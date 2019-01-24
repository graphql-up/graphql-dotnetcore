using GraphQL.Types;

namespace Market.Types
{
    public class CategoryInputType : InputObjectGraphType
    {
        public CategoryInputType()
        {
            Name = "CategoryInputType";
            Field<NonNullGraphType<IntGraphType>>("CategoryId");
            Field<NonNullGraphType<StringGraphType>>("Name");
        }
    }
}
