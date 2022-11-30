using aoc22;
using Microsoft.Extensions.Configuration;
using System.Net;


public class DataFetcher
{
    private readonly Uri _uri;
    private readonly AocCookie _aocCookie;
    private readonly HttpClient _httpClient;
    public string? Data { get; private set; }

    public DataFetcher(IConfiguration configuration, AocCookie aocCookie, HttpClient httpClient)
    {
        _uri = new Uri(configuration.GetValue<string>("BaseUrl")!) ?? throw new ArgumentNullException(nameof(configuration));
        _aocCookie = aocCookie ?? throw new ArgumentNullException(nameof(aocCookie));
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }  


    public List<string> ParseDataAsStrings(string deliminator) => Data.Split($"{deliminator}").Where(x => !string.IsNullOrEmpty(x)).ToList();
    public List<int> ParseDataAsInts(string deliminator) => Data.Split($"{deliminator}").Where(x => !string.IsNullOrEmpty(x)).Select(x => int.Parse(x)).ToList();

    private void StoreData(int year, int day) => File.WriteAllText($"year{year}day{day}.txt", Data);
    private bool TryLoadDataFromFile(int year, int day)
    {
        try
        {
            Data = File.ReadAllText($"year{year}day{day}.txt");
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task GetAndStoreData(int year, int day, string part = "")
    {
        if (TryLoadDataFromFile(year, day)) return;
        var cookieContainer = new CookieContainer();
        using var handler = new HttpClientHandler() { CookieContainer = cookieContainer };
        cookieContainer.Add(_uri, new Cookie(_aocCookie.Name, _aocCookie.Value)) ;
        var res = await _httpClient.GetAsync(_uri + $"/{year}/day/{day}/input{part}");
        Data = await res.Content.ReadAsStringAsync();
        StoreData(year, day);
    }
    

    public void SetTestData(string testData)
    {
        Data = testData.Replace("\r", "");
    }
}

