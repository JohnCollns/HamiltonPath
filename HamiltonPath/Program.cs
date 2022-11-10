// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
AdjGraph g = new AdjGraph(3);
//g.AddEdgeUni(0, 1);
//g.AddEdgeUni(1, 2);
//g.AddEdgeUni(2, 0);

g.AddEdgeDirected(0, 1);
g.AddEdgeDirected(1, 2);
g.AddEdgeDirected(2, 0);

Console.WriteLine("Edges of 0:\t");
List<int[]> edgesOf0 = g.GetAllEdgesOfNode(0);
foreach (int[] edge in edgesOf0)
    Console.Write($"({edge[0]}, {edge[1]}), ");
RubinSearch search = new RubinSearch();
Console.WriteLine(search.HasHamiltonCycle(g));
g.PrintEdges();

