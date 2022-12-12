using MoreLinq;
using System.Xml.Linq;

public class DayTwelve
{
    private readonly DataFetcher _dataFetcher;
    public DayTwelve(DataFetcher dataFetcher)
    {
        _dataFetcher = dataFetcher ?? throw new ArgumentNullException(nameof(dataFetcher));
    }

    public async Task Run()
    {
        await _dataFetcher.GetAndStoreData();
        Part1();
        Part2();
    }

    class Node
    {
        public Node(char name)
        {
            Name = name;
        }

        public char Name { get; set; }
        public bool Visited { get; set; }
        public List<Node> Children { get; set; } = new();
        public Node Previous { get; set; }
    }

    private static int BFS(Node start, Node end)
    {
        Queue<Node> queue = new Queue<Node>();
        queue.Enqueue(start);

        while (queue.Count > 0)
        {
            Node currentNode = queue.Dequeue();
            if (currentNode.Name == end.Name)
                break;

            foreach (Node child in currentNode.Children.Where(x => x.Name <= currentNode.Name + 1))
            {
                if (!child.Visited)
                {
                    child.Visited = true;
                    child.Previous = currentNode;
                    queue.Enqueue(child);
                }
            }
        }

        List<Node> shortestPath = new List<Node>();
        Node current = end;
        while (current != null)
        {
            shortestPath.Add(current);
            current = current.Previous;
        }
        return shortestPath.Count() - 1;
    }



    private static List<Node> PopulateGraph(char[][] input)
    {
        var nodes = new List<Node>();
        foreach (var row in input)
            foreach (var ch in row)
                nodes.Add(new Node(ch));

        for (int row = 0; row < input.Length; row++)
        {
            for (int col = 0; col < input[row].Length; col++)
            {
                var nodeIndex = row * input[row].Length + col;
                var node = nodes[nodeIndex];

                if (col > 0)
                    node.Children.Add(nodes[nodeIndex - 1]); // left
                if (col < input[row].Length - 1)
                    node.Children.Add(nodes[nodeIndex + 1]); // right
                if (row > 0)
                    node.Children.Add(nodes[nodeIndex - input[row].Length]); // up
                if (row < input.Length - 1)
                    node.Children.Add(nodes[nodeIndex + input[row].Length]); // down
            }
        }
        return nodes;
    }


    private void Part1()
    {
        var graph = PopulateGraph(_dataFetcher.Parse<string>("\n")
            .Select(x => x.ToArray()).ToArray());

        var start = graph.First(x => x.Name == 'S');
        var end = graph.First(x => x.Name == 'E');

        start.Name = (char)('a');
        end.Name = (char)('z' + 1);
        start.Visited = true;
        start.Previous = null;

        Console.WriteLine($"part 1: {BFS(start, end)}");
    }

    private void Part2()
    {
        var graph = PopulateGraph(_dataFetcher.Parse<string>("\n")
            .Select(x => x.ToArray()).ToArray());

        var end = graph.First(x => x.Name == 'E');
        var min = graph.Where(x => x.Name == 'a').Select(start =>
        {
            graph.ForEach(x =>
            {
                x.Visited = false;
                x.Name = x.Name == 'S' ? 'a' : x.Name;
            });
            start.Name = (char)('a');
            end.Name = (char)('z' + 1);
            start.Visited = true;
            start.Previous = null;

            return BFS(start, end);
        }).Min();


        Console.WriteLine($"part 2: {min}");
    }
}