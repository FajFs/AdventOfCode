using AdventOfCode.Common;
using AdventOfCode.Worker.Interfaces;

namespace AdventOfCode.Worker;

public partial class DayTen(
    ILogger<DayX> logger,
    IInputRepository inputRepository)
    : IDay
{
    private readonly IInputRepository _inputRepository = inputRepository ?? throw new ArgumentNullException(nameof(inputRepository));
    private readonly ILogger<DayX> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task Part1()
    {
        await _inputRepository.GetInputAsync(year: 2023, day: 10);

        //_logger.LogInformation("Part 1: {result}", result);
    }

    public async Task Part2()
    {
        await _inputRepository.GetInputAsync(year: 2023, day: 10);

        //_logger.LogInformation("Part 2: {result}", result);
    }
}