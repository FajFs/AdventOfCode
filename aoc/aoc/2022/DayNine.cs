public class DayNine
{
    private readonly DataFetcher _dataFetcher;
    public DayNine(DataFetcher dataFetcher)
    {
        _dataFetcher = dataFetcher ?? throw new ArgumentNullException(nameof(dataFetcher));
    }

    public async Task Run()
    {
        await _dataFetcher.GetAndStoreData();
        Part1();
        Part2();
    }


    private HashSet<(int, int)> Visited { get; set; } = new();
    private List<(int x, int y)> Rope = new();


    private (int, int) MoveHeadInDirection((int x, int y) head, string direction)
        => direction switch
        {
            "U" => (head.x, head.y + 1),
            "D" => (head.x, head.y - 1),
            "R" => (head.x + 1, head.y),
            "L" => (head.x - 1, head.y),
            _ => throw new Exception("Invalid direction")
        };
 

    private bool IsAdjacantOrOverlapping((int x, int y) head, (int x, int y) tail)
     => (Math.Abs(head.x - tail.x) <= 1 && Math.Abs(head.y - tail.y) <= 1);

    public (int, int) MoveTailAdjacentToHead((int x, int y) head, (int x, int y) tail)
    {
        if (head.x > tail.x)
        {
            if (head.y > tail.y)
                return (tail.x + 1, tail.y + 1);
            else if (head.y < tail.y)
                return (tail.x + 1, tail.y - 1);
            else
                return (tail.x + 1, tail.y);
        }
        else if (head.x < tail.x)
        {
            if (head.y > tail.y)
                return (tail.x - 1, tail.y + 1);
            else if (head.y < tail.y)
                return (tail.x - 1, tail.y - 1);
            else
                return (tail.x - 1, tail.y);
        }
        else
        {
            if (head.y > tail.y)
                return (tail.x, tail.y + 1);
            else if (head.y < tail.y)
                return (tail.x, tail.y - 1);
        }
        return (0,0);
    }



    private void MoveRope(List<(string, int)> rope)
    {
        foreach (var (direction, length) in rope)
        {
            for (int i = 0; i < length; i++)
            {
                Rope[^1] = MoveHeadInDirection(Rope.Last(), direction);
                MoveRopeRecursive(direction, Rope.Count - 1);
            }
        }
    }

    private void MoveRopeRecursive(string direction, int ropeIndex)
    {
        if (ropeIndex <= 0)
        {
            Visited.Add(Rope[ropeIndex]);
            return;
        }
        var head = Rope[ropeIndex];
        var tail = Rope[ropeIndex - 1];
        if (!IsAdjacantOrOverlapping(head, tail))
            Rope[ropeIndex - 1] = MoveTailAdjacentToHead(head, tail);
        MoveRopeRecursive(direction, ropeIndex - 1);
    }

    private void Part1()
    {
        Visited = new();
        var result = _dataFetcher.Parse<string>("\n", new char[] { ' ' })
            .Select(x => (x[0], int.Parse(x[1])))
            .ToList();

        Rope = new() { new(), new() };
        MoveRope(result);

        Console.WriteLine($"part 1: {Visited.Count}");
    }

    private void Part2()
    {
        Visited = new();
        var result = _dataFetcher.Parse<string>("\n", new char[] { ' ' })
            .Select(x => (x[0], int.Parse(x[1])))
            .ToList();

        Rope = new();
        for (int i = 0; i < 10; i++)
            Rope.Add(new());

        MoveRope(result);
        Console.WriteLine($"part 2: {Visited.Count}");
    }
}