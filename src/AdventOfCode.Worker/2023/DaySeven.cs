using AdventOfCode.Common;
using AdventOfCode.Worker.Interfaces;

namespace AdventOfCode.Worker;

public partial class DaySeven(
    ILogger<DaySeven> logger,
    IInputRepository inputRepository)
    : IDay
{
    private readonly IInputRepository _inputRepository = inputRepository ?? throw new ArgumentNullException(nameof(inputRepository));
    private readonly ILogger<DaySeven> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task Part1()
    {
        await _inputRepository.GetInputAsync(year: 2023, day: 7);


        //_logger.LogInformation("Part 1: {result}", result);
    }

    public async Task Part2()
    {
        await _inputRepository.GetInputAsync(year: 2023, day: 7);


        //_logger.LogInformation("Part 2: {result}", result);
    }

}