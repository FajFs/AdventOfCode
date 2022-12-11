using System.Numerics;
using System.Text.RegularExpressions;
using static DayEleven;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class DayEleven
{
    private readonly DataFetcher _dataFetcher;
    public DayEleven(DataFetcher dataFetcher)
    {
        _dataFetcher = dataFetcher ?? throw new ArgumentNullException(nameof(dataFetcher));
    }

    public async Task Run()
    {
        await _dataFetcher.GetAndStoreData();
        Part1();
        Part2();
    }

    public static List<Monkey> Monkeys { get; set; } = new();


    private void ParseMonkeys(IEnumerable<string> monkeys, double divideBy)
    {
        Monkeys = new();

        //find a common multiplier for all devisors to have a common number base for all monkeys
        var commonMultiplier = monkeys.Select(monkeyString => int.Parse(monkeyString.Split("\n").Select(x => x.Trim()).ToList()[3].Split(" ").Last()))
            .Aggregate((x,y) => x * y);
        
        foreach (var monkeyString in monkeys)
        {
            //parse meta data about monkey
            var monkeyStringList = monkeyString.Split("\n").Select(x => x.Trim()).ToList();
            var id = int.Parse(monkeyStringList[0].Split(new char[] { ' ', ':' }).Skip(1).First());
            var items = new Queue<long>(monkeyStringList[1].Split(new string[] { ", ", " " }, StringSplitOptions.None).Skip(2).Select(x => long.Parse(x)).ToArray());

            //parse monkey business operation method
            var op = monkeyStringList[2].Split(" ").Skip(4).First();
            var hasStaticWorry = long.TryParse(monkeyStringList[2].Split(" ").Skip(5).First(), out var staticWorry);

            long operation(long oldWorry)
                => op == "*" ? (long)Math.Floor((oldWorry * (hasStaticWorry ? staticWorry : oldWorry) / divideBy)) % commonMultiplier 
                : (long)Math.Floor((oldWorry + (hasStaticWorry ? staticWorry : oldWorry) / divideBy)) % commonMultiplier;

            //parse monkey business test method
            var modulu = int.Parse(monkeyStringList[3].Split(" ").Last());
            var trueMonkey = long.Parse(monkeyStringList[4].Split(" ").Last());
            var falseMonkey = long.Parse(monkeyStringList[5].Split(" ").Last());

            long test(long newValue) 
                => newValue % modulu == 0 ? trueMonkey : falseMonkey;

            var monkey = new Monkey
            {
                Id = id,
                Items = items,
                InspectedItems = 0,
                Operation = operation,
                Test = test,
            };

            //add monkey to list of MANKIS
            Monkeys.Add(monkey);
        }
    }

    private void Part1()
    {
        ParseMonkeys(_dataFetcher.Parse<string>("\n\n"), 3);

        for (int i = 0; i < 20; i++)
            Monkeys.ForEach(monkey => monkey.DoMonkeyBusiness());

        var bigMankies = Monkeys.OrderBy(x => -x.InspectedItems)
            .Take(2)
            .Select(x => x.InspectedItems)
            .Aggregate((x, y) => x * y);
        Console.WriteLine($"part 1: {bigMankies}");
    }

    private void Part2()
    {
        ParseMonkeys(_dataFetcher.Parse<string>("\n\n"), 1);

        for (int i = 0; i < 10_000; i++)
            Monkeys.ForEach(monkey => monkey.DoMonkeyBusiness());

        var bigMankies = Monkeys.OrderBy(x => -x.InspectedItems)
            .Take(2)
            .Select(x => x.InspectedItems)
            .Aggregate((x, y) => x * y);
        Console.WriteLine($"part 2: {bigMankies}");
    }

    public class Monkey
    {
        public int Id { get; set; }
        public Queue<long> Items { get; set; }
        public Func<long, long> Operation { get; init; }
        public Func<long, long> Test{ get; init; }
        public BigInteger InspectedItems { get; set; } = 0;

 
        public void DoMonkeyBusiness()
        {
            while (Items.Any())
            {
                var item = Operation(Items.Dequeue());
                DayEleven.Monkeys.SingleOrDefault(x => x.Id == Test(item))!.Items.Enqueue(item);
                InspectedItems++;
            }            
        }
    }
}