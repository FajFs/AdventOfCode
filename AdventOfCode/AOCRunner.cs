class AOCRunner
{
    private int _day;
    public AOCRunner(int day) => _day = day;

    public object Run()
    {
        return _day switch
        {
            1 => new Day1().Part1().Part2(),
            2 => new Day2().Part1().Part2(),
            3 => new Day3().Part1().Part2(),
            4 => new Day4().Part1().Part2(),
            5 => new Day5().Part1().Part2(),
            6 => new Day6().Part1().Part2(),
            _=> ""     
        };
    }
}
