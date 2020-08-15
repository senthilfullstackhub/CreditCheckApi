namespace CreditCheck
{
    using CreditCheck.Data;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.EntityFrameworkCore;
    using CreditCheck.EFCore;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<AppDbContext>(o =>
                   o.UseLazyLoadingProxies()
                   .UseSqlServer(Configuration.GetConnectionString("MyCardDatabase")), ServiceLifetime.Scoped);

            services.AddScoped<EfCoreCustomerRepository>();
            services.AddScoped<EfCoreCardRepository>();
            services.AddControllers()
                        .ConfigureApiBehaviorOptions(options =>
                        {
                            options.SuppressConsumesConstraintForFormFileParameters = true;
                            options.SuppressInferBindingSourcesForParameters = true;
                            options.SuppressModelStateInvalidFilter = true;
                            options.SuppressMapClientErrors = true;
                            options.ClientErrorMapping[StatusCodes.Status404NotFound].Link =
                                "https://httpstatuses.com/404";
                        });
            services.AddControllersWithViews()
                    .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AppDbContext db)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            // MyCardsDb will be created if not exists
            db.Database.EnsureCreated();
            
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
