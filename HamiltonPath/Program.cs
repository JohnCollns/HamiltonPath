// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
AdjGraph g = new AdjGraph(5);
g.AddEdgeUni(0, 1);
g.AddEdgeUni(1, 2); g.AddEdgeUni(1, 4);
g.AddEdgeUni(2, 3);
g.AddEdgeUni(3, 0);
g.AddEdgeUni(4, 2);

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

