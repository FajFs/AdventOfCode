using AdventOfCode.Common;
using AdventOfCode.Worker.Interfaces;

namespace AdventOfCode.Worker;

public partial class DayNine(
    ILogger<DayNine> logger,
    IInputRepository inputRepository)
    : IDay
{
    private readonly IInputRepository _inputRepository = inputRepository ?? throw new ArgumentNullException(nameof(inputRepository));
    private readonly ILogger<DayNine> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    private static int ExtrapolateValuesRecursive(List<int> numbers)
    {
        if(numbers.All(x => x == 0))
            return 0;

        var nextNumbers = new List<int>();
        for (int i = 0; i < numbers.Count-1; i++)
            nextNumbers.Add(numbers[i + 1] - numbers[i]);

        return numbers.Last() + ExtrapolateValuesRecursive(nextNumbers);
    }

    public async Task Part1()
    {
        await _inputRepository.GetInputAsync(year: 2023, day: 9);

        var inputs = _inputRepository.ToList<string>("\n")
            .Select(x => x.Split(" ")
            .Select(int.Parse));

        var result = inputs.Select(x => ExtrapolateValuesRecursive(x.ToList())).Sum();
        _logger.LogInformation("Part 1: {result}", result);
    }


    private static int ExtrapolateValuesBackwardsRecursive(List<int> numbers)
    {
        if (numbers.All(x => x == 0))
            return 0;

        var nextNumbers = new List<int>();
        for (int i = 0; i < numbers.Count - 1; i++)
            nextNumbers.Add(numbers[i + 1] - numbers[i]);

        return numbers.First() - ExtrapolateValuesBackwardsRecursive(nextNumbers);
    }


    public async Task Part2()
    {
        await _inputRepository.GetInputAsync(year: 2023, day: 9);

        var inputs = _inputRepository.ToList<string>("\n")
            .Select(x => x.Split(" ")
            .Select(int.Parse));

        var result = inputs.Select(x => ExtrapolateValuesBackwardsRecursive(x.ToList())).Sum();
        _logger.LogInformation("Part 2: {result}", result);
    }
}