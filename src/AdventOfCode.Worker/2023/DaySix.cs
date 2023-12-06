using AdventOfCode.Common;
using AdventOfCode.Worker.Interfaces;

namespace AdventOfCode.Worker;

public partial class DaySix(
    ILogger<DaySix> logger,
    IInputRepository inputRepository)
    : IDay
{
    private readonly IInputRepository _inputRepository = inputRepository ?? throw new ArgumentNullException(nameof(inputRepository));
    private readonly ILogger<DaySix> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    private record Race(long Time, long Distance);

    private int CalculateRaceWin(Race race)
    {
        var wins = 0;
        for (int holdTime = 0; holdTime < race.Time; holdTime++)
        {
            var distance = holdTime * (race.Time - holdTime);
            if (distance > race.Distance)
                wins++;
        }
        return wins;
    }

    public async Task Part1()
    {
        await _inputRepository.GetInputAsync(year: 2023, day: 6);

        List<Race> races = [new(46, 214), new(80, 1177), new(78, 1402), new(66, 1024)];

        var result = races.Select(CalculateRaceWin)
            .Aggregate((a, b) => a * b);

        _logger.LogInformation("Part 1: {result}", result);
    }

    public async Task Part2()
    {
        await _inputRepository.GetInputAsync(year: 2023, day: 6);

        var race = new Race(46807866, 214117714021024);
        var result = CalculateRaceWin(race);

        _logger.LogInformation("Part 2: {result}", result);
    }

}