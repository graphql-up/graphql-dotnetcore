using GraphQL;
using GraphQL.Types;
using System;

namespace Market.Inventory
{
    public class InventorySchema : Schema
    {      
        public InventorySchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<InventoryQuery>();
            Mutation = resolver.Resolve<InventoryMutation>();
        }
    }
}
