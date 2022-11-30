using aoc22;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddUserSecrets<AocCookie>()
    .Build();

var serviceCollection = new ServiceCollection();
var cookie = configuration.GetSection(nameof(AocCookie)).Get<AocCookie>()!;
Uri baseAddress = new("https://adventofcode.com/");
serviceCollection.AddHttpClient("DataFetcherClient")
    .ConfigureHttpClient(client =>
    {
        client.BaseAddress = baseAddress;
    })
    .ConfigurePrimaryHttpMessageHandler(
        () => 
        {
            var cookieContainer = new CookieContainer();
            cookieContainer.Add(baseAddress, new Cookie(cookie.Name, cookie.Value));
            return new HttpClientHandler() { CookieContainer = cookieContainer };
        });
serviceCollection.AddScoped<DataFetcher>();
serviceCollection.AddTransient<DayOne>();
var services = serviceCollection.BuildServiceProvider();

await new AocRunner().Run(services, DateTime.Now.Day);



