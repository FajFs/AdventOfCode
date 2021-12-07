using System.Linq;

class AOCRunner
{
    public AOCRunner() { }

    public void RunAll()
    {
        foreach (var i in Enumerable.Range(1,26))
        {
            System.Console.WriteLine($"########### DAY {i} ############");
            Run(2021,i);
            System.Console.WriteLine();
        }
    }

    public object Run(int year, int day)
    {
        return day switch
        {
            1 => new Day1().Part1().Part2(),
            2 => new Day2().Part1().Part2(),
            3 => new Day3().Part1().Part2(),
            4 => new Day4().Part1().Part2(),
            5 => new Day5().Part1().Part2(),
            6 => new Day6().Part1().Part2(),
            7 => new Day7(year, day).Part1().Part2(),
            8 => new Day8(year, day).Part1().Part2(),
            _=> ""     
        };
    }
}
