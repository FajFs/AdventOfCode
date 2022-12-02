public class DayTwo
{
    private readonly DataFetcher _dataFetcher;
    public DayTwo(DataFetcher dataFetcher)
    {
        _dataFetcher = dataFetcher ?? throw new ArgumentNullException(nameof(dataFetcher));
    }
    
    public async Task Run()
    {
        await _dataFetcher.GetAndStoreData();
        Part1();
        Part2();
    }

    private int GetSignValue(string option) => option switch
    {
        "A" or "X" => 1, //rock
        "B" or "Y" => 2, //paper
        "C" or "Z" => 3, //scissor
    };

    private static int GetPredictedValue(int opponent, string outcome) => outcome switch
    {
        "X" => GetPriviousInt(opponent),
        "Y" => opponent,
        "Z" => GetNextInt(opponent),
    };

    private static int GetPriviousInt(int x)
        => x == 1 ? 3 : x - 1;

    private static int GetNextInt(int x)
        => x == 3 ? 1 : x + 1;

    private int CalculateOutcome(int opponent, int you)
    {
        if (you == opponent)                 //draw
            return you + 3;              
        if (GetNextInt(you) == opponent)     //loss
            return you;      
        if (GetPriviousInt(you) == opponent) //win
            return you + 6;  
        throw new Exception("you fucked up");
    }

    private void Part1()
    {
        var result = _dataFetcher.ParseDataAsStrings("\n")
            .Select(x =>
            {
                var tmp = x.Split(" ");
                return (GetSignValue(tmp[0]), GetSignValue(tmp[1]));
            })
            .Select(x => CalculateOutcome(x.Item1, x.Item2))
            .Sum();

        Console.WriteLine($"part 1: {result}");
    }

    private void Part2()
    {
        var result = _dataFetcher.ParseDataAsStrings("\n")
            .Select(x =>
            {
                var tmp = x.Split(" ");
                var opponent = GetSignValue(tmp[0]);
                var you = GetPredictedValue(opponent, tmp[1]);
                return (opponent, you);
            })
            .Select(x => CalculateOutcome(x.Item1, x.Item2))
            .Sum();

        Console.WriteLine($"part 2: {result}");
    }
}