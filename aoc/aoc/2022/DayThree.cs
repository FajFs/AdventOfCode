using MoreLinq;

public class DayThree
{
    private readonly DataFetcher _dataFetcher;
    public DayThree(DataFetcher dataFetcher)
    {
        _dataFetcher = dataFetcher ?? throw new ArgumentNullException(nameof(dataFetcher));
    }

    public async Task Run()
    {
        await _dataFetcher.GetAndStoreData(day: 3);
        Part1();
        Part2();
    }

    private int GetAsciiValue(char x)
        => (int)x > 96 ? (int)x - 96 : (int)x - 64 + 26;

    private int GetCommonCharValue(IEnumerable<char> xs, IEnumerable<char> ys)
        => xs.Where(x => ys.Contains(x)).Select(x => GetAsciiValue(x)).First();

    private int GetCommonCharValue(IEnumerable<char> xs, IEnumerable<char> ys, IEnumerable<char> zs)
        => xs.Where(x => ys.Contains(x) && zs.Contains(x)).Select(x => GetAsciiValue(x)).First();

    private void Part1()
    {
        var result = _dataFetcher.Parse<string>("\n")
            .Select(rucksacks => GetCommonCharValue(rucksacks.Take(rucksacks.Length / 2), rucksacks.Skip(rucksacks.Length / 2)))
            .Sum();

        Console.WriteLine($"part 1: {result}");
    }

    private void Part2()
    {
        var result = _dataFetcher.Parse<string>("\n")
            .Batch(3)
            .Select(rucksackGroup =>
                GetCommonCharValue(rucksackGroup.ElementAt(0), rucksackGroup.ElementAt(1), rucksackGroup.ElementAt(2)))
            .Sum();

        Console.WriteLine($"part 2: {result}");
    }
}