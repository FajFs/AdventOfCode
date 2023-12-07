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

    private record PokerHand(char[] Cards, int Bet);

    private List<char> cards = [];

    private int GetHand(PokerHand a)
    {
        if (a.Cards.Distinct().Count() == 1)
            return 6;
        if(a.Cards.GroupBy(x => x).Any(x => x.Count() == 4))
            return 5;
        if(a.Cards.GroupBy(x => x).Any(x => x.Count() == 2) && a.Cards.GroupBy(x => x).Any(x => x.Count() == 3))
            return 4;
        if(a.Cards.GroupBy(x => x).Any(x => x.Count() == 3))
            return 3;
        if(a.Cards.GroupBy(x => x).Where(x => x.Count() == 2).Count() == 2)
            return 2;
        if(a.Cards.Distinct().Count() == 4)
            return 1;
        return 0;
    }

    private bool HasABetterValueThanB(PokerHand a, PokerHand b)
    {
        var indexValuesA = a.Cards.Select(x => cards.IndexOf(x)).ToList();
        var indexValuesB = b.Cards.Select(x => cards.IndexOf(x)).ToList();

        for(var i = 0; i < indexValuesA.Count; i++)
        {
            // if a has a higher value than b then a is better
            if (indexValuesA[i] > indexValuesB[i])
                return true;
            // if b has a higher value than a then b is better
            if (indexValuesB[i] > indexValuesA[i])
                return false;
        }
        return true;
    }

    private int ComparePokerHands(PokerHand a, PokerHand b)
    {
        var avalue = GetHand(a);
        var bvalue = GetHand(b);

        if (avalue > bvalue)
            return 1;
        else if (bvalue > avalue)
            return -1;
        else
            return HasABetterValueThanB(a, b) 
                ? 1 
                : -1;
    }

    public async Task Part1()
    {
        await _inputRepository.GetInputAsync(year: 2023, day: 7);

        var pokerHands = _inputRepository.ToList<string>("\n")
            .Select(x =>
            {
                var split = x.Split(" ");
                var cards = split.First().ToCharArray();
                var bet = int.Parse(split.Last());
                return new PokerHand(cards, bet);
            })
            .ToList();

        cards = ['2', '3', '4', '5', '6', '7', '8', '9', 'T', 'J', 'Q', 'K', 'A'];
        pokerHands.Sort(ComparePokerHands);

        var sum = 0;
        for(var i = 1; i <= pokerHands.Count; i++)
            sum += pokerHands[i - 1].Bet * i;

        _logger.LogInformation("Part 1: {@result}", sum);
    }

    private HashSet<char[]> CreatePermutationsOfJoker(char[] hand)
    {
        var permutations = new HashSet<char[]>();
        foreach(var card in cards)
            permutations.Add(hand.ToList().Select(x => x == 'J' ? card : x).ToArray());
        return permutations;
    }

    private int ComparePokerHandsWildCard(PokerHand a, PokerHand b)
    {   
        var avalue = CreatePermutationsOfJoker(a.Cards)
            .Select(x => new PokerHand(x, a.Bet))
            .Select(GetHand)
            .Max();

        var bvalue = CreatePermutationsOfJoker(b.Cards)
            .Select(x => new PokerHand(x, b.Bet))
            .Select(GetHand)
            .Max();

        if (avalue > bvalue)
            return 1;
        else if (bvalue > avalue)
            return -1;
        else
            return HasABetterValueThanB(a, b) 
                ? 1 
                : -1;
    }


    public async Task Part2()
    {
        await _inputRepository.GetInputAsync(year: 2023, day: 7);
        
        var pokerHands = _inputRepository.ToList<string>("\n")
            .Select(x =>
            {
                var split = x.Split(" ");
                var cards = split.First().ToCharArray();
                var bet = int.Parse(split.Last());
                return new PokerHand(cards, bet);
            })
            .ToList();

        cards  = ['J', '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'Q', 'K', 'A'];
        pokerHands.Sort(ComparePokerHandsWildCard);

        var sum = 0;
        for (var i = 1; i <= pokerHands.Count; i++)
            sum += pokerHands[i - 1].Bet * i;

        _logger.LogInformation("Part 2: {result}", sum);
    }

}