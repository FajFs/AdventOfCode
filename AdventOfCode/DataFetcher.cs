using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


static class DataFetcher
{
    private static string data;
    public static List<string> ParseDataAsStrings(string deliminator) => data.Split($"{deliminator}").ToList();
    public static List<int> ParseDataAsInts(string deliminator)
    {
        var x = 0;
        return data.Split($"{deliminator}").Where(d => int.TryParse(d, out x)).Select(d => x).ToList();  
    }

    public static void GetAndStoreData(int year, int day, string part = "")
    {
        var baseAdress = new Uri("https://adventofcode.com");
        var cookieContainer = new CookieContainer();
        using var handler = new HttpClientHandler() { CookieContainer = cookieContainer };
        using var client = new HttpClient(handler) {BaseAddress = baseAdress };

        cookieContainer.Add(baseAdress, new Cookie("session", "53616c7465645f5f6b0272abc48e7abee05b26713c0163b4807bcba0e4253fbb3383c629e58172f792c3f8b808e98c58")) ;
        var res = client.GetAsync($"/{year}/day/{day}/input{part}").GetAwaiter().GetResult();
        data = res.Content.ReadAsStringAsync().GetAwaiter().GetResult();
    }
}

