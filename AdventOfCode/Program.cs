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
        var year = int.Parse(conf.GetSection("Configuration:Year").Value);
        DataFetcher.Init(url, name, val);

        System.Console.WriteLine($"\n\n########### DAY {day} ############");
        new AOCRunner().Run(year, day);
        System.Console.WriteLine($"##############################\n\n");

    }

    public static void Main(string[] args)
    {
        Configure();
    }
}

