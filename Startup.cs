using GraphQL;
using GraphQL.DataLoader;
using GraphQL.Http;
using GraphQL.Types;
using Market.Controllers;
using Market.Data;
using Market.Infrastructure;
using Market.Inventory;
using Market.Repositories;
using Market.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Market
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IHostingEnvironment HostingEnvironment { get; }
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                   .SetBasePath(env.ContentRootPath)
                   .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                   .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);         

            builder.AddEnvironmentVariables();
            HostingEnvironment = env;
            Configuration = builder.Build();
        }
       
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));

            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<IDocumentWriter, DocumentWriter>();

            services.AddSingleton<IDataLoaderContextAccessor, DataLoaderContextAccessor>();
            services.AddSingleton<DataLoaderDocumentListener>();

            services.AddScoped<InventoryQuery>();
            services.AddScoped<InventoryMutation>();
            services.AddScoped<ISchema, InventorySchema>();

            services.AddScoped<CategoryType>();
            services.AddScoped<ProductType>();
            services.AddScoped<OrderType>();
            services.AddScoped<UserType>();
            services.AddScoped<ReviewType>();
            services.AddScoped<CategoryInputType>();
            services.AddScoped<UserInputType>();
            services.AddScoped<ReviewInputType>();
            services.AddScoped<ProductInputType>();
            services.AddScoped<OrderInputType>();

            services.AddScoped<ICategory, CategoryRepository>();
            services.AddScoped<IProduct, ProductRepository>();
            services.AddScoped<IReview, ReviewRepository>();
            services.AddScoped<IUser, UserRepository>();
            services.AddScoped<IOrder, OrderRepository>();

            services.AddCors(options => options.AddPolicy("AllowAllOrigins", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

            services.AddEntityFrameworkNpgsql().AddDbContextPool<MarketContext>(
                options => options
                .UseNpgsql(GetExtraValue(Configuration), x => x.EnableRetryOnFailure())
                .ConfigureWarnings(x => x.Throw(RelationalEventId.QueryClientEvaluationWarning))
                .EnableSensitiveDataLogging(HostingEnvironment.IsDevelopment())
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
        }
        private string GetExtraValue(IConfiguration configuration)
        {
            return configuration.GetValue<string>(configuration.GetConnectionString("DefaultConnection"));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {         
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseCors("AllowAllOrigins");

            app.UseMiddleware<GraphQLMiddleware>(new GraphQLSettings { BuildUserContext = ctx => new GraphQLUserContext { User = ctx.User } });
            DbInitializer.Initialize(app);
        }
    }
}
