using System.Text.RegularExpressions;

namespace AdventOfCode.Common;

public class InputRepository(
    HttpClient _httpClient)
    :
 IInputRepository
{
    public string? Data { get; private set; }

    private int _year;
    private int _day;

    private string FileName => $"year{_year}day{_day}.txt";

    public IEnumerable<IList<TType>> ToEnumerableOfList<TType>(string regex, string separator, int skip = 0)
        => Split(regex, skip)
            .Select(x => x.Split(separator).Select(y => (TType)Convert.ChangeType(y, typeof(TType))).ToList());

    public IList<TType> ToList<TType>(string regex, int skip = 0)
        => Split(regex, skip)
            .Select(x => (TType)Convert.ChangeType(x, typeof(TType))).ToList();

    private IEnumerable<string> Split(string regex, int skip = 0)
        => Regex.Split(Data!, regex, RegexOptions.Multiline)
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(x => x.Trim())
            .Skip(skip);

    public async Task GetInputAsync(int year, int day)
    {
        _day = day;
        _year = year;

        Data = await LoadDataFromFile() ?? await FetchDataFromWeb();
    }

    private async Task<string?> LoadDataFromFile()
        => File.Exists(FileName)
        ? await File.ReadAllTextAsync(FileName)
        : null;

    private async Task<string?> FetchDataFromWeb()
    {
        var response = await _httpClient.GetAsync($"{_year}/day/{_day}/input");
        var content = await response.Content.ReadAsStringAsync();
        // make sure we have a newline at the end of the file, makes parsing easier
        content.Append('\n');
        await StoreData(content);
        return content;
    }

    private async Task StoreData(string content)
        => await File.WriteAllTextAsync(FileName, content);


    public void SetTestData(string testData)
        => Data = testData.Replace("\r", "");
}

