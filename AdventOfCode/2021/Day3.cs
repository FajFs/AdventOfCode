using System;
using System.Collections.Generic;
using System.Linq;

public class Day3
{
    public Day3() => DataFetcher.GetAndStoreData(2021, 3);

    private int[] GetSumOfPositiveBits(List<string> d)
    {
        var ones = new int[d.First().Length];
        foreach (var e in d)
            for (var i = 0; i < e.Length; i++)
                ones[i] += (int)char.GetNumericValue(e[i]);
        return ones;
    }

    private char GetBit(int noOnes, int length, bool floorCheck = true) 
        => floorCheck switch
        {
            true => noOnes >= length / 2 ? '1' : '0',
            false => noOnes > length / 2 ? '1' : '0'
        };



    public Day3 Part1()
    {
        var binLines = DataFetcher.ParseDataAsStrings("\n");
        var gammaRate = "";
        var epsilonRate = "";

        var filter = GetSumOfPositiveBits(binLines);
        foreach (var noOnes in filter)
        {
            gammaRate += GetBit(noOnes, binLines.Count);
            epsilonRate += GetBit(binLines.Count - noOnes, binLines.Count);
        }
        Console.WriteLine($"Part 1: {Convert.ToInt32(gammaRate, 2) * Convert.ToInt32(epsilonRate, 2)}");
        return this;
    }

    public Day3 Part2()
    {
        var oxygenBinLines = DataFetcher.ParseDataAsStrings("\n");
        var co2BinLines = DataFetcher.ParseDataAsStrings("\n");

        for(var i = 0; i < oxygenBinLines.First().Length; i++)
        {
            if (oxygenBinLines.Count == 1) break;
            var filter = GetBit(GetSumOfPositiveBits(oxygenBinLines)[i], oxygenBinLines.Count);
            oxygenBinLines = oxygenBinLines.Where(binLine => binLine[i] == filter).ToList();
        }
        
        for (var i = 0; i < co2BinLines.First().Length; i++)
        {
            if (co2BinLines.Count == 1) break;
            var filter = GetBit(co2BinLines.Count - GetSumOfPositiveBits(co2BinLines)[i], co2BinLines.Count, floorCheck: false);
            co2BinLines = co2BinLines.Where(binLine => binLine[i] == filter).ToList();
        }
        Console.WriteLine($"Part 1: {Convert.ToInt32(oxygenBinLines.First(), 2) * Convert.ToInt32(co2BinLines.First(), 2)}");
        return this;
    }
}