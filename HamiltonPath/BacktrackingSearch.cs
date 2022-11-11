using System;

public class BacktrackingSearch{
    public List<int>? HamiltonianCycle(AdjGraph graph){
        // Find any hamiltonian path in the graph
        for(int i = 0; i < graph.edges.Count; i++){
            // Check for hamiltonian path starting at all edges.
            // If there exists a cycle then we just return the cycle else we
            // we check another candidate vertex. If we run out of candidate vertices
            // we return null as no hamiltonian cycle exists.
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
        // List to keep track of where we are in the edge list when we 
        // checked for candidate vertices.
        List<int> checkedUntilEdgeInEdgeList= Enumerable.Repeat(-1,edges.Count-1).ToList();
        while(true){
            // If even the last vertex was popped we know that
            // there are no hamiltonian cycles starting with the given vertex.
            if(path.Count == 0){
                return null;
            }

            // If we have a path of size with the same size as the number of vertices
            // we have a complete hamiltonian path but not a cycle yet. We need to check
            // if it is a cycle by checking if the last vertex connects to the 
            // start if not then remove that vertex and then continue the search. If so
            // return the path.
            if(path.Count == edges.Count){
                if(edges[path[path.Count-1]].Contains(start)){
                    return path;
                }else{
                    path.RemoveAt(path.Count-1);
                }
            }

            bool added = false;
            int currentVertex = path[path.Count-1];
 
            // try to add the vertices which has not been added.
            for(int i = checkedUntilEdgeInEdgeList[path.Count-1]+1; i < edges.Count; i++){

                if(!path.Contains(i) && graph.UVBiDirectional(i, currentVertex)){
                    checkedUntilEdgeInEdgeList[path.Count-1] = i;
                    path.Add(i);
                    added = true;
                    break;
                }
            }
            
            // if nothing is added we know that taking this path further will not lead to a 
            // Hamiltonian cycle.
            if(!added){
                checkedUntilEdgeInEdgeList[path.Count-1] = -1;
                path.RemoveAt(path.Count-1);
            }
        }
    }
}
