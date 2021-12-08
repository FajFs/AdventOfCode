using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Day8
{
    public Day8(int year, int day) => DataFetcher.GetAndStoreData(year, day);

    public Day8 Part1()
    {
        var filter = new List<int>() { 2, 3, 4, 7 };
        var data = DataFetcher.ParseDataAsStrings("\n").Select(x => x.Split("|")[1]).Select(x => x.Trim().Split(" ")).ToList();
        var res = 0;
        foreach (var row in data)
        {
            foreach(var s in row)
            {
                if (filter.Contains(s.Length)) res++;
            }
        } 
        Console.WriteLine($"Part 1: {res}");
        return this;
    }

    static IEnumerable<IEnumerable<T>> GetPermutations<T>(List<T> list, int length)
    {
        if (length == 1) return list.Select(t => new T[] { t });

        return GetPermutations(list, length - 1)
            .SelectMany(t => list.Where(e => !t.Contains(e)),
                (t1, t2) => t1.Concat(new T[] { t2 }));
    }


    public Day8 Part2()
    {
        var signals = DataFetcher.ParseDataAsStrings("\n").Select(x => x.Split("|")[1]).Select(x => x.Trim().Split(" ")).ToList();
        var wires = DataFetcher.ParseDataAsStrings("\n").Select(x => x.Split("|")[0]).Select(x => x.Trim().Split(" ")).ToList();
        
        var validDigitSegments = new List<string>{ "abcefg", "cf", "acdeg", "acdfg", "bcdf", "abdfg", "abdefg", "acf", "abcdefg", "abcdfg"};
        var segments = "abcdefg";
        var permutations = GetPermutations(segments.ToCharArray().ToList(), segments.Length).Select(x => string.Join("", x)).ToList(); //wtf???
        var res = 0;
        foreach (var (w, s) in wires.Zip(signals))
        {
            //get current segment map
            var mapSegment = w.Where(x => x.Length == 7).First();
            //check all permutations of 8 segments against the given mapping
            foreach (var perm in permutations)
            {
                //create mapper between the mapSegment and current permutation
                var mapper = new Dictionary<char, char>();
                foreach (var (a, b) in mapSegment.Zip(perm))
                    mapper[a] = b;

                //use mapper to try translate valid segments to value
                var valueMapper = new Dictionary<string, string>();
                foreach (var wire in w)
                {
                    var translatedValue = "";
                    foreach (var c in wire)
                        translatedValue += mapper[c];
                    //KEY = sorted wire value, VALUE = index of translated value in validSegments
                    valueMapper[String.Concat(wire.OrderBy(c => c))] = validDigitSegments.IndexOf(string.Concat(translatedValue.OrderBy(x => x))).ToString();
                }
                //if map not successful test next perm
                if (valueMapper.Values.Any(k => k == "-1")) continue;

                var tmp = "";
                foreach (var num in s)
                    tmp += valueMapper[String.Concat(num.OrderBy(c => c))];

                res += int.Parse(tmp);
                break;
            }
        }
        Console.WriteLine($"Part 2: {res}");
        return this;
    }
}
