using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    public bool HasHamiltonCycle(AdjGraph original, int initialNode)
    {
        g = new AdjGraph(original);
        numVertices = original.numVertices;
        partialPath = new List<int>();
        required    = new List<int[]>();
        deleted     = new List<int[]>();
        undecided   = original.GetAllEdges();

        int searchIndex = 0;
        Queue<int>[] potentialNodes = new Queue<int>[original.numVertices];
        for (int i=0; i < original.numVertices; i++)
            potentialNodes[i] = new Queue<int>();
        while (partialPath.Count < numVertices)
        {
            // S4. Else if path is inadmissible, delete last node and choose next from list. 

            // S5. If list empty, delete last node prior to list. 

            // S6. If all extensions from a node a inadmissable, return false. 

            // S7. If a successor of the last node is origin, return true. 

            if (PathAdmissible())   // S2. Test path for admissibility
            {
                // S3. If path admissible, list successors of last node and extend path to first of these. 
                Queue<int> successors = new Queue<int>();
                for (int i=0;i< partialPath.Count; i++)
                    successors.Enqueue(partialPath[i]);

                while (successors.Count > numVertices)
                {
                    partialPath.Add(successors.Dequeue());
                    if (PathAdmissible())
                    {
                        continue; // probably incorrect
                    }
                    else// S4. Else if path is inadmissible, delete last node and choose next from list. 
                        partialPath.RemoveAt(partialPath.Count - 1);
                }

                // S5. If list empty, delete last node prior to list. 
                partialPath.RemoveAt(partialPath.Count - 1);

            }

            // Iterative plan. 
            // Make an array of int queues length numVertices. 
            // When a node is accepted, increment the index to the array of queues. 
            // When the queue at a point is emptied, decrement this index. 
            // If a successor is the initial node, and the array of queues index is numVertices return true. 
            if (searchIndex < 0)
                return false;

        }

        return false;
    }

    public bool PathAdmissible()
    {
        return false;
    }

    public bool EdgeRequired(int u, int v)
    {
        // Handles R1 and R2. 
        return g.GetDegree(u) < 2 || g.GetDegree(v) < 2;
    }

    public void DeleteEdges(int vertex) // Deciding for vertex
    {
        List<int[]> edgesToDelete = new List<int[]>();
        List<int[]> requiredEdges = new List<int[]>();
        List<int[]> undecidedEdges = GetAllUndecidedOfNode(vertex);
        foreach (int[] edge in required)
        {
            if (edge[0] == vertex || edge[1] == vertex)
                requiredEdges.Add(edge);
        }

        // D1. If a vertex has two required arcs incident, then all undecided arcs incident may be deleted. 
        if (requiredEdges.Count == 2)
        {
            for (int i=0;i<undecidedEdges.Count;i++)
                edgesToDelete.Add(undecidedEdges[i]);
        } 
        else
        {
            // D2. If a vertex has a required directed arc entering(leaving), then all undecided directed arcs entering(leaving) may be deleted.
            bool[] deleteSimilarEdges = { false, false };
            foreach (int[] reqEdge in requiredEdges)
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
        // I don't know what this means

        // Finished, now remove the collated list. 
        for (int i = 0; i < edgesToDelete.Count; i++)
        {
            deleted.Add(edgesToDelete[i]);
            undecided.Remove(edgesToDelete[i]);
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

    public bool HasHamiltonCycle(AdjGraph original) { return HasHamiltonCycle(original, 0); }
}
