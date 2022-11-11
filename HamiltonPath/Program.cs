AdjGraph g = new AdjGraph(3);
g.AddEdgeUni(0, 1);
g.AddEdgeUni(1, 2); 
g.AddEdgeUni(0, 2); 
Solution DFSsol = DFSHamilton.HasHamiltonPath(g, 0);
Console.WriteLine($"Passes: {DFSsol.hasHamiltonCycle}");
if (DFSsol.hasHamiltonCycle)
{
    Console.Write("Path is: ");
    foreach (int node in DFSsol.solutionPath)
        Console.Write($"{node}, ");
    Console.WriteLine();
}

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
