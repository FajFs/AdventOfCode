using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Day7
{
    public Day7(int year, int day) => DataFetcher.GetAndStoreData(year, day);

    public Day7 Part1()
    {
        var data = DataFetcher.ParseDataAsInts(",");
        var minMoves = int.MaxValue;
        for (int moveTo = 0; moveTo < data.Max(); moveTo++)
        {
            var dist = data.Select(x => Math.Abs(x - moveTo)).Sum();
            if (dist < minMoves) minMoves = dist;
        }
        Console.WriteLine($"Part 1: {minMoves}");
        return this;
    }
    public Day7 Part2()
    {
        var data = DataFetcher.ParseDataAsInts(",");
        var minMoves = int.MaxValue;
        for (int moveTo = 0; moveTo < data.Max(); moveTo++)
        {
            var dist = data.Select(
                x => Enumerable.Range(1, Math.Abs(x - moveTo)).Sum()).Sum();
            if (dist < minMoves) minMoves = dist;
        }
        Console.WriteLine($"Part 2: {minMoves}");
        return this;
    }
}
