// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
var g = new AdjGraph(5);
g.AddEdgeDirected(0, 1);
g.AddEdgeUni(1, 2);
g.AddEdgeUni(0, 3);
g.AddEdgeDirected(3, 4);
var ret = g.GetAllEdges();
foreach (int[] uv in ret)
    Console.WriteLine(uv[0]+" - " + uv[1]);
g.PrintEdges();

