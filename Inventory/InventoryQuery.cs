using GraphQL.DataLoader;
using GraphQL.Types;
using Market.Models;
using Market.Repositories;
using Market.Types;
using System.Collections.Generic;

namespace Market.Inventory
{
    public class InventoryQuery : ObjectGraphType<object>
    {
        public InventoryQuery(IDataLoaderContextAccessor accessor, ICategory category, IProduct product, IReview review, IUser user, IOrder order)
        {
            Name = "MarketAppQuery";

            #region Category
            Field<CategoryType, Category>()
             .Name("CategoryById")
             .Description("This field returns the category of the submitted id")
             .Argument<NonNullGraphType<IntGraphType>>(Name = "CategoryId", Description = "Category Id")
             .ResolveAsync(ctx =>
             {
                 return category.GetByIdAsync(ctx.GetArgument<int>("CategoryId"));
             });           

            Field<ListGraphType<CategoryType>, IEnumerable<Category>>()
                .Name("getAllCategories")
                .Description("This field returns all categories")
                .ResolveAsync(ctx =>
                {
                    IDataLoader<IEnumerable<Category>> loader = accessor.Context.GetOrAddLoader("GetAllCategories", () => category.GetAllAsync());
                    return loader.LoadAsync();
                });

            #endregion

            #region Product
            Field<ProductType>(
                    "getProductById",
                    Description = "This field returns the product of the submitted id",
                    arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "ProductId" }
                    ),
            resolve: context => product.GetByIdAsync(context.GetArgument<int>("ProductId"))
                );

            Field<ListGraphType<ProductType>, IEnumerable<Product>>()
                .Name("getAllProducts")
                .Description("This field returns all products")
                .ResolveAsync(ctx =>
                {
                    var loader = accessor.Context.GetOrAddLoader("GetAllProducts", () => product.GetAllAsync());
                    return loader.LoadAsync();
                });           
            #endregion

            #region Review
            Field<ReviewType>(
                    "getReviewById",
                    Description = "This field returns the review of the submitted id",
                    arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "ReviewId" }
                    ),
                resolve: context => review.GetByIdAsync(context.GetArgument<int>("ReviewId"))
                );

            Field<ListGraphType<ReviewType>, IEnumerable<Review>>()
                .Name("getAllReviews")
                .Description("This field returns all reviews")
                .ResolveAsync(ctx =>
                {
                    var loader = accessor.Context.GetOrAddLoader("GetAllReviews", () => review.GetAllAsync());
                    return loader.LoadAsync();
                });          
            #endregion

            #region User
            Field<UserType>(
                    "getUserById",
                    Description = "This field returns the user of the submitted id",
                    arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "UserId" }
                    ),
                resolve: context => user.GetByIdAsync(context.GetArgument<int>("UserId"))
                );

            Field<ListGraphType<UserType>, IEnumerable<User>>()
              .Name("getAllUsers")
              .Description("This field returns all reviews")
              .ResolveAsync(ctx =>
              {
                  var loader = accessor.Context.GetOrAddLoader("GetAllUsers", () => user.GetAllAsync());
                  return loader.LoadAsync();
              });         
            #endregion

            #region Order
            Field<OrderType>(
                    "getOrderById",
                    Description = "This field returns the order of the submitted id",
                    arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "OrderId" }
                    ),
                resolve: context => order.GetByIdAsync(context.GetArgument<int>("OrderId"))
                );

            Field<ListGraphType<OrderType>, IEnumerable<Order>>()
                .Name("getAllOrders")
                .Description("This field returns all orders")
                .ResolveAsync(ctx =>
                {
                    var loader = accessor.Context.GetOrAddLoader("GetAllOrders", () => order.GetAllAsync());
                    return loader.LoadAsync();
                });

            #endregion
            Description = "MarketApp Query Fields for, You can query about categories, products, users, reviews and orders";

        }

        public IDataLoader<IEnumerable<Category>> loader { get; private set; }
    }
}
