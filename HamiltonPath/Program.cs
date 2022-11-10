// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
AdjGraph g = new AdjGraph(3);
g.AddEdgeDirected(0, 1);
g.AddEdgeDirected(1, 2); g.AddEdgeDirected(1, 4);
g.AddEdgeDirected(2, 3);
g.AddEdgeDirected(3, 0);

Console.WriteLine("Edges of 0:\t");
List<int[]> edgesOf0 = g.GetAllEdgesOfNode(0);
foreach (int[] edge in edgesOf0)
    Console.Write($"({edge[0]}, {edge[1]}), ");
RubinSearch search = new RubinSearch();
Solution sol = search.HasHamiltonCycle(g);
Console.WriteLine($"Passes: {sol.hasHamiltonCycle}");
if (sol.hasHamiltonCycle)
{
    Console.Write("Path is: ");
    foreach (int node in sol.solutionPath)
        Console.Write($"{node}, ");
    Console.WriteLine();
}
g.PrintEdges();

