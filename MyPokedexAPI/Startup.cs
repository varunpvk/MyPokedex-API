namespace MyPokedexAPI
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;
    using MyPokedex.API;
    using MyPokedex.ApplicationServices.Features;
    using MyPokedex.Infrastructure.FunTranslationsClient;
    using MyPokedex.Infrastructure.FunTranslationsClient.Config;
    using MyPokedex.Infrastructure.PokeAPIClient;
    using MyPokedex.Infrastructure.PokeAPIClient.Config;
    using System;
    using System.Net;
    using System.Net.Http;

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
            services.AddMvcCore().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddControllers(options => options.Filters.Add(new HttpResponseExceptionFilter()));
            services.AddSwaggerGen(o => {
                o.SwaggerDoc("v1", new OpenApiInfo {
                    Title = "My Pokedex API",
                    Version = "1.0.0"
                });
            });
            services.AddSingleton<IPokeApiClientConfig, PokeApiClientConfig>();
            services.AddSingleton<ITranslationsClientConfig, TranslationsClientConfig>();
            services.AddHttpClient<IPokeService, PokeService>()
                .ConfigureHttpClient((serviceProvider, httpClient) => {
                    var clientConfig = serviceProvider.GetRequiredService<IPokeApiClientConfig>();
                    httpClient.BaseAddress = clientConfig.BaseUri;
                    httpClient.Timeout = TimeSpan.FromSeconds(clientConfig.Timeout);
                    httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                })
                .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                .ConfigurePrimaryHttpMessageHandler(x =>
                new HttpClientHandler {
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                    UseCookies = false,
                    AllowAutoRedirect = false,
                    UseDefaultCredentials = true,
                });

            services.AddHttpClient<ITranslationsService, TranslationsService>()
                .ConfigureHttpClient((serviceProvider, httpClient) => {
                    var clientConfig = serviceProvider.GetRequiredService<ITranslationsClientConfig>();
                    httpClient.BaseAddress = clientConfig.BaseUri;
                    httpClient.Timeout = TimeSpan.FromSeconds(clientConfig.Timeout);
                    httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                })
                .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                .ConfigurePrimaryHttpMessageHandler(x =>
                new HttpClientHandler {
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                    UseCookies = false,
                    AllowAutoRedirect = false,
                    UseDefaultCredentials = true,
                });

            services.AddScoped<IBasicPokemonFeature, BasicPokemonFeature>();
            services.AddScoped<ITranslatedPokemonFeature, TranslatedPokemonFeature>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) {
                app.UseExceptionHandler("/error-local-development");
            }
            else {
                app.UseExceptionHandler("/error");
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(o => {
                o.SwaggerEndpoint("/swagger/v1/swagger.json", "My Pokedex API");
            });
        }
    }
}
