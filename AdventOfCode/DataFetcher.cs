using System;
using System.Collections.Generic;
using System.IO;
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

    public static List<string> ParseDataAsStrings(string deliminator) => data.Split($"{deliminator}").Where(x => !string.IsNullOrEmpty(x)).ToList();
    public static List<int> ParseDataAsInts(string deliminator) => data.Split($"{deliminator}").Where(x => !string.IsNullOrEmpty(x)).Select(x => int.Parse(x)).ToList();

    private static void StoreData(int year, int day) => File.WriteAllText($"year{year}day{day}.txt", data);
    private static bool TryLoadDataFromFile(int year, int day)
    {
        try
        {
            data = File.ReadAllText($"year{year}day{day}.txt");
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public static void GetAndStoreData(int year, int day, string part = "")
    {
        if (TryLoadDataFromFile(year, day)) return;
        var baseAdress = new Uri(_baseUrl);
        var cookieContainer = new CookieContainer();
        using var handler = new HttpClientHandler() { CookieContainer = cookieContainer };
        using var client = new HttpClient(handler) {BaseAddress = baseAdress };

        cookieContainer.Add(baseAdress, new Cookie(_cookieName, _cookieVal)) ;
        var res = client.GetAsync($"/{year}/day/{day}/input{part}").GetAwaiter().GetResult();
        data = res.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        StoreData(year, day);
    }


    public static void SetTestData(string testData)
    {
        data = testData;
    }
}

