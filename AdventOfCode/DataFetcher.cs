using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


static class DataFetcher
{
    private static string _baseUrl;
    private static string _cookieName;
    private static string _cookieVal;

    private static string data;

    public static void Init(string baseUrl, string cookieName, string cookieVal)
    {
        _baseUrl = baseUrl;
        _cookieName = cookieName;
        _cookieVal = cookieVal;
    }

    public static List<string> ParseDataAsStrings(string deliminator) => data.Split($"{deliminator}").ToList().Where(d => !string.IsNullOrEmpty(d)).ToList();
    public static List<int> ParseDataAsInts(string deliminator)
    {
        var x = 0;
        return data.Split($"{deliminator}").Where(d => int.TryParse(d, out x)).Select(d => x).ToList();  
    }

    public static void GetAndStoreData(int year, int day, string part = "")
    {
        if (string.IsNullOrEmpty(_baseUrl)) return;
        var baseAdress = new Uri(_baseUrl);
        var cookieContainer = new CookieContainer();
        using var handler = new HttpClientHandler() { CookieContainer = cookieContainer };
        using var client = new HttpClient(handler) {BaseAddress = baseAdress };

        cookieContainer.Add(baseAdress, new Cookie(_cookieName, _cookieVal)) ;
        var res = client.GetAsync($"/{year}/day/{day}/input{part}").GetAwaiter().GetResult();
        data = res.Content.ReadAsStringAsync().GetAwaiter().GetResult();
    }

    public static void SetTestData(string testData)
    {
        data = testData;
    }
}

