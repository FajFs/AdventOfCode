using System;
using System.Collections.Generic;
using System.Linq;

public class Day3
{
    public Day3() => DataFetcher.GetAndStoreData(2021, 3);

    private int[] getOnes(List<string> d)
    {
        var ones = new int[d.First().Length];
        foreach (var e in d)
            for (var i = 0; i < e.Length; i++)
                ones[i] += (int)char.GetNumericValue(e[i]);
        return ones;
    }

    public Day3 Part1()
    {
        var d = DataFetcher.ParseDataAsStrings("\n");
        var gammaRate = "";
        var epsilonRate = "";

        var ones = getOnes(d);
        foreach (var one in ones)
        {
            gammaRate += one > d.Count / 2 ? "1" : "0";
            epsilonRate += one > d.Count / 2 ? "0" : "1";
        }
        Console.WriteLine($"Part 1: {Convert.ToInt32(gammaRate, 2) * Convert.ToInt32(epsilonRate, 2)}");
        return this;
    }

    public Day3 Part2()
    {
        var oxygen = DataFetcher.ParseDataAsStrings("\n");
        var co2 = DataFetcher.ParseDataAsStrings("\n");

        for(var i = 0; i < oxygen.First().Length; i++)
        {
            if (oxygen.Count == 1) break;
            var val = getOnes(oxygen)[i] >= oxygen.Count / 2 ? '1' : '0';
            oxygen = oxygen.Where(line => line[i] == val).ToList();
        }
        
        for (var i = 0; i < co2.First().Length; i++)
        {
            if (co2.Count == 1) break;
            var val = getOnes(co2)[i] >= co2.Count / 2 ? '0' : '1';
            co2 = co2.Where(line => line[i] == val).ToList();
        }
        Console.WriteLine($"Part 1: {Convert.ToInt32(oxygen.First(), 2) * Convert.ToInt32(co2.First(), 2)}");
        return this;
    }
}