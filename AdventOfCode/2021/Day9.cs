using System;
using System.Collections.Generic;
using System.Linq;

class Day9
{
    public Day9(int year, int day) => DataFetcher.GetAndStoreData(year, day);

    private bool IsLowPoint(List<List<int>> map, int x, int y, int val)
    {
        var a = x + 1 > map.Count - 1? -1 : map[x + 1][y];
        var b = x - 1 < 0 ? -1 : map[x - 1][y];
        var c = y + 1 > map.First().Count - 1? -1 : map[x][y + 1];
        var d = y - 1 < 0 ? -1 : map[x][y - 1];
        if (!new List<int>() { a, b, c, d }.Where(x => x != -1).All(x => x > val)) return false;
        return true;
    }

    public Day9 Part1()
    {
        var data = DataFetcher.ParseDataAsStrings("\n").Select(x => x.ToCharArray().Select(y => int.Parse(y.ToString())).ToList()).ToList();
        var res = 0;
        for (int i = 0; i < data.Count; i++)
        {
            for (int j = 0; j < data.First().Count; j++)
            {
                var a = i + 1 > data.Count - 1 ? -1 : data[i + 1][j];
                var b = i - 1 < 0 ? -1 : data[i - 1][j];
                var c = j + 1 > data.First().Count - 1 ? -1 : data[i][j + 1];
                var d = j - 1 < 0 ? -1 : data[i][j - 1];
                if (!new List<int>() { a, b, c, d }.Where(x => x != -1).All(x => x > data[i][j])) continue;
                res += data[i][j] + 1;
            }
        }

        Console.WriteLine($"Part 1: {res}");
        return this;
    }


    private void RecCountBasin(List<List<int>> map, int x, int y, ref Dictionary<(int, int), bool> visited)
    {
        var pos = map[x][y];
        visited.TryGetValue((x, y), out var isVisited);
        if (isVisited || pos == 9) return;
        visited[(x, y)] = true;

        if(x + 1 < map.Count) RecCountBasin(map, x + 1, y, ref visited);
        if(x - 1 >= 0) RecCountBasin(map, x - 1, y, ref visited); 
        if(y + 1 < map.First().Count) RecCountBasin(map, x, y + 1, ref visited);
        if(y - 1 >= 0) RecCountBasin(map, x, y - 1, ref visited);
    }

    public Day9 Part2()
    {
        var data = DataFetcher.ParseDataAsStrings("\n").Select(x => x.ToCharArray().Select(y => int.Parse(y.ToString())).ToList()).ToList();
        var basins = new List<int>();
        for (int i = 0; i < data.Count; i++)
        {
            for (int j = 0; j < data.First().Count; j++)
            {
                var visited = new Dictionary<(int, int), bool>();
                if(IsLowPoint(data, i, j, data[i][j]))
                {
                    RecCountBasin(data, i, j, ref visited);
                    basins.Add(visited.Count());
                }
            }
        }
        var res = 1;
        basins.Sort();
        basins.TakeLast(3).ToList().ForEach(x => res *= x);
        Console.WriteLine($"Part 1: {res}");
        return this;
    }

}
