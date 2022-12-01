﻿public class DayOne
{
    private readonly DataFetcher _dataFetcher;
    public DayOne(DataFetcher dataFetcher)
    {
        _dataFetcher = dataFetcher ?? throw new ArgumentNullException(nameof(dataFetcher));
    }
    
    public async Task Run()
    {
        await _dataFetcher.GetAndStoreData();
        Part1();
        Part2();
    }

    private void Part1()
    {
        var result = _dataFetcher.ParseDataAsStrings("\n\n")
            .Select(elf =>
                elf.Split("\n").Select(x => int.Parse(x)).Sum())
            .Max();

        Console.WriteLine($"part 1: {result}");
    }
    
    private void Part2()
    {
        var result = _dataFetcher.ParseDataAsStrings("\n\n")
            .Select(elf =>
                elf.Split("\n").Select(x => int.Parse(x)).Sum())
            .OrderBy(x => -x)
            .Take(3)
            .Sum();
            
        Console.WriteLine($"part 2: {result}");
    }
}