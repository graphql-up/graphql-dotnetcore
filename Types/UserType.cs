using GraphQL.DataLoader;
using GraphQL.Types;
using Market.Models;
using Market.Repositories;
using System.Collections.Generic;

namespace Market.Types
{
    public class UserType : ObjectGraphType<User>
    {
        public UserType(IReview review, IOrder order, IDataLoaderContextAccessor accessor)
        {
            Name = "User";
            Description = "User fields";

            Field(x => x.UserId).Description("User Id");
            Field(x => x.FullName).Description("User name and surname");

            Field<ListGraphType<ReviewType>, IEnumerable<Review>>()
                .Name("Reviews")
                .Description("User reviews")
                .ResolveAsync(ctx =>
                {
                    var ordersLoader = accessor.Context.GetOrAddCollectionBatchLoader<int, Review>("GetReviewsByUserId", review.GetReviewsByUserIdAsync);
                    return ordersLoader.LoadAsync(ctx.Source.UserId);
                });

            Field<ListGraphType<OrderType>, IEnumerable<Order>>()
                .Name("Orders")
                .Description("User orders")
                .ResolveAsync(ctx =>
                {
                    var ordersLoader = accessor.Context.GetOrAddCollectionBatchLoader<int, Order>("GetOrdersByUserId", order.GetOrdersByUserIdAsync);

                    return ordersLoader.LoadAsync(ctx.Source.UserId);
                });                      
        }
    }
}
