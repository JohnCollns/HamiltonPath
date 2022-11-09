// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
AdjGraph g = new AdjGraph(3);
g.AddEdgeUni(0, 1);
g.AddEdgeUni(1, 2);
g.AddEdgeUni(2, 0);
RubinSearch search = new RubinSearch();
Console.WriteLine(search.HasHamiltonCycle(g));
g.PrintEdges();

