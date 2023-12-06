using AdventOfCode.Common;
using AdventOfCode.Worker.Interfaces;
using MoreLinq;

namespace AdventOfCode.Worker;

public partial class DayFive(
    ILogger<DayFive> logger,
    IInputRepository inputRepository)
    : IDay
{
    private readonly IInputRepository _inputRepository = inputRepository ?? throw new ArgumentNullException(nameof(inputRepository));
    private readonly ILogger<DayFive> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    private record Seed(long Destination, long Source, long Range);
    private record SeedRange(long Start, long End);
    public async Task Part1()
    {
        await _inputRepository.GetInputAsync(year: 2023, day: 5);

        var input = _inputRepository.ToList<string>("\n");
        var seeds = input
            //take first line
            .First()
            //split by space and skip first word
            .Split(' ')[1..]
            //convert to long
            .Select(long.Parse).ToList();


        // split list on input line starting with non-digit
        var blocks = _inputRepository.ToEnumerableOfList<string>(
            regex: "seed-to-soil map:|soil-to-fertilizer map:|fertilizer-to-water map:|water-to-light map:|light-to-temperature map:|temperature-to-humidity map:|humidity-to-location map:",
            separator: "\n",
            skip: 1);


        //store seed ranges per block
        var blockranges = new List<List<Seed>>();
        //process blockwise
        foreach (var block in blocks)
        {
            var seedRanges = block.Select(line
                => new Seed(
                    Destination: long.Parse(line.Split(' ')[0]),
                    Source: long.Parse(line.Split(' ')[1]),
                    Range: long.Parse(line.Split(' ')[2]))).ToList();

            var temporarySeeds = new List<long>();

            // go through all seeds in current block
            foreach (var seed in seeds)
            {
                var match = false;
                foreach (var seedRange in seedRanges)
                {
                    if (seedRange.Source <= seed && seed < seedRange.Source + seedRange.Range)
                    {
                        temporarySeeds.Add(seed - seedRange.Source + seedRange.Destination);
                        match = true;
                        break;
                    }
                }
                //if no match found, add seed to temporary list
                if (!match)
                    temporarySeeds.Add(seed);
            }
            seeds = temporarySeeds;
        }

        _logger.LogInformation("Part 1: {@result}", seeds.Min());
    }



    public async Task Part2()
    {
        await _inputRepository.GetInputAsync(year: 2023, day: 5);

        var input = _inputRepository.ToList<string>("\n");
        var seedsRanges = input
            //take first line
            .First()
            //split by space and skip first word
            .Split(' ')[1..]
            //convert to long
            .Select(long.Parse)
            .Batch(2)
            .Select(x => new SeedRange(x.First(), x.First() + x.Last()))
            .ToList();


        // split list on input line starting with non-digit
        var blocks = _inputRepository.ToEnumerableOfList<string>(
             regex: "seed-to-soil map:|soil-to-fertilizer map:|fertilizer-to-water map:|water-to-light map:|light-to-temperature map:|temperature-to-humidity map:|humidity-to-location map:",
             separator: "\n",
             skip: 1);

        var blockRanges = new List<List<Seed>>();
        //process blockwise
        foreach (var block in blocks)
        {
            //
            var seeds = block.Select(line
                => new Seed(
                    Destination: long.Parse(line.Split(' ')[0]),
                    Source: long.Parse(line.Split(' ')[1]),
                    Range: long.Parse(line.Split(' ')[2]))).ToList();

            var temporarySeedRanges = new List<SeedRange>();
            while (seedsRanges.Count != 0)
            {
                //take first seedRange
                var seedRange = seedsRanges.First();
                //remove first seedRange
                seedsRanges = seedsRanges[1..];

                var match = false;
                //go through all seeds in current block
                foreach (var seed in seeds)
                {
                    //check if there is any overlap at all
                    var overlapStart = Math.Max(seedRange.Start, seed.Source);
                    var overlapEnd = Math.Min(seedRange.End, seed.Source + seed.Range);
                    if (overlapStart < overlapEnd)
                    {
                        //there is overlap!
                        match = true;

                        //add overlap to temporary list
                        temporarySeedRanges.Add(new(overlapStart - seed.Source + seed.Destination, overlapEnd - seed.Source + seed.Destination));

                        //add lower non-overlapping parts to seedsRanges and reprocess
                        if (overlapStart > seedRange.Start)
                            seedsRanges.Add(new(seedRange.Start, overlapStart));

                        //add upper non-overlapping parts to seedsRanges and reprocess
                        if (seedRange.End > overlapEnd)
                            seedsRanges.Add(new(overlapEnd, seedRange.End));

                        break;
                    }
                }

                //if no match found, add seedRange to temporary list
                if (!match)
                    temporarySeedRanges.Add(seedRange);
            }
            //replace seedsRanges with temporary list and move on to next block
            seedsRanges = temporarySeedRanges;
        }

        _logger.LogInformation("Part 2: {@result}", seedsRanges.Min(x => x.Start));
    }



}