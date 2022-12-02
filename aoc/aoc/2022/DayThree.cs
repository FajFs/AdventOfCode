public class DayThree
{
    private readonly DataFetcher _dataFetcher;
    public DayThree(DataFetcher dataFetcher)
    {
        _dataFetcher = dataFetcher ?? throw new ArgumentNullException(nameof(dataFetcher));
    }

    public async Task Run()
    {
        await _dataFetcher.GetAndStoreData();
        Part1();
        Part2();
    }

    private void Part1()
    {
        var result = "";

        Console.WriteLine($"part 1: {result}");
    }

    private void Part2()
    {
        var result = "";

        Console.WriteLine($"part 2: {result}");
    }
}