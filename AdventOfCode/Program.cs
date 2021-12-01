using AdventOfCode._2021;
using Microsoft.Extensions.Configuration;
using System;
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
        DataFetcher.Init(url, name, val);
    }

    static void Main(string[] args)
    {
        Configure();
        new AOCRunner().Run();
    }
}

