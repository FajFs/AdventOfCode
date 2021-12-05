using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class BingoBoard
{
    public BingoBoard(List<List<int>> board)
    {
        this.Board = board;
        HasBingo = false;
        Size = board.Count;
    }
    public int GetPoints()
    {
        var res = 0;
        foreach (var r in Board)
        {
            res += r.Where(x => x != int.MinValue).Sum();
        }
        return res;
    }

    public List<List<int>> Board {get; set;}
    public bool HasBingo { get; set; }
    public int Size { get; set; }
    public int lastTake { get; set; }
}

public class Day4
{
    public Day4() => DataFetcher.GetAndStoreData(2021, 4);

    private List<BingoBoard> FormBingoBoards(List<string> b)
    {
        var rowOfInts = new List<List<int>>();
        b.Skip(1).ToList().ForEach(r => 
            rowOfInts.Add(
                r.Split(" ").Where(x => !string.IsNullOrEmpty(x)).Select(x => int.Parse(x)).ToList()));

        return rowOfInts.Select((x, i) => (I: i, V: x)).GroupBy(x => x.I / 5).Select(x => x.Select(v => v.V).ToList()).ToList().Select(x => new BingoBoard(x)).ToList();
    }

    private BingoBoard ApplyTakeToBoard(BingoBoard bingo, int take)
    {
        if (bingo.HasBingo) return bingo;
        for (int i = 0; i < bingo.Size; i++)
        {
            if (bingo.Board[i].Contains(take)) bingo.Board[i][bingo.Board[i].IndexOf(take)] = int.MinValue;
        }
        return bingo;
    }

    public bool CheckBingo(BingoBoard bingo)
    {
        var hasBingo = true;
        //Row
        foreach (var r in bingo.Board)
        {
            if (r.TrueForAll(x => x == int.MinValue)) return true;
        }

        //col
        for (int i = 0; i < bingo.Size; i++)
        {
            hasBingo = true;
            for (int j = 0; j < bingo.Size; j++)
            {
                if (bingo.Board[j][i] != int.MinValue) hasBingo = false;
            }
            if (hasBingo) return true;
        }

        return false;
    }

    public Day4 Part1()
    {
        var data = DataFetcher.ParseDataAsStrings("\n");
        var takes = data.First().Split(",").Select(d => int.Parse(d)).ToList();
        var boards = FormBingoBoards(data);

        foreach (var take in takes)
        {
            for (int i = 0; i < boards.Count; i++)
            { 
                boards[i] = ApplyTakeToBoard(boards[i], take);
                if (CheckBingo(boards[i]))
                {
                    Console.WriteLine($"Part 1: {boards[i].GetPoints() * take}"); return this;
                }
            }
        }

        return this;
    }

    public Day4 Part2()
    {
        var data = DataFetcher.ParseDataAsStrings("\n");
        var takes = data.First().Split(",").Select(d => int.Parse(d)).ToList();
        var boards = FormBingoBoards(data);

        BingoBoard lastBingo = null;
        foreach (var take in takes)
        {
            for (int i = 0; i < boards.Count; i++)
            {
                if (boards[i].HasBingo) continue;
                boards[i] = ApplyTakeToBoard(boards[i], take);
                if (CheckBingo(boards[i]))
                {
                    boards[i].HasBingo = true;
                    boards[i].lastTake = take;
                    lastBingo = boards[i];
                }
            }
        }
        Console.WriteLine($"Part 2: {lastBingo.lastTake * lastBingo.GetPoints()}");
        return this;
    }
}
