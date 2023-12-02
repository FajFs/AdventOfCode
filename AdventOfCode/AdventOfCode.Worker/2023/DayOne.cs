using AdventOfCode.Common;
using AdventOfCode.Worker.Interfaces;
using System.Text.RegularExpressions;

namespace AdventOfCode.Worker;

public partial class DayOne(
    ILogger<DayOne> logger,
    IInputRepository inputRepository)
    : IDay
{
    private readonly IInputRepository _inputRepository = inputRepository ?? throw new ArgumentNullException(nameof(inputRepository));
    private readonly ILogger<DayOne> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task Part1()
    {
        await _inputRepository.GetInputAsync(year: 2023, day: 1);

        var result = _inputRepository.ToList<string>("\n")
            .Select(x =>
            {
                var matches = MatchDigit().Matches(x);
                var first = matches.First().Value;
                var last = matches.Last().Value;
                return first + last;
            })
            .Select(x => int.Parse(x))
            .Sum();

        _logger.LogInformation("Part 1: {result}", result);
    }

    [GeneratedRegex(@"\d", RegexOptions.Compiled)]
    private static partial Regex MatchDigit();


    public async Task Part2()
    {
        await _inputRepository.GetInputAsync(year: 2023, day: 1);

        var result = _inputRepository.ToList<string>("\n")
            .Select(x =>
            {
                var first = ConvertWordToNumber(MatchWordAndDigit().Match(x).Value);
                var last = ConvertWordToNumber(MatchWordAndDigitReversed().Match(x).Value);
                return first + last;
            })
            .Select(x => int.Parse(x))
            .Sum();

        _logger.LogInformation("Part 2: {result}", result);
    }

    [GeneratedRegex(@"one|two|three|four|five|six|seven|eight|nine|\d")]
    private static partial Regex MatchWordAndDigit();

    [GeneratedRegex(@"one|two|three|four|five|six|seven|eight|nine|\d", RegexOptions.RightToLeft)]
    private static partial Regex MatchWordAndDigitReversed();

    private static string ConvertWordToNumber(string word)
        => word switch
        {
            "one" => "1",
            "two" => "2",
            "three" => "3",
            "four" => "4",
            "five" => "5",
            "six" => "6",
            "seven" => "7",
            "eight" => "8",
            "nine" => "9",
            _ => word,
        };
}