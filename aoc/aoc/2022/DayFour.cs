public class DayFour
{
    private readonly DataFetcher _dataFetcher;
    public DayFour(DataFetcher dataFetcher)
    {
        _dataFetcher = dataFetcher ?? throw new ArgumentNullException(nameof(dataFetcher));
    }

    public async Task Run()
    {
        await _dataFetcher.GetAndStoreData();
        Part1();
        Part2();
    }

    private static bool IsCompleteOverlap(int a, int b, int x, int y)
        => (a <= x && x <= b && a <= y && y <= b) 
        || (x <= a && a <= y && x <= b && b <= y);

    private static bool IsPartialOvelap(int a, int b, int x, int y)
        => (a <= x && x <= b && y > b) 
        || (a <= y && y <= b && x < a)
        || (x <= a && a <= y && b > y) 
        || (x <= b && b <= y && a < x);

    private void Part1()
    {
        var result = _dataFetcher.Parse<int>("\n", new char[] { '-', ',' })
        .Select(ps =>
            IsCompleteOverlap(ps[0], ps[1], ps[2], ps[3]))
        .Sum(x => x ? 1 : 0);

        Console.WriteLine($"part 1: {result}");
    }

    private void Part2()
    {
        var result = _dataFetcher.Parse<int>("\n", new char[] { '-', ',' })
        .Select(ps =>
            IsCompleteOverlap(ps[0], ps[1], ps[2], ps[3])
            || IsPartialOvelap(ps[0], ps[1], ps[2], ps[3]))
        .Sum(x => x ? 1 : 0);
        
        Console.WriteLine($"part 2: {result}");
    }
}