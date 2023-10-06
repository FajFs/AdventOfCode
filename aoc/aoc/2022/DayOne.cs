using AdventOfCode.Common;
using AdventOfCode.Worker;

namespace AdventOfCode.Worker;

public class DayOne(
    ILogger<DayOne> logger,
    IInputRepository inputRepository)
    : IDay
{
    private readonly IInputRepository _inputRepository = inputRepository ?? throw new ArgumentNullException(nameof(inputRepository));
    private readonly ILogger<DayOne> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task Part1()
    {
       await _inputRepository.GetInputAsync(year: 2022, day: 1);
        var result = _inputRepository.ToEnumerableOfList<int>("\n\n", "\n")
            .Select(x => x.Sum())
            .Max();

        _logger.LogInformation("Part 1: {result}", result);
    }

    public async Task Part2()
    {
        await _inputRepository.GetInputAsync(year: 2022, day: 1);

        var result = _inputRepository.ToEnumerableOfList<int>("\n\n", "\n")
            .Select(elf => elf.Sum())
            .OrderBy(x => -x)
            .Take(3)
            .Sum();


        _logger.LogInformation("Part 2: {result}", result);
    }
}