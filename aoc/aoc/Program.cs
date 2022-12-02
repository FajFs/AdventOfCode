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
serviceCollection.AddTransient<DayTwo>();
serviceCollection.AddTransient<DayThree>();
//serviceCollection.AddTransient<DayFour>();
//serviceCollection.AddTransient<DayFive>();
//serviceCollection.AddTransient<DaySix>();
//serviceCollection.AddTransient<DaySeven>();
//serviceCollection.AddTransient<DayEight>();
//serviceCollection.AddTransient<DayNine>();
//serviceCollection.AddTransient<DayTen>();
//serviceCollection.AddTransient<DayEleven>();
//serviceCollection.AddTransient<DayTwelve>();
//serviceCollection.AddTransient<DayThirteen>();
//serviceCollection.AddTransient<DayFourteen>();
//serviceCollection.AddTransient<DayFifteen>();
//serviceCollection.AddTransient<DaySixteen>();
//serviceCollection.AddTransient<DaySeventeen>();
//serviceCollection.AddTransient<DayEighteen>();
//serviceCollection.AddTransient<DayNineteen>();
//serviceCollection.AddTransient<DayTwenty>();
//serviceCollection.AddTransient<DayTwentyOne>();
//serviceCollection.AddTransient<DayTwentyTwo>();
//serviceCollection.AddTransient<DayTwentyThree>();
//serviceCollection.AddTransient<DayTwentyFour>();
//serviceCollection.AddTransient<DayTwentyFive>();

var services = serviceCollection.BuildServiceProvider();

await new AocRunner().Run(services, DateTime.Now.Day);



