public class DayEight
{
    private readonly DataFetcher _dataFetcher;
    public DayEight(DataFetcher dataFetcher)
    {
        _dataFetcher = dataFetcher ?? throw new ArgumentNullException(nameof(dataFetcher));
    }

    public async Task Run()
    {
        await _dataFetcher.GetAndStoreData();
        Part1();
        Part2();
    }

    private List<List<int>> TreeGrid { get; set; } = new List<List<int>>();
    private List<int> ScenicScore { get; set; } = new List<int>();

    private int CalculateGridCircumference()
        => 2 * TreeGrid.Count + 2 * TreeGrid.First().Count - 4;

    private (int, bool) IsVisableTop(int i, int j)
    {
        var visableTrees = 0;
        var currentTree = TreeGrid[i][j];
        for (int x = i - 1; x >= 0; x--)
            if(TreeGrid[x][j] >= currentTree)
                return (++visableTrees, false);
            else
                visableTrees++;
        return (visableTrees, true);
    }

    private (int, bool) IsVisableBottom(int i, int j)
    {
        var visableTrees = 0;
        var currentTree = TreeGrid[i][j];
        for (int x = i + 1; x < TreeGrid.Count; x++)
            if (TreeGrid[x][j] >= currentTree)
                return (++visableTrees, false);
            else
                visableTrees++;
        return (visableTrees, true);
    }

    private (int, bool) IsVisableLeft(int i, int j)
    {
        var visableTrees = 0;
        var currentTree = TreeGrid[i][j];
        for (int y = j - 1; y >= 0; y--)
            if (TreeGrid[i][y] >= currentTree)
                return (++visableTrees, false);
            else
                visableTrees++;
        return (visableTrees, true);
    }

    private (int, bool) IsVisableRight(int i, int j)
    {
        var visableTrees = 0;
        var currentTree = TreeGrid[i][j];
        for (int y = j + 1; y < TreeGrid.First().Count; y++)
            if (TreeGrid[i][y] >= currentTree)
                return (++visableTrees, false);
            else
                visableTrees++;
        return (visableTrees, true);
    }

    private int TravelInnerGrid()
    {
        var visableTrees = 0;
        for (int i = 1; i < TreeGrid.Count - 1; i++)
        {
            for (int j = 1; j < TreeGrid[i].Count - 1; j++)
            {
                var visabilityList = new List<(int, bool)> 
                { 
                    IsVisableTop(i, j), 
                    IsVisableBottom(i, j), 
                    IsVisableLeft(i, j), 
                    IsVisableRight(i, j) 
                };
                
                if (visabilityList.Any(x => x.Item2 == true))
                {
                    visableTrees++;
                    ScenicScore.Add(visabilityList.Select(x => x.Item1).Aggregate((x, y) => x * y));
                }
            }
        }      
        return visableTrees;
    }

    private void Part1()
    {
        TreeGrid = _dataFetcher.Parse<string>("\n")
            .Select(x => x.ToArray().Select(y => int.Parse(y.ToString())).ToList())
            .ToList();

        Console.WriteLine($"part 1: {CalculateGridCircumference() + TravelInnerGrid()}");
    }


    private void Part2()
    {
        Console.WriteLine($"part 2: {ScenicScore.Max()}");
    }
}