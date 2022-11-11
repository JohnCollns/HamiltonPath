// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
AdjGraph g = new AdjGraph(5);
g.AddEdgeUni(0, 1);
g.AddEdgeUni(1, 2);
g.AddEdgeUni(2, 3);
g.AddEdgeUni(3, 0);
g.AddEdgeUni(1, 4);
g.PrintEdges();
//AdjGraph g = new AdjGraph(20);
//g.AddEdgeUni(0, 1); g.AddEdgeUni(0, 4); g.AddEdgeUni(0, 19);
//g.AddEdgeUni(1, 2); g.AddEdgeUni(1, 11);
//g.AddEdgeUni(2, 3); g.AddEdgeUni(2, 9);
//g.AddEdgeUni(3, 4); g.AddEdgeUni(3, 7);
//g.AddEdgeUni(4, 5);
//g.AddEdgeUni(5, 6); g.AddEdgeUni(5, 18);
//g.AddEdgeUni(6, 7); g.AddEdgeUni(6, 16);
//g.AddEdgeUni(7, 8);
//g.AddEdgeUni(8, 9); g.AddEdgeUni(8, 15);
//g.AddEdgeUni(9, 10);
//g.AddEdgeUni(10, 11); g.AddEdgeUni(10, 14);
//g.AddEdgeUni(11, 12);
//g.AddEdgeUni(12, 13); g.AddEdgeUni(12, 19);
//g.AddEdgeUni(13, 14); g.AddEdgeUni(13, 17);
//g.AddEdgeUni(14, 15);
//g.AddEdgeUni(15, 16);
//g.AddEdgeUni(16, 17);
//g.AddEdgeUni(17, 18);
//g.AddEdgeUni(18, 19);
//

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


