using GraphQL.Types;

namespace Market.Types
{
    public class OrderInputType : InputObjectGraphType
    {
        public OrderInputType()
        {
            Name = "OrderInputType";
            Field<IntGraphType>("OrderId");
            Field<NonNullGraphType<IntGraphType>>("Quantity");
            Field<NonNullGraphType<IntGraphType>>("ProductId");
            Field<NonNullGraphType<IntGraphType>>("UserId");
        }
    }
}
