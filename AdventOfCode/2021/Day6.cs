using System;
using System.Collections.Generic;
using System.Linq;

class Day6
{
    public Day6() => DataFetcher.GetAndStoreData(2021, 6);

    public Day6 Part1()
    {
        var data = DataFetcher.ParseDataAsInts(",");
        for (int i = 0; i < 80; i++)
        {
            data = data.Select(x => x - 1).ToList();
            var filter = data.Where(x => x == -1).ToList();
            for (int j = 0; j < filter.Count(); j++)
            {
                data.Add(8);
                data.Add(6);
            }
            data = data.Where(x => x != -1).ToList();
        }
        Console.WriteLine($"Part 1: {data.Count()}");
        return this;
    }

    private void Solver(int itterator)
    {

    }

    public Day6 Part2()
    {
        var data = DataFetcher.ParseDataAsInts(",");
        var res = new UInt64[9];
        for (int i = 0; i < data.Count; i++)
                res[data[i]] += 1;

        for (int i = 0; i < 256; i++)
        {
            UInt64 zeros = 0;
            for (var j = 1; j < res.Length; j++)
            {
                if (j == 1)
                    zeros = res[j - 1];
                res[j - 1] = res[j];
            }
            res[6] += zeros;
            res[8] = zeros;
        }
        UInt64 sum = 0;
        for (int i = 0; i < res.Length; i++)
        {
            sum += res[i];
        }
        Console.WriteLine(value: $"Part 2: {sum}");
        return this;
    }

}
