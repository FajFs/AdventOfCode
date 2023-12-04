using AdventOfCode.Common;
using AdventOfCode.Worker.Interfaces;

namespace AdventOfCode.Worker;

public partial class DayFive(
    ILogger<DayFive> logger,
    IInputRepository inputRepository)
    : IDay
{
    private readonly IInputRepository _inputRepository = inputRepository ?? throw new ArgumentNullException(nameof(inputRepository));
    private readonly ILogger<DayFive> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task Part1()
    {
        await _inputRepository.GetInputAsync(year: 2023, day: 4);


        //_logger.LogInformation("Part 1: {result}", result);
    }


    private readonly List<int> _totalCards = [];

    public async Task Part2()
    {
        await _inputRepository.GetInputAsync(year: 2023, day: 4);


        //_logger.LogInformation("Part 2: {result}", result);
    }

}