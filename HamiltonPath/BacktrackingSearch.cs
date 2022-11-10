using System;

class BacktrackingSearch{
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

//class BacktrackingSearch{
//    // - Start with a vertex.
//    // - Check all the edges that exist in the graph that is not in the path and then add that to the path and break the loop.
//    // - Update the current index of the search in the edge list. So when we backtrack we can start from that given index.
//    // - If there is no edge left to see then nothing is added so we need to pop this path and then backtrack to another solution. Update the edge list accordingly.
//    // - If all the values are removed then we know we don't have a path and we should try starting with another vertex.
//    // - Once the path list is as big as the edge list, we need to check whether the path is valid and see if it connects with the start and if it does we need to return that.
//    public List<int>? HamiltonianCycle(AdjGraph graph){
//        for (int i = 0; i < graph.edges.Count; i++){
//            // Start with any vertex.
//            List<int>? path = HamiltonianPath2(graph, i);
//            if(path != null){
//                return path;
//            }
//        }
//        // When no other paths remain, we know no hamiltonian path exists.
//        return null;
//    }
//
//    public List<int>? HamiltonianPath2(AdjGraph graph, int start){
//        List<List<int>> edges= graph.edges;
//        List<int> path = new List<int>(new int[]{start});
//        List<int> currentIdxInEdgeList = Enumerable.Repeat(-1, edges.Count-1).ToList();
//        while(true){
//            print(path);
//            bool added = false;
//
//            if(path.Count == edges.Count){
//                if(edges[path[path.Count-1]].Contains(start)){
//                    return path;
//                }
//                path.RemoveAt(path.Count-1);
//            }
//
//            // Check if the path is empty. If so return.
//            if(path.Count==0){
//                return null;
//            }
//
//            int currentVertex = path[path.Count -1];
//
//            for(int i = currentIdxInEdgeList[path.Count-1]+1; i < edges.Count; i++){
//                if(!path.Contains(i) && graph.UVBiDirectional(currentVertex, i)){
//                    currentIdxInEdgeList[path.Count-1] = i;
//                    path.Add(i);
//                    added = true;
//                    // We want to add only one.
//                    break;
//                }
//            }
//
//            if(!added){
//                // If nothing gets added we need to back track to previous edge.
//                path.RemoveAt(path.Count-1);
//                currentIdxInEdgeList[path.Count-1] = -1; 
//            } 
//        }
//    }
//
//    public List<int>? HamiltonianPath(AdjGraph graph, int start){
//        List<List<int>> edges = graph.edges;
//        List<int> path = new List<int>(new int[]{start});
//        List<int> currentIdxInEdgeList = Enumerable.Repeat(0, edges.Count).ToList();
//        while(true){
//            int currentVertex = path[path.Count-1];
//            var (nextVertIdxInEdgeList, nextVertex) = GetNextVertex(edges, path, currentVertex, currentIdxInEdgeList[currentVertex]);
//            if(nextVertex!=0){
//                Console.WriteLine(nextVertex);
//            }
//            if(nextVertex == null){
//                // Go back a vertex.
//                path.RemoveAt(path.Count-1);
//                // Reset current index in edge list for the vertex.
//                currentIdxInEdgeList[currentVertex] = 0;
//                if(path.Count == 1){
//                    Console.Write("Here");
//                    return null;
//                }
//            }else{
//                // We can cast to int here because of null check above
//                path.Add((int)nextVertex);
//                currentIdxInEdgeList[currentVertex] = (int)nextVertIdxInEdgeList;
//                if(path.Count == edges.Count){
//                    int lastVertex = path[path.Count-1];
//                    if(edges[lastVertex].Contains(start)){
//                        return path;
//                    }else{
//                        if(checkIfExhausted(currentIdxInEdgeList, edges, start)){
//                            return null;
//                        }else{
//                            path.RemoveAt(path.Count-1);
//                        }
//                    }
//                }
//            }
//        }
//    }
//
//    public bool checkIfExhausted(List<int> idxInEdge, List<List<int>> edges, int start){
//        List<int> edgeCount = new List<int>();
//        foreach(List<int> vertex in edges){
//            edgeCount.Add(vertex.Count);
//        }
//        // We don't want to check first index as we generalizing for path as well as cycle.
//        for(int i = 0; i < edgeCount.Count; i++){
//            if(edgeCount[i] != idxInEdge[i]+1 && i != start){
//                return false;
//            }
//        }
//        return true;
//    }
//
//    void print(List<int> arr){
//        int[] newArr = arr.ToArray();
//        Console.WriteLine(string.Join(",", newArr));
//    }
//
//    (int?, int?) GetNextVertex(List<List<int>> edges, List<int> path, int currentVertex, int idxInEdgeList){
//        List<int> candidateVertices = edges[currentVertex];
//        for(int i = idxInEdgeList; i< candidateVertices.Count; i++){
//            int vertex = candidateVertices[i];
//            if(!path.Contains(vertex)){
//                return (i,vertex);
//            }
//        }
//        return (null,null);
//    }
//}
