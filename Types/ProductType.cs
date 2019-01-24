using GraphQL.DataLoader;
using GraphQL.Types;
using Market.Models;
using Market.Repositories;
using System.Collections.Generic;

namespace Market.Types
{
    public class ProductType : ObjectGraphType<Product>
    {
        public ProductType(ICategory category, IReview review, IOrder order, IDataLoaderContextAccessor accessor)
        {
            Name = "Product";
            Description = "Product fields";

            Field(x => x.ProductId).Description("Product Id");
            Field(x => x.Name).Description("Product name");
            Field(x => x.Description).Description("Product description");
            Field(x => x.Price).Description("Product price");
            Field(x => x.ImagePath).Description("Product image path");

            Field<ProductType>("Category",
                               Description = "Category of product",
                                  resolve: context =>
                                  {
                                      return category.GetByIdAsync(context.Source.CategoryId);
                                  });
           

            Field<ListGraphType<ReviewType>, Review>()
               .Name("Reviews")
               .Description("Reviews of product")
               .ResolveAsync(context =>
               {
                   var loader = accessor.Context.GetOrAddBatchLoader<int, Review>("GetReviewsByProductId", async (IEnumerable<int> ids) => { return await review.GetReviewsByProductIdAsync(ids); });
                   return loader.LoadAsync(context.Source.ProductId);
                   //var loader = accessor.Context.GetOrAddBatchLoader<int, Review>("GetReviewsByProductId", review.GetReviewsByProductIdAsync);
                   //return loader.LoadAsync(context.Source.ProductId);
               });

            Field<OrderType, Order>()
            .Name("Orders")
            .Description("Orders of product")
            .ResolveAsync(context =>
            {
                var loader = accessor.Context.GetOrAddBatchLoader<int, Order>("GetOrdersByProductId", async (IEnumerable<int> ids) => { return await order.GetOrdersByProductIdAsync(ids); });
                return loader.LoadAsync(context.Source.ProductId);
                //var loader = accessor.Context.GetOrAddBatchLoader<int, Order>("GetOrdersByProductId", order.GetOrdersByProductIdAsync);
                //return loader.LoadAsync(context.Source.ProductId);
            });

        }
    }
}
