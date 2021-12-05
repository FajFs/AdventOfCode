using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;

public class Point
{
    public int X, Y;
    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }
    public override bool Equals(object obj)
    {
        var p = obj as Point;
        return p.X == this.X && this.Y == p.Y;
    }
    public override int GetHashCode()
    {
        return this.X.GetHashCode() + this.Y.GetHashCode();
    }
}
public class Line
{
    public int X1, X2, Y1, Y2;
    public List<Point> PointsInStraightLine { get; private set; }
    public List<Point> PointsInDiagonalLine { get; private set; }
    public List<Point> AllPoints { get; private set; }
    public Line(List<int> data)
    {
        X1 = data[0];
        X2 = data[2];
        Y1 = data[1];
        Y2 = data[3];
        SetPointsInStraightLine();
        SetPointsInDiagonalLine();
        SetAllPoints();
    }
    private void SetPointsInStraightLine()
    {
        var points = new List<Point>();
        if (Y1 == Y2)
        {
            for (int i = 0; i <= Math.Abs(X1 - X2); i++)
            {
                points.Add(new Point(Math.Min(X1, X2) + i, Y1));
            }
        }
        if( X1 == X2)
        {
            for (int i = 0; i <= Math.Abs(Y1 - Y2); i++)
            {
                points.Add(new Point( X1, Math.Min(Y1, Y2) + i));
            }
        }
        PointsInStraightLine = points;
    }
    private void SetPointsInDiagonalLine()
    {
        var points = new List<Point>();
        var minX = Math.Min(X1, X2);
        var minY = Math.Min(Y1, Y2);
        var maxY = Math.Max(Y1, Y2);
        if (X1 + Y1 == X2 + Y2)
        {
            for (int i = 0; i <= Math.Abs(X1 - X2); i++)
            {
                points.Add(new Point(minX + i, maxY - i));
            }
        }
        if (X1 - X2 == Y1 - Y2 )
        {
            for (int i = 0; i <= Math.Abs(X1 - X2); i++)
            {
                points.Add(new Point(minX + i, minY + i));
            }
        }
        PointsInDiagonalLine = points;
    }

    private void SetAllPoints()
    {
        this.AllPoints = this.PointsInDiagonalLine;
        this.AllPoints.AddRange(PointsInStraightLine);
    }

}

class Day5
{
    public Day5() => DataFetcher.GetAndStoreData(2021, 5);

    public Day5 Part1()
    {
        var lines = DataFetcher.ParseDataAsStrings("\n")
            .Select(x => x.Replace(" -> ", ","))
            .Select(x => new Line( x.Split(",").ToList().Select(x => int.Parse(x)).ToList())).ToList();
        var map = new Dictionary<Point, int>();
        foreach (var line in lines)
        {
            var points = line.PointsInStraightLine;
            foreach (var point in points)
            {
                if (map.ContainsKey(point)) map[point] += 1;
                else map.Add(point, 1);
            }
        }
        Console.WriteLine($"Part 1: {map.Where(x => x.Value > 1).Count()}");
        return this;
    }

    public Day5 Part2()
    {

        var lines = DataFetcher.ParseDataAsStrings("\n")
            .Select(x => x.Replace(" -> ", ","))
            .Select(x => new Line(x.Split(",").ToList().Select(x => int.Parse(x)).ToList())).ToList();
        var map = new Dictionary<Point, int>();
        foreach (var line in lines)
        {
            var points = line.AllPoints;
            foreach (var point in points)
            {
                if (map.ContainsKey(point)) map[point] += 1;
                else map.Add(point, 1);
            }
        }
        Console.WriteLine($"Part 2: {map.Where(x => x.Value > 1).Count()}");
        return this;
    }
}
