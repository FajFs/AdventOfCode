using System;


class Day1
{
    public Day1() => DataFetcher.GetAndStoreData(2021, 1);

    public Day1 Part1()
    {
        var res = 0;
        var data = DataFetcher.ParseDataAsInts("\n");
        for (var i = 1; i < data.Count; i++)
            if (data[i] > data[i - 1])
                res += 1;
        Console.WriteLine($"part 1: {res}");
        return this;
    }

    public Day1 Part2()
    {
        var res = 0;
        var data = DataFetcher.ParseDataAsInts("\n");
        for (var i = 3; i < data.Count; i++)
            if (data[i] + data[i - 1] + data[i - 2] > data[i - 1] + data[i - 2] + data[i - 3])
                res += 1;
        Console.WriteLine($"part 2: {res}");
        return this;
    }

}
