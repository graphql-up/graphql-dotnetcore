using GraphQL.DataLoader;
using GraphQL.Types;
using Market.Models;
using Market.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Market.Types
{
    public class ReviewType : ObjectGraphType<Review>
    {
        public ReviewType(IUser user, IProduct product, IDataLoaderContextAccessor accessor)
        {
            Name = "Review";
            Description = "Review fields";

            Field(x => x.ReviewId).Description("Review Id");
            Field(x => x.Text).Description("Review text");
            Field(x => x.Star).Description("Review star");

            Field<UserType>("User",
                               Description = "User's review",
                                  resolve: context =>
                                  {
                                      return user.GetByIdAsync(context.Source.UserId);
                                  });
            Field<ProductType>("Product",
                               Description = "Reviewed product",
                                 resolve: context =>
                                 {
                                     return product.GetByIdAsync(context.Source.ProductId);
                                 });
        }
    }
}
