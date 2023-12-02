using AdventOfCode.Common;
using MoreLinq;

namespace AdventOfCode.Worker;

public partial class DayTwo(
    ILogger<DayTwo> logger,
    IInputRepository inputRepository)
    : IDay
{
    private readonly IInputRepository _inputRepository = inputRepository ?? throw new ArgumentNullException(nameof(inputRepository));
    private readonly ILogger<DayTwo> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task Part1()
    {
        await _inputRepository.GetInputAsync(year: 2023, day: 2);
        var lines = _inputRepository.ToList<string>("\n");

        var reds = 12;
        var blues = 14;
        var greens = 13;

        //Game 1: 3 blue, 20 red; 1 red, 2 green, 6 blue; 2 green
        var result = lines.Select(x =>
        {
            var gameNumber = int.Parse(x.Replace("Game ", "").Split(":").First());
            var games = x.Split(":").Last().Split(";");
            foreach (var game in games)
            {
                var gameParts = game.Split(",").Select(x => x.Trim()).Select(x => (int.Parse(x.Split(" ").First()), x.Split(" ").Last()));
                foreach (var gamePart in gameParts)
                    if (gamePart.Item2 == "red" && gamePart.Item1 > reds || gamePart.Item2 == "blue" && gamePart.Item1 > blues || gamePart.Item2 == "green" && gamePart.Item1 > greens)
                        return 0;
            }
            return gameNumber;
        })
        .Sum();

        _logger.LogInformation("Part 1: {result}", result);
    }

    public async Task Part2()
    {
        await _inputRepository.GetInputAsync(year: 2023, day: 2);
        var lines = _inputRepository.ToList<string>("\n");

        //Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
        var result = lines.Select(x =>
        {
            var minumumReds = 0;
            var minumumBlues = 0;
            var minumumGreens = 0;

            var gameNumber = int.Parse(x.Replace("Game ", "").Split(":").First());
            var games = x.Split(":").Last().Split(";");

            foreach (var game in games)
            {
                var gameParts = game.Split(",").Select(x => x.Trim()).Select(x => (int.Parse(x.Split(" ").First()), x.Split(" ").Last()));
                foreach (var gamePart in gameParts)
                {
                    if (gamePart.Item2 == "red" && gamePart.Item1 > minumumReds)
                        minumumReds = gamePart.Item1;
                    else if (gamePart.Item2 == "blue" && gamePart.Item1 > minumumBlues)
                        minumumBlues = gamePart.Item1;
                    else if (gamePart.Item2 == "green" && gamePart.Item1 > minumumGreens)
                        minumumGreens = gamePart.Item1;
                }
            }
            return minumumBlues * minumumGreens * minumumReds;
        })
        .Sum();

        _logger.LogInformation("Part 2: {result}", result);
    }
}