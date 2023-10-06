using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Net;

namespace AdventOfCode.Common;
public static class AdventOfCodeExtensions
{
    public static IServiceCollection AddAdventOfCodeCommon(this IServiceCollection services)
    {
        // Logging serilog to console
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .CreateLogger();

        var configuration = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddUserSecrets<AocCookie>()
             .Build();

        var cookie = configuration.GetSection(nameof(AocCookie))
            .Get<AocCookie>()!;

        Uri baseAddress = new("https://adventofcode.com/");
        services.AddHttpClient<IInputRepository, InputRepository>("DataFetcherClient")
            .ConfigureHttpClient(client =>
            {
                client.BaseAddress = baseAddress;
            })
            .ConfigurePrimaryHttpMessageHandler(() =>
            {
                var cookieContainer = new CookieContainer();
                cookieContainer.Add(baseAddress, new System.Net.Cookie(cookie.Name, cookie.Value));
                return new HttpClientHandler() { CookieContainer = cookieContainer };
            });

        return services;
    }

    public static IServiceCollection AddRangeTransient<TService>(this IServiceCollection services)
    {
        foreach (var dayService in typeof(TService).Assembly.GetTypes()
            .Where(x => typeof(TService).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract))
                services.AddTransient(dayService);

        return services;
    }
}
