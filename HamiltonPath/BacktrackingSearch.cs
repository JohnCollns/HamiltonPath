using System;

class BacktrackingSearch{
    public List<int>? HamiltonianCycle(AdjGraph graph){
        for(int i = 0; i < graph.edges.Count; i++){
            List<int>? path = HamiltonianPath(graph, i);
            if(path != null){
                return path;
            }
        }
        return null;
    }

    public List<int>? HamiltonianPath(AdjGraph graph, int start){
        List<List<int>> edges = graph.edges;
        List<int> path = new List<int>(new int[]{start});
        List<int> checkedUntilEdgeInEdgeList= Enumerable.Repeat(-1,edges.Count-1).ToList();
        while(true){
            if(path.Count == 0){
                return null;
            }

            if(path.Count == edges.Count){
                if(edges[path[path.Count-1]].Contains(start)){
                    return path;
                }else{
                    path.RemoveAt(path.Count-1);
                }
            }

            bool added = false;
            int currentVertex = path[path.Count-1];

            for(int i = checkedUntilEdgeInEdgeList[path.Count-1]+1; i < edges.Count; i++){

                if(!path.Contains(i) && graph.UVBiDirectional(i, currentVertex)){
                    checkedUntilEdgeInEdgeList[path.Count-1] = i;
                    path.Add(i);
                    added = true;
                    break;
                }
            }
            if(!added){
                checkedUntilEdgeInEdgeList[path.Count-1] = -1;
                path.RemoveAt(path.Count-1);
            }
        }
    }
}
