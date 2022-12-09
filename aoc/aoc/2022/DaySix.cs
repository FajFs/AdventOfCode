public class DaySix
{
    private readonly DataFetcher _dataFetcher;
    public DaySix(DataFetcher dataFetcher)
    {
        _dataFetcher = dataFetcher ?? throw new ArgumentNullException(nameof(dataFetcher));
    }

    public async Task Run()
    {
        await _dataFetcher.GetAndStoreData(day: 6);
        Part1();
        Part2();
    }

    private static int GetMarkerPosition(string? buffer, int requiredUniques)
    {
        for (int i = 0; i < buffer!.Length - requiredUniques; i++)
            if (buffer[i..(i + requiredUniques)].Distinct().Count() == requiredUniques)
                return i + requiredUniques;
        throw new Exception("You fucked up");
    }

    private void Part1()
    {
        var result = GetMarkerPosition(_dataFetcher.Parse<string>("\n").SingleOrDefault(), 4);
        Console.WriteLine($"part 1: {result}");
    }

    private void Part2()
    {
        var result = GetMarkerPosition(_dataFetcher.Parse<string>("\n").SingleOrDefault(), 14);
        Console.WriteLine($"part 2: {result}");
    }
}