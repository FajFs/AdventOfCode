using AdventOfCode.Common;
using AdventOfCode.Worker.Interfaces;
using MathNet.Numerics;
using System.Collections.Frozen;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace AdventOfCode.Worker;

public partial class DayEight(
    ILogger<DayEight> logger,
    IInputRepository inputRepository)
    : IDay
{
    private readonly IInputRepository _inputRepository = inputRepository ?? throw new ArgumentNullException(nameof(inputRepository));
    private readonly ILogger<DayEight> _logger = logger ?? throw new ArgumentNullException(nameof(logger));


    [GeneratedRegex(@"^(\w+) = \((\w+), (\w+)\)$")]
    private static partial Regex ParseNode();
    private record Node(string Name, string LeftChild, string RightChild );

    public async Task Part1()
    {
        await _inputRepository.GetInputAsync(year: 2023, day: 8);
        var instructions = _inputRepository.ToList<string>("\n").First();

        var nodes = _inputRepository.ToList<string>("\n")
            .Skip(1)
            .Select(x =>
            {
                var match = ParseNode().Match(x);
                return new Node(
                    Name: match.Groups[1].Value,
                    LeftChild: match.Groups[2].Value,
                    RightChild: match.Groups[3].Value);
            })
            .ToFrozenSet();

        var currentNode = nodes.First(x => x.Name == "AAA");
        var steps = 0;
        while (currentNode.Name != "ZZZ")
        {
            foreach (var instruction in instructions)
            {
                steps++;
                if (instruction == 'L')
                    currentNode = nodes.First(x => x.Name == currentNode.LeftChild);
                else
                    currentNode = nodes.First(x => x.Name == currentNode.RightChild);

                if (currentNode.Name == "ZZZ")
                {
                    _logger.LogInformation("Part 1: {result}", steps);
                    return;
                }
            }
        }
    }

    private long GetStepsUntilGhostStops(Node currentNode, string instructions, FrozenSet<Node> map)
    {
        var _nodes = new HashSet<string>();
        long cycle = 0;
        while (true)
        {
            foreach (var instruction in instructions)
            {
                cycle++;
                if (instruction == 'L')
                    currentNode = map.First(x => x.Name == currentNode.LeftChild);
                else
                    currentNode = map.First(x => x.Name == currentNode.RightChild);
                if (currentNode.Name.EndsWith('Z'))
                    return cycle;
            }
        }
    }

    public async Task Part2()
    {
        await _inputRepository.GetInputAsync(year: 2023, day: 8);
        var instructions = _inputRepository.ToList<string>("\n")
           .First();

        var nodes = _inputRepository.ToList<string>("\n")
            .Skip(1)
            .Select(x =>
            {
                var match = ParseNode().Match(x);
                return new Node(
                    Name: match.Groups[1].Value,
                    LeftChild: match.Groups[2].Value,
                    RightChild: match.Groups[3].Value);
            })
            .ToFrozenSet();

        var stepsToReachEnd = nodes
            .Where(x => x.Name.EndsWith('A'))
            .Select(x => GetStepsUntilGhostStops(x, instructions, nodes)).ToList();

        //calculate LCM
        var lcm = stepsToReachEnd.Aggregate(Euclid.LeastCommonMultiple);

        _logger.LogInformation("Part 2: {result}", lcm);

    }
}
