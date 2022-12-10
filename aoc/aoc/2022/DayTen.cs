using MoreLinq;
public class DayTen
{
    private readonly DataFetcher _dataFetcher;
    public DayTen(DataFetcher dataFetcher)
    {
        _dataFetcher = dataFetcher ?? throw new ArgumentNullException(nameof(dataFetcher));
    }

    public async Task Run()
    {
        await _dataFetcher.GetAndStoreData();
        Part1();
        Part2();
    }

    private List<(string, int)> InstructionsList = new();
    private int ExecutionCycles { get; set; } = 0;
    private int RegisterXValue { get; set; } = 0;
    private List<int> ShouldPeekRegisterX { get; init; }  =  new() { 20, 60, 100, 140, 180, 220 };


    private char[] Crt { get; set; } = new char[40 * 6];

    private void PrepareInstructions()
    {
        ExecutionCycles = 1;
        RegisterXValue = 1;
        InstructionsList = _dataFetcher.Parse<string>("\n")
            .Select(x => x.Split(" "))
            .Select(x => (x[0], x.Length == 2 ? int.Parse(x[1]) : 0)).ToList();
    }

    private void DrawCrt()
    {
        Crt[ExecutionCycles - 1] = new List<int> 
        { 
            RegisterXValue - 1, 
            RegisterXValue, 
            RegisterXValue + 1 
        }.Contains((ExecutionCycles - 1) % 40) ? '#' : '.';
    }


    private IEnumerable<int> ExecuteProgram()
    {
        foreach (var (operation, value) in InstructionsList)
        {
            DrawCrt();
            ExecutionCycles += 1;
            if (ShouldPeekRegisterX.Contains(ExecutionCycles))
                yield return ExecutionCycles * RegisterXValue;

            if (operation == "addx")
            {
                DrawCrt();
                RegisterXValue += value;
                ExecutionCycles += 1;
                if (ShouldPeekRegisterX.Contains(ExecutionCycles))
                    yield return ExecutionCycles * RegisterXValue;
            } 
        }
    }

    
    private void Part1()
    {
        PrepareInstructions();
        Console.WriteLine($"part 1: {ExecuteProgram().Sum()}");
    }

    private void Part2()
    {
        Crt.Batch(40).ForEach(x => Console.WriteLine(string.Join("", x)));
    }
}