using System;
using System.Collections.Generic;
using System.Linq;
class Day10
{
    public Day10(int y, int d) => DataFetcher.GetAndStoreData(y, d);

    private Dictionary<char, int> symbolsValue = new Dictionary<char, int>() {
        [')'] = 3,
        [']'] = 57,
        ['}'] = 1197,
        ['>'] = 25137,
    };
    private Dictionary<char, char> symbolsMatch = new Dictionary<char, char>()
    {
        ['('] = ')',
        ['['] = ']',
        ['{'] = '}',
        ['<'] = '>',
    };
    HashSet<char> right = new HashSet<char>() { ')', ']', '}', '>' };
    private char ValidateInput(char[] s, ref Stack<char> stack, int i)
    {
        if (i == s.Length) return ' ';
        var sym = s[i];
        if (stack.Count == 0 && !right.Contains(sym))
        {
            stack.Push(sym);
            return ValidateInput(s, ref stack, i + 1);
        }
        if(stack.Count == 0 && right.Contains(sym)) return sym;

        if (right.Contains(sym) && sym == symbolsMatch[stack.First()])stack.Pop();
        else if (right.Contains(sym) && sym != symbolsMatch[stack.First()])return sym;
        else if (symbolsMatch.TryGetValue(sym, out var x)) stack.Push(sym);
        return ValidateInput(s, ref stack, i + 1);   
    }

    public Day10 Part1()
    {
        var text = DataFetcher.ParseDataAsStrings("\n").Select(x => x.ToCharArray());
        var res = 0;
        var symComp = new Dictionary<char, int>();
        foreach (var row in text)
        {
            var stack = new Stack<char>();
            var sym = ValidateInput(row, ref stack, 0);
            if(sym != ' ')
            {
                symComp[sym] = symComp.TryGetValue(sym, out var x) ? x + 1 : 1;
            }
        }

        foreach (var (k,v) in symbolsValue)
            res += v * symComp[k];

        Console.WriteLine($"part 1: {res}");
        return this;
    }

    private Dictionary<char, UInt64> symbolsValue2 = new Dictionary<char, UInt64>()
    {
        ['('] = 1,
        ['['] = 2,
        ['{'] = 3,
        ['<'] = 4,
    };
    public Day10 Part2()
    {
        var text = DataFetcher.ParseDataAsStrings("\n").Select(x => x.ToCharArray());
        var res = new List<UInt64>();
        var symComp = new Dictionary<char, int>();
        foreach (var row in text)
        {
            var stack = new Stack<char>();
            var sym = ValidateInput(row, ref stack, 0);
            if (sym == ' ')
            {
                UInt64 tmp = 0;
                var missing = stack.ToList();
                foreach (var s in missing)
                {
                    tmp = (tmp * 5) + symbolsValue2[s];
                }
                res.Add(tmp);
            }
        }
        res.Sort();
        Console.WriteLine($"part 2: {res[res.Count()/2]}");
        return this;
    }

}
