using BookBingoApi.Services;
using BookBingoApi.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BookBingoApi.AutoMapper;
using AutoMapper;
using System;
using BookBingoApi.Repository;
using BookBingoApi.Repository.Cosmos;

namespace BookBingoApi
{
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
            var goodreadsUri = new Uri("https://www.goodreads.com");

            services.Configure<GoodreadsOptions>(Configuration.GetSection("Goodreads"));
            services.Configure<CosmosOptions>(Configuration.GetSection("Cosmos"));
            services.AddAutoMapper(typeof(GoodreadsProfile));
            services.AddSingleton<IOAuthTokenRepository, CosmosOAuthTokenRepository>();
            services.AddHttpClient<IBooksService, GoodreadsService>(client => client.BaseAddress = goodreadsUri);
            services.AddHttpClient<IOAuthService, GoodReadsOAuthService>(client =>
                {
                    client.DefaultRequestHeaders.Host = goodreadsUri.Host;
                    client.BaseAddress = goodreadsUri;
                }
            );
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler("/error");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(policy => policy
                .WithOrigins("http://localhost:3000")
                .AllowCredentials()
            );

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
