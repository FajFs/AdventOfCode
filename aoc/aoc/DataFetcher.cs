using aoc22;
using Microsoft.Extensions.Configuration;
using System.Net;


public class DataFetcher
{
    private readonly HttpClient _httpClient;
    public string? Data { get; private set; }

    public DataFetcher(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("DataFetcherClient") ?? throw new ArgumentNullException(nameof(httpClientFactory));
    }

    public List<string> ParseDataAsStrings(string deliminator) => Data.Split($"{deliminator}").Where(x => !string.IsNullOrEmpty(x)).Select(x => x.Trim()).ToList();
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
        var res = await _httpClient.GetAsync(new Uri($"{year}/day/{day}/input{part}", UriKind.Relative));
        Data = await res.Content.ReadAsStringAsync();
        StoreData(year, day);
    }
    

    public void SetTestData(string testData)
    {
        Data = testData.Replace("\r", "");
    }
}

