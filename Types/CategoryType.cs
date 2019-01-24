using GraphQL.DataLoader;
using GraphQL.Types;
using Market.Models;
using Market.Repositories;
using System.Collections.Generic;

namespace Market.Types
{
    public class CategoryType : ObjectGraphType<Category>
    {
        public CategoryType(IProduct product, IDataLoaderContextAccessor accessor)
        {
            Name = "Category";
            Description = "Product Category";

            Field(x => x.CategoryId).Description("Category Id");
            Field(x => x.Name).Description("Category name");

            Field<ProductType, Product>()
            .Name("Products")
            .ResolveAsync(context =>
            {
                IDataLoader<int, Product> loader = accessor.Context.GetOrAddBatchLoader<int, Product>("GetProductsByCategoryId",
                    async (IEnumerable<int> ids) => { return await product.GetProductsByCategoryIdAsync(ids); });
                return loader.LoadAsync(context.Source.CategoryId);
            });            
        }
    }
}
