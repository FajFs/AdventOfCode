
using Newtonsoft.Json;

public class DayThirteen
{
    private readonly DataFetcher _dataFetcher;
    public DayThirteen(DataFetcher dataFetcher)
    {
        _dataFetcher = dataFetcher ?? throw new ArgumentNullException(nameof(dataFetcher));
    }

    public async Task Run()
    {
        await _dataFetcher.GetAndStoreData(day: 13);
        Part1();
        Part2();
    }


    private bool IsNum(char a) => 48 <= a && a <= 57;

    private bool Compare(object left, object right)
    {
        var l = left is Array ? 
            left as Array
            : 
            left;




        //if (left is Array || right is Array)
        //    return Compare(left, right);
        return true;
    }




    private void Part1()
    {
        var inputs = _dataFetcher.Parse<string>("\n\n").Select(x => (x.Split("\n")[0], x.Split("\n")[1])).ToList();
        var result = 0;

        for (var i = 0; i < inputs.Count; i++)
        {
            var left = JsonConvert.DeserializeObject(inputs[i].Item1);
            var right = JsonConvert.DeserializeObject(inputs[i].Item2);

            if (Compare(left!, right!))

                result = result + i + 1;

        }

        Console.WriteLine($"part 1: {result}");
    }

    private void Part2()
    {
        var result = "";

        Console.WriteLine($"part 2: {result}");
    }
}