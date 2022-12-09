public class DayFive
{
    private readonly DataFetcher _dataFetcher;
    public DayFive(DataFetcher dataFetcher)
    {
        _dataFetcher = dataFetcher ?? throw new ArgumentNullException(nameof(dataFetcher));
    }

    public async Task Run()
    {
        await _dataFetcher.GetAndStoreData();
        Part1();
        Part2();
    }

    private List<(int,int,int)> InitialSetupOfCargos()
    {
        //prepare for inserting data into a list of stacks
        cargoStacks = new List<Stack<char>>(10);
        //Bruh :|
        var initialsetup = _dataFetcher.Data!
            .Split("\n")
            //replace 4 spaces with small x, trim brackets, remove whitespace, trim
            .Select(x => x.Replace("    ", "x").Replace("[", "").Replace("]", "").Replace(" ", "").Trim())
            .Take(8)
            .Reverse()
            .ToList();
        
        //instert each index into the cargoStacks
        for (int i = 0; i < initialsetup.Count() + 1; i++)
        {
            var stack = new Stack<char>();
            for (int j = 0; j < initialsetup.Count(); j++)
                if (initialsetup[j][i] != 'x')
                    stack.Push(initialsetup[j][i]);

            cargoStacks.Add(stack);
        }
        
        return _dataFetcher.Parse<string>("\n", skip: 9)
            .Select(x =>
            {
                var tmp = x.Split(" ");
                return (int.Parse(tmp[1]), int.Parse(tmp[3]), int.Parse(tmp[5]));
            })
            .ToList();
    }

    //could be done better with lists and slicing but commited to this to early...
    private List<Stack<char>> cargoStacks { get; set; }

    private void MoveCargoStack(int move, int from, int to)
    {
        for (int i = 0; i < move; i++)
            cargoStacks[to -1].Push(cargoStacks[from -1].Pop());

    }
    
    private void Part1()
    {
        foreach (var (move, to, from) in InitialSetupOfCargos())
            MoveCargoStack(move, to, from);

        var result = "";
        foreach (var cargo in cargoStacks)
            result += cargo.Pop();
     
        Console.WriteLine($"part 1: {result}");
    }

    private void MoveCargoStackV2(int move, int from, int to)
    {
        var tmpStack = new Stack<char>();
        for (int i = 0; i < move; i++)
            tmpStack.Push(cargoStacks[from - 1].Pop());

        while (tmpStack.Count > 0)
            cargoStacks[to - 1].Push(tmpStack.Pop());
    }

    private void Part2()
    {
        foreach (var (move, to, from) in InitialSetupOfCargos())
            MoveCargoStackV2(move, to, from);

        var result = "";
        foreach (var cargo in cargoStacks)
            result += cargo.Pop();

        Console.WriteLine($"part 2: {result}");
    }
}