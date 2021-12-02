using System;
using System.Linq;

public class Day2
{
    public Day2() => DataFetcher.GetAndStoreData(2021, 2);

    private int depth { get; set; }
    private int horizontal { get; set; }
    private int aim { get; set; }

    private void setPosPart1(string dir, int val)
    {
        switch (dir)
        {
            case "forward": horizontal += val;
                break;
            case "down": depth += val;
                break;
            case "up": depth -= val;
                break;
            default: break;
        };
    }

    public Day2 Part1()
    {
        var operations = DataFetcher.ParseDataAsStrings("\n").Where(d => !string.IsNullOrEmpty(d));
        foreach (var o in operations)
        {
            setPosPart1(o.Split(" ")[0], int.Parse(o.Split(" ")[1]));
        }
        Console.WriteLine($"Part 1: {depth * horizontal}");
        return this;
    }


    private void setPosPart2(string dir, int val)
    {
        switch (dir)
        {
            case "forward": 
                horizontal += val;
                depth += aim * val;
                break;
            case "down": aim += val;
                break;
            case "up": aim -= val; 
                break;
            default: break;
        };
    }

    private void reset() => depth = horizontal = aim = 0;
    public Day2 Part2()
    {
        reset();
        var operations = DataFetcher.ParseDataAsStrings("\n").Where(d => !string.IsNullOrEmpty(d));
        foreach (var o in operations)
        {
            setPosPart2(o.Split(" ")[0], int.Parse(o.Split(" ")[1]));
        }
        Console.WriteLine($"Part 2: {depth * horizontal}");
        return this;
    }
}