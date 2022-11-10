AdjGraph g = new AdjGraph(4);
g.AddEdgeUni(0, 1);
g.AddEdgeUni(1, 2);
g.AddEdgeUni(2, 3);
g.AddEdgeUni(3, 0);
BacktrackingSearch bsearch = new BacktrackingSearch();
List<int> k = bsearch.HamiltonianPath(g,3);
foreach(int a in k){
    Console.WriteLine(a);
}
