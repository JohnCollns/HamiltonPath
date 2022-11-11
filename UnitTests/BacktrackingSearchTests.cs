namespace UnitTests{
    public class BacktrackingSearchTests{
        BacktrackingSearch bsearch = new BacktrackingSearch();
        
        [Fact]
        public void NoHamiltonianCycleShouldExistInAGraphWithAVertexOfDegree1(){
            AdjGraph g = new AdjGraph(6);
            g.AddEdgeUni(0,1);
            g.AddEdgeUni(1,2);
            g.AddEdgeUni(2,3);
            g.AddEdgeUni(3,4);
            g.AddEdgeUni(4,5);
            List<int>? path = bsearch.HamiltonianCycle(g);
            Assert.Null(path);
        }

        [Fact]
        public void HamiltonianCycleShouldExistRegardlessOfStartingVertexInACircularGraph(){
            int graphSize = 8;
            AdjGraph g = new AdjGraph(graphSize);
            g.AddEdgeUni(0,1);
            g.AddEdgeUni(1,2);
            g.AddEdgeUni(2,3);
            g.AddEdgeUni(3,4);
            g.AddEdgeUni(4,5);
            g.AddEdgeUni(5,6);
            g.AddEdgeUni(6,7);
            g.AddEdgeUni(7,0);

            List<List<int>?> solutions= new List<List<int>?>();
            for(int i = 0; i < graphSize; i++){
                solutions.Add(bsearch.HamiltonianPath(g,i));
            }
            foreach(List<int>? solution in solutions){
                Assert.NotNull(solution);
                CheckHamiltonianCycles(solution, g);
            }
        }

        [Fact]
        public void HamiltonianCycleShouldNotExistInDisconnectedGraph(){
            AdjGraph g = new AdjGraph(10);
            g.AddEdgeUni(0,1);

            List<int>? path = bsearch.HamiltonianCycle(g);

            Assert.Null(path);
        }

        bool CheckHamiltonianCycles(List<int> path, AdjGraph g){
            List<int> visited = new List<int>();
            for(int i =0; i < path.Count-1; i++){
                if(!g.UVBiDirectional(path[i],path[i+1]) || visited.Contains(path[i])){
                    return false;
                }
            }
            if(!g.UVBiDirectional(path[path.Count-1], path[0])){
                return false;
            }
            if(g.edges.Count != path.Count){
                return false;
            }
            return true;
        }

    }
}
