using AdventOfCode.Common;
using AdventOfCode.Worker.Interfaces;
using MoreLinq;

namespace AdventOfCode.Worker;

public partial class DayFour(
    ILogger<DayFour> logger,
    IInputRepository inputRepository)
    : IDay
{
    private readonly IInputRepository _inputRepository = inputRepository ?? throw new ArgumentNullException(nameof(inputRepository));
    private readonly ILogger<DayFour> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task Part1()
    {
        await _inputRepository.GetInputAsync(year: 2023, day: 4);
        var result = _inputRepository.ToList<string>("\n")
            .Select(card =>
            {
                //Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
                var winningEntries = card.Split(':', '|')[1].Replace("  ", " ").Trim().Split(" ")
                .Select(x => int.Parse(x))
                .ToList();

                var drawnEntries = card.Split(':', '|')[2].Replace("  ", " ").Trim().Split(" ")
                .Select(x => int.Parse(x))
                .ToList();

                //calculate score for each card
                var result = drawnEntries.Select(x => winningEntries.Count(y => y == x)).Sum();
                return Math.Floor(Math.Pow(2, result - 1));
            })
            .Sum();

        _logger.LogInformation("Part 1: {result}", result);
    }


    private readonly List<int> _totalCards = [];

    public async Task Part2()
    {
        await _inputRepository.GetInputAsync(year: 2023, day: 4);
        var input = _inputRepository.ToList<string>("\n");

        input.ForEach(card =>
        {
            //Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
            var parsedCard = card.Split(':', '|');
            var cardNumber = int.Parse(parsedCard[0].Split(' ').Last());

            //store current card number
            _totalCards.Add(cardNumber);

            var winningEntries = parsedCard[1].Replace("  ", " ").Trim().Split(" ")
            .Select(x => int.Parse(x))
            .ToList();

            var drawnEntries = parsedCard[2].Replace("  ", " ").Trim().Split(" ")
            .Select(x => int.Parse(x))
            .ToList();

            //calculate score for card
            var numberOfWinsInCard = drawnEntries.Select(x => winningEntries.Count(y => y == x)).Sum();

            //get existing copies of game
            var existingCopiesOfGame = _totalCards.Count(x => x == cardNumber);
            var copies = Enumerable.Repeat(Enumerable.Range(cardNumber + 1, numberOfWinsInCard), existingCopiesOfGame);

            //copies means we are playing the same game multiple times and winning
            foreach (var copy in copies)
                _totalCards.AddRange(copy.Select(x => x));
        });

        _logger.LogInformation("Part 2: {result}", _totalCards.Count);
    }

}