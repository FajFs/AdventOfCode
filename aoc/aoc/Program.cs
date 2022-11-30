using aoc22;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using Microsoft.Extensions.Configuration.UserSecrets;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddUserSecrets<AocCookie>()
    .Build();



var serviceCollection = new ServiceCollection();

serviceCollection.AddSingleton(configuration.GetSection(nameof(AocCookie)).Get<AocCookie>()!);
serviceCollection.AddSingleton<IConfiguration>(configuration);
serviceCollection.AddHttpClient();
serviceCollection.AddScoped<DataFetcher>();

serviceCollection.AddTransient<DayOne>();

var services = serviceCollection.BuildServiceProvider();


using var scope = services.CreateScope();
await scope.ServiceProvider.GetService<DayOne>()!.Run();

