using AdventOfCode.Common;
using AdventOfCode.Worker.Interfaces;
using System;
using System.Text.RegularExpressions;

namespace AdventOfCode.Worker;

public partial class DayTree(
    ILogger<DayTree> logger,
    IInputRepository inputRepository)
    : IDay
{
    private readonly IInputRepository _inputRepository = inputRepository ?? throw new ArgumentNullException(nameof(inputRepository));
    private readonly ILogger<DayTree> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    //maintain a reference to the point that is the base of the collision
    private record Point(int X, int Y, Point? BasePoint = null);
    private readonly HashSet<Point> _symbolsCollisionMatrix = [];

    private void BuildSymbolsCollisionMatrix(IList<string> input)
    {
        _symbolsCollisionMatrix.Clear();
        var maxX = input.Max(x => x.Length);
        var maxY = input.Count;

        for (int y = 0; y < maxY; y++)
        {
            var line = input[y];
            for (int x = 0; x < maxX; x++)
            {
                var symbol = line[x];
                var point = new Point(x, y, new(x,y));
                //check if symbol is not point and not digit
                if (symbol != '.' && !char.IsDigit(symbol))
                {
                    //check around point and validate bounds
                    _symbolsCollisionMatrix.Add(point);
                    if (x - 1 >= 0)
                        _symbolsCollisionMatrix.Add(new(x - 1, y, point));
                    if (x + 1 < maxX)
                        _symbolsCollisionMatrix.Add(new(x + 1, y, point));
                    if (y - 1 >= 0)
                        _symbolsCollisionMatrix.Add(new(x, y - 1, point));
                    if (y + 1 < maxY)
                        _symbolsCollisionMatrix.Add(new(x, y + 1, point));
                    if (x - 1 >= 0 && y - 1 >= 0)
                        _symbolsCollisionMatrix.Add(new(x - 1, y - 1, point));
                    if (x + 1 < maxX && y + 1 < maxY)
                        _symbolsCollisionMatrix.Add(new(x + 1, y + 1, point));
                    if (x - 1 >= 0 && y + 1 < maxY)
                        _symbolsCollisionMatrix.Add(new(x - 1, y + 1, point));
                    if (x + 1 < maxX && y - 1 >= 0)
                        _symbolsCollisionMatrix.Add(new(x + 1, y - 1, point));                   
                }
            }
        }
    }

    private IEnumerable<int> PartNumbersConnectedToSymbol(IList<string> input)
    {
        var maxX = input.Max(x => x.Length);
        var maxY = input.Count;

        for (int y = 0; y < maxY; y++)
        {
            var line = input[y];
            var points = new List<Point>();

            for (int x = 0; x < maxX; x++)
            {
                var symbol = line[x];
                if (char.IsDigit(symbol))
                    points.Add(new(x,y));

                //check if symbol is not digit or last symbol in line
                if(!char.IsDigit(symbol) || x == maxX - 1)
                {
                    if (points.Count == 0)
                        continue;
                    //check if point is in symbol collistion matrix
                    else if (points.Any(x => _symbolsCollisionMatrix.Any(y => y.X == x.X && y.Y == x.Y)))
                    //aggregate xpoints and convert to int
                        yield return
                            int.Parse(
                                    string.Join("", points.Select(x => line[x.X])));

                    //collision! clear points
                    points.Clear();
                }
            }
        }
    }

    public async Task Part1()
    {
        await _inputRepository.GetInputAsync(year: 2023, day: 3);
        var input = _inputRepository.ToList<string>("\n");
        BuildSymbolsCollisionMatrix(input);
        
        var result = PartNumbersConnectedToSymbol(input).Sum();
        _logger.LogInformation("Part 1: {result}", result);
    }

    private IEnumerable<(Point, int)> PartNumbersConnectedToAGear(IList<string> input)
    {
        var gearCandidates = new List<(Point, int)>();
        var maxX = input.Max(x => x.Length);
        var maxY = input.Count;

        for (int y = 0; y < maxY; y++)
        {
            var line = input[y];
            var points = new List<Point>();

            for (int x = 0; x < maxX; x++)
            {
                var symbol = line[x];
                if (char.IsDigit(symbol))
                    points.Add(new(x, y));

                //check if symbol is not digit or last symbol in line
                if (!char.IsDigit(symbol) || x == maxX - 1)
                {
                    if (points.Count == 0)
                        continue;

                    //check if point is in symbol collistion matrix
                    else if (points.Any(x => _symbolsCollisionMatrix.Any(y => y.X == x.X && y.Y == x.Y)))
                    {
                        //check if collision point is connected to a gear symbol
                        var gearCandidate = _symbolsCollisionMatrix.Where(x => points.Any(y => y.X == x.X && y.Y == x.Y)).FirstOrDefault()?.BasePoint;
                        if (gearCandidate is not null && input[gearCandidate!.Y][gearCandidate!.X] == '*')
                            yield return 
                                (gearCandidate, 
                                int.Parse(string.Join("", points.Select(x => line[x.X]))));
                    }
                    //collision! clear points
                    points.Clear();
                }
            }
        }
    }

    public async Task Part2()
    {
        await _inputRepository.GetInputAsync(year: 2023, day: 3);
        var input = _inputRepository.ToList<string>("\n");
        BuildSymbolsCollisionMatrix(input);
        
        
        var result = PartNumbersConnectedToAGear(input)
            //group by gear candidate
            .GroupBy(x => x.Item1)
            //check if gear candidate is connected to two numbers
            .Where(x => x.Count() == 2)
            //select gear candidate and multiply numbers
            .Select(x => (x.Key, x.Select(y => y.Item2).Aggregate((a, b) => a * b)))
            //sum all results
            .Sum(x => x.Item2);
             
        _logger.LogInformation("Part 2: {result}", result);
    }

}