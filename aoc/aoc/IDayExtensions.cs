namespace AdventOfCode.Worker;

public static class IDayExtensions
{
    public static async Task ExecuteAsync(this IDay day)
    {
        //await day.Part1();
        await day.Part2();
    }
}
