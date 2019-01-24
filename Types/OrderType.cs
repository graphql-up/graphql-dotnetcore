using GraphQL.DataLoader;
using GraphQL.Types;
using Market.Models;
using Market.Repositories;
using System.Collections.Generic;

namespace Market.Types
{
    public class OrderType : ObjectGraphType<Order>
    {
        public OrderType(IUser user, IProduct product, IDataLoaderContextAccessor accessor)
        {
            Name = "Order";
            Description = "Order fields";

            Field(x => x.OrderId).Description("Order Id");
            Field(x => x.Quantity).Description("Product quantity");           

            Field<UserType, User>()
                    .Name("User")
                    .Description("the user who places the order")
                    .ResolveAsync(context =>
                    {
                        return user.GetByIdAsync(context.Source.UserId);
                    });

            Field<ListGraphType<UserType>, IEnumerable<User>>()
             .Name("Users")
             .Description("the user who places the order")
             .ResolveAsync(ctx =>
             {
                 var ordersLoader = accessor.Context.GetOrAddCollectionBatchLoader<int, User>("GetReviewsByUserId", user.GetByIdAsync);
                 return ordersLoader.LoadAsync(ctx.Source.UserId);
             });

            Field<ProductType>("Product",
                               Description = "Ordered product",
                                 resolve: context =>
                                 {
                                     return product.GetByIdAsync(context.Source.ProductId);
                                 });
        }
    }
}
