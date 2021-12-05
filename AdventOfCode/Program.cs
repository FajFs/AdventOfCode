using Microsoft.Extensions.Configuration;
using System.IO;


class Program
{
    public static void Configure() 
    {
        var conf = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false).Build();

        var url = conf.GetSection("Configuration:BaseURL").Value;
        var name = conf.GetSection("Configuration:CookieName").Value;
        var val = conf.GetSection("Configuration:CookieValue").Value;
        var day = int.Parse(conf.GetSection("Configuration:Day").Value);
        DataFetcher.Init(url, name, val);
        new AOCRunner(day).Run();
    }

    public static void Main(string[] args)
    {
        Configure();
    }
}

