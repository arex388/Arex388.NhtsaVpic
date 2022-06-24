using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace Arex388.NhtsaVpic.Extensions.Microsoft.DependencyInjection {
    public static class ServiceCollectionExtensions {
        /// <summary>
        /// Add NHTSA vPIC API client as a singleton.
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="debug">Toggle response debugging. Disabled by default.</param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddNhtsaVpic(
            this IServiceCollection services,
            bool debug = false) {
            services.AddHttpClient<NhtsaVpicClient>(nameof(NhtsaVpicClient));

            return services.AddSingleton<INhtsaVpicClient>(
                _ => {
                    var httpClientFactory = _.GetRequiredService<IHttpClientFactory>();
                    var httpClient = httpClientFactory.CreateClient(nameof(NhtsaVpicClient));

                    return new NhtsaVpicClient(httpClient, debug);
                });
        }
    }
}