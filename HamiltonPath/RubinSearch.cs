using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

public class RubinSearch
{
    #region Rules
    /* 
    S. Search Rules
S1. Select any single node as the initial path.
S2. Test the path for admissibility.
S3. If the path so far is admissible, list the successors of the last node chosen, and extend the path
    to the first of these. Repeat step S2.
S4. If the path so far is inadmissible, delete the last node chosen and choose the 
    next listed successor of the preceding node. Repeat step S2.
S5. If all extensions from a given node have been shown inadmissible, repeat step S4.
S6. If all extensions from the initial node have been shown inadmissible, then no circuit exists.
S7. If a successor of the last node is the origin, a Hamilton circuit is formed; if all Hamilton circuits are required, 
    then list the circuit found, mark the partial path inadmissible, and repeat step S4

    R. Required edge rules:
R1. If a vertex has only one directed arc entering (leaving), then t h a t arc is required.
R2. If a vertex has only two arcs incident, then b o t h arcs are required.

    A. Direction assignment rules:
A1. If a vertex has a required directed arc entering (leaving), then all incident un-
    directed arcs are assigned the direction leaving (entering) t h a t vertex.
A2. If a vertex has a required undirected arc incident, and all other incident arcs are
    leaving (entering) the vertex, then the required arc is assigned the direction entering (leaving) the vertex.

    D. Deleted edge rules:
D1. If a vertex has two required arcs incident, then all undecided arcs incident m a y
    be deleted.
D2. If a vertex has a required directed arc entering (leaving), then all undecided
    directed arcs entering (leaving) m a y be deleted.
D3. Delete any arc which forms a closed circuit with required arcs, unless it com-
    pletes the Hamilton circuit.

    F. Failure, or termination, rules:
F1. Fail if any vertex becomes isolated, that is, has no incident arc.
F2. Fail if any vertex has only one incident arc.
F3. Fail if any vertex has no directed arc entering (leaving).
F4. Fail if any vertex has two required directed arcs entering (leaving).
F5. Fail if any vertex has three required arcs incident.
F6. Fail if any set of required arcs forms a closed circuit, other than a Hamilton
F7. Fail if for any node not in the partial path there is no directed path to that node
    from the last node in the partial path.
F8. Fail if for any node not in the partial path there is no directed path from that
node to the initial node of the partial path.
F9. Fail if the graph is 1-connected. 

    C. Connectivity
C1. Flag all the nodes in the partial path.
C2. Place the last node on the partial path in the list.
C3. Choose any node on the list. Delete it from the list. Place all of its unflagged
    successors on the list, and flag them.
C4. Repeat step C3 until the list is empty. If every node is flagged, then the partial
    path is admissible; otherwise, it is inadmissible
    */
    #endregion

    private AdjGraph g;
    private int numVertices;
    private List<int> partialPath;
    private List<int[]> required;
    private List<int[]> deleted;
    private List<int[]> undecided;
    private int initialNode;

    public RubinSearch() { } // Gets initialised in the search function instead. 
    public Solution HasHamiltonCycle(AdjGraph original, int initialNode)
    {
        g = new AdjGraph(original);
        numVertices = original.numVertices;
        partialPath = new List<int>();
        required    = new List<int[]>();
        deleted     = new List<int[]>();
        undecided   = original.GetAllEdges();
        this.initialNode = initialNode;

        int searchIndex = 0;
        partialPath.Add(initialNode);
        Queue<int>[] potentialNodes = new Queue<int>[original.numVertices];
        for (int i=0; i < original.numVertices; i++)
            potentialNodes[i] = new Queue<int>();

        while (partialPath.Count <= numVertices)
        {
            // Iterative plan. 
            // Make an array of int queues length numVertices. 
            // When a node is accepted, increment the index to the array of queues. 
            // When the queue at a point is emptied, decrement this index. 
            // If a successor is the initial node, and the array of queues index is numVertices return true. 
            if (searchIndex < 0)
                return Solution.Fail;
            if (PathAdmissible())   
            {
                if (searchIndex == g.numVertices - 1) // S7. If a successor of the last node is the origin, a hamilton ciruit is formed. 
                    foreach (int outgoingEdge in g.GetAllOutwardEdgesOfNode(partialPath[searchIndex]))
                        if (outgoingEdge == initialNode)
                            return new Solution(true, partialPath);

                // S3. List successors
                foreach (int outgoingEdge in g.GetAllOutwardEdgesOfNode(partialPath[searchIndex]))
                {
                    if (searchIndex > 0)
                    {
                        if (outgoingEdge == partialPath[searchIndex - 1]) // if its a bidirectional edge going in reverse, ignore
                            continue;
                    }
                    potentialNodes[searchIndex].Enqueue(outgoingEdge);
                }
                //foreach (int[] edge in g.GetAllEdgesOfNode(partialPath[searchIndex]))]
                //potentialNodes[searchIndex].Enqueue(edge[0] == partialPath[searchIndex] ? edge[1] : edge[0]);
                partialPath.Add(potentialNodes[searchIndex].Dequeue()); // And extend path to first of these
                searchIndex++;
                continue;
            }
            else // S4. Delete last node and choose next listed successor of previous node
            {
                partialPath.RemoveAt(partialPath.Count - 1); // we should make sure that this is functioning
                searchIndex--;
                if (searchIndex < 0)
                    return Solution.Fail;
                if (potentialNodes[searchIndex].Count > 0)
                {
                    partialPath.Add(potentialNodes[searchIndex].Dequeue()); // And extend path to first of these
                    searchIndex++;
                    continue;
                }
                else
                    continue; // S5. If all extensions from a node are inadmissable, try from node before it
            }
        }

        return Solution.Fail;
    }

    public bool PathAdmissible()
    {
        int vertex = partialPath.Last();
        List<int[]> edgesOfVertex = g.GetAllEdgesOfNode(vertex);
        foreach (int[] edge in edgesOfVertex)
        {
            if (EdgeRequired(edge) && !ListContains(required, edge))//EdgeRequired(edge[0], edge[1])
            {
                required.Add(edge);
                //bool result = undecided.Remove(edge);
                RemoveFromList(undecided, edge);
                int checkitworked = 1;
            }
        }
        AssignDirection(vertex);
        DeleteEdges(vertex);

        // Failure cases
        if (g.TerminateF1F2F3())
            return false;
        // Handle F4,5,6
        // go through all req edges, with two bool arrays, for entering and leaving. 
        // each time a vertex is seen in one of the two positions, mark it. If it happens a third time fail, at the end if its not all true fail
        List<int> reqPath = new List<int>();
        reqPath.Add(initialNode);
        bool[] hasEntering = new bool[numVertices]; // not sure +1 is correct solution
        bool[] hasLeaving  = new bool[numVertices];
        foreach (int[] reqEdge in required)
        {
            if (!hasLeaving[reqEdge[0]])
                hasLeaving[reqEdge[0]] = true;
            else
                return false;
            if (!hasEntering[reqEdge[1]])
                hasEntering[reqEdge[1]] = true;
            else
                return false;
            reqPath.Add(reqEdge[1]);
            if (DFSLoop(reqPath))
                return false;
            //for (int i = 0; i < reqPath.Count; i++)
            //{
            //    if (reqPath[i] == reqPath.Last() && reqPath.Last() != initialNode && i != numVertices - 2) // check its not just the next in the path
            //        return false;   // F6
            //}
        }

        return true;
    }

    private bool DFSLoop(List<int> path)
    {
        List<int> visitedNodes = new List<int>();
        foreach (int node in path)
        {
            /*if (visitedNodes.Contains(node) && !(path.Count == numVertices + 1 && node == initialNode))*/
            if (visitedNodes.Contains(node) && !(node == initialNode))
                return true; // a loop is detected
            visitedNodes.Add(node);
        }
        return false;
    }

    //public bool EdgeRequired(int[] edge)
    //{
    //    foreach (int node in edge)
    //    {
    //        // Find how many edges it has leaving,  if 1 req
    //        // how many edges it has entering,      if 1 req
    //        // what its total degree is             if 2 req
    //        int edgesLeaving, edgesEntering, edgesTotal = 0;
    //        edgesLeaving = g.GetOutwardDegree(node);
    //        edgesEntering = g.GetInwardDegree(node);
    //        edgesTotal = edgesLeaving + edgesEntering;
    //        if (edgesLeaving == 1 || edgesEntering == 1 || edgesTotal == 2)
    //            return true;
    //    }
    //    return false;
    //}

    public bool EdgeRequired(int[] edge)
    {
        //int edgesEntering = g.GetInwardDegree(edge[0]);
        //int edgesLeaving = g.GetOutwardDegree(edge[1]);
        return g.GetInwardDegree(edge[1]) == 1;
    }

    public bool EdgeRequired(int u, int v)
    {
        // Handles R1 and R2. 
        return (g.GetInwardDegree(u) < 2 || g.GetOutwardDegree(u) < 2) || (g.GetInwardDegree(v) < 2 || g.GetOutwardDegree(v) < 2) || (g.GetDegree(u) < 2 || g.GetDegree(v) < 2);
        //return g.GetDegree(u) < 2 || g.GetDegree(v) < 2;
    }

    public void DeleteEdges(int vertex) // Deciding for vertex
    {
        List<int[]> edgesToDelete = new List<int[]>();
        List<int[]> requiredEdgesOfNode = new List<int[]>();
        List<int[]> undecidedEdges = GetAllUndecidedOfNode(vertex);
        foreach (int[] edge in required)
        {
            if (edge[0] == vertex || edge[1] == vertex) { 
                requiredEdgesOfNode.Add(edge);
                //bool edgeIn = undecidedEdges.Contains(edge);
                //bool result = undecidedEdges.Remove(edge);
                RemoveFromList(undecidedEdges, edge);
            }
        }

        // D1. If a vertex has two required arcs incident, then all undecided arcs incident may be deleted. 
        if (requiredEdgesOfNode.Count == 2)
        {
            //for (int i=0;i<undecidedEdges.Count;i++)
            //    edgesToDelete.Add(undecidedEdges[i]);
            foreach (int[] edge in undecidedEdges)
                edgesToDelete.Add(edge);
        } 
        else
        {
            // D2. If a vertex has a required directed arc entering(leaving), then all undecided directed arcs entering(leaving) may be deleted.
            bool[] deleteSimilarEdges = { false, false };
            foreach (int[] reqEdge in requiredEdgesOfNode)
            {
                if (reqEdge[0] == vertex) // Delete all other outgoing vertices
                    deleteSimilarEdges[0] = true;
                if (reqEdge[1] == vertex) // Delete all other incoming vertives
                    deleteSimilarEdges[1] = true;
            }
            foreach (int[] edge in undecidedEdges)
            {
                if (edge[0] == vertex && deleteSimilarEdges[0])
                    edgesToDelete.Add(edge);
                if (edge[1] == vertex && deleteSimilarEdges[1])
                    edgesToDelete.Add(edge);
            }
        }

        // D3. Delete any arc which forms a closed circuit with required arcs, unless it completes the Hamilton circuit
        // if the arc connects to any node already on the path, delete it. Unless, it completes the circuit. 
        foreach (int[] edge in undecidedEdges)//undecided
        {
            if (edge[1] == initialNode)//partialPath.Count == numVertices - 1 && 
                continue;
            for (int i = 0; i < partialPath.Count; i++)
            {
                if (i == partialPath.Count - 1)
                    continue;
                if (edge[1] == partialPath[i])
                    edgesToDelete.Add(edge);
            }
        }

        // Finished, now remove the collated list. 
        for (int i = 0; i < edgesToDelete.Count; i++)
        {
            deleted.Add(edgesToDelete[i]);
            //undecided.Remove(edgesToDelete[i]);
            RemoveFromList(undecided, edgesToDelete[i]);
            g.RemoveEdgeDirected(edgesToDelete[i]);
        }
    }

    public void AssignDirection(int node)
    {
        // get all undecided vertices of node
        // get required nodes about node
        // if has a required node in one direction, reform all bidirectional nodes into just opposing required direction. 
        bool hasRequiredEntering = false;
        bool hasRequiredLeaving  = false;
        foreach (int[] reqEdge in required)
        {
            if (reqEdge[0] == node)
                hasRequiredLeaving = true;
            if (reqEdge[1] == node)
                hasRequiredEntering = true;
        }
        List<int[]> edgesToDelete = new List<int[]>();
        List<int[]> localUndecided = GetAllUndecidedOfNode(node);
        foreach (int[] edge in localUndecided)
        {
            if (hasRequiredEntering && edge[1] == node)
            {
                if (g.UVBiDirectional(edge[0], edge[1]))
                    edgesToDelete.Add(new int[] { edge[0], edge[1] });
            }
            if (hasRequiredLeaving && edge[0] == node)
            {
                if (g.UVBiDirectional(edge[0], edge[1]))
                    edgesToDelete.Add(new int[] { edge[1], edge[0] });
            }
        }
        // carry out deletions 
        for (int i = 0; i < edgesToDelete.Count; i++)
        {
            deleted.Add(edgesToDelete[i]);
            //undecided.Remove(edgesToDelete[i]);
            RemoveFromList(undecided, edgesToDelete[i]);
            g.RemoveEdgeDirected(edgesToDelete[i]);
        }
    }

    public List<int[]> GetAllUndecidedOfNode(int vertex)
    {
        List<int[]> ret = new List<int[]>();
        foreach (int[] edge in undecided)
            if (edge[0] == vertex || edge[1] == vertex)
                ret.Add(edge);
        return ret;
    }

    // Forgive me for this terrible function, this is what C# should be doing, but it's actually comparing pointers. 
    public void RemoveFromList(List<int[]> list, int[] item)
    {
        int index = -1;
        for (int i=0; i < list.Count; i++)
        {
            if (list[i][0] == item[0] && list[i][1] == item[1])
            {
                index = i;
                break;
            }
        }
        if (index != -1)
            list.RemoveAt(index);
    }

    public bool ListContains(List<int[]> list, int[] edge)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i][0] == edge[0] || list[i][1] == edge[1])
            {
                return true;
            }
        }
        return false;
    }

    public Solution HasHamiltonCycle(AdjGraph original) { return HasHamiltonCycle(original, 0); }

}
