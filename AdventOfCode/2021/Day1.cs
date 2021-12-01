using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2021
{
    class Day1
    {
        public Day1() => DataFetcher.GetAndStoreData(2021, 1);

        public void Part1()
        {
            var res = 0;
            var data = DataFetcher.ParseDataAsInts("\n");
            for (var i = 1; i < data.Count; i++)
                if (data[i] > data[i - 1])
                    res += 1;
            Console.WriteLine($"part 1: {res}");
        }

        public void Part2()
        {
            var res = 0;
            var data = DataFetcher.ParseDataAsInts("\n");
            for (var i = 3; i < data.Count; i++)
                if (data[i] + data[i - 1] + data[i - 2] > data[i - 1] + data[i - 2] + data[i - 3])
                    res += 1;
            Console.WriteLine($"part 2: {res}");
        }

    }
}
