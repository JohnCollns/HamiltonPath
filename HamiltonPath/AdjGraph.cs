using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// A better data structure would have been to have Nodes, each with a list of incoming and outgoing nodes. 
// There is a great deal of two way traversal in the search, and that structure would have handled it better. 
public class AdjGraph
{
    public int numVertices { get; }
    public List<List<int>> edges;

    public AdjGraph(int numVertices)
    {
        this.numVertices = numVertices;
        edges = new List<List<int>>();
        for (int i = 0; i < numVertices; i++)
        {
            edges.Add(new List<int>());
            edges[i] = new List<int>();
        }
    }

    public AdjGraph(AdjGraph original)
    {
        numVertices = original.numVertices;
        edges = new List<List<int>>();
        for (int i = 0; i < numVertices; i++)
        {
            edges.Add(new List<int>());
            for (int j = 0; j < original.edges[i].Count; j++)
                edges[i].Add(original.edges[i][j]);
        }
    }

    public void AddEdgeDirected(int entering, int leaving)
    {   // O(E)
        if (UVExist(entering, leaving) || entering == leaving)
            return;
        edges[entering].Add(leaving);
    }

    public void AddEdgeUni(int entering, int leaving)
    {
        AddEdgeDirected(entering, leaving);
        AddEdgeDirected(leaving,  entering);
    }

    public void RemoveEdgeDirected(int entering, int leaving)
    {   // O(E)
        for (int i=0; i < edges[entering].Count; i++)
        {
            if (edges[entering][i] == leaving)
            {
                edges[entering].RemoveAt(i);
                return;
            }
        }
    }

    public void RemoveEdgeDirected(int[] edge) { RemoveEdgeDirected(edge[0], edge[1]); }

    public void RemoveEdgeUni(int entering, int leaving)
    {   // O(2E)
        RemoveEdgeDirected(entering, leaving);
        RemoveEdgeDirected(leaving,  entering);
    }

    public int GetOutwardDegree(int vertex) { return edges[vertex].Count; } // O(1)

    public int GetDegree(int vertex)
    {
        int inwardCount = 0;
        for (int i=0; i < numVertices; i++)
            for (int j=0; j < edges[i].Count; j++)
                inwardCount += (edges[i][j] == vertex) ? 1 : 0;
        for (int i = 0; i < edges[vertex].Count; i++)   // Decrement for any bidirectional edges, so they do not count for two edges. 
            inwardCount -= (UVBiDirectional(vertex, edges[vertex][i])) ? 1 : 0;
        return inwardCount + edges[vertex].Count;
    }

    public int GetInwardDegree(int vertex)
    {
        int inwardCount = 0;
        for (int i = 0; i < numVertices; i++)
            for (int j = 0; j < edges[i].Count; j++)
                inwardCount += (edges[i][j] == vertex) ? 1 : 0;
        return inwardCount;
    }

    public bool UVExist(int u, int v)
    {   // O(E)
        for (int i=0; i < edges[u].Count; i++)
        {
            if (edges[u][i] == v)
                return true;
        }
        return false;
    }

    public bool UVBiDirectional(int u, int v)
    {   // O(2E)
        return UVExist(u, v) && UVExist(v, u);
    }

    public int FindIsolatedVertex() // F1
    {
        //List<int> verticesWithOutwardEdges = new List<int>();
        //int[] verticesNoLeavingEdge = new int[numVertices];
        List<int> verticesNoLeavingEdge = new List<int>();
        bool[] verticesEnteringEdge = new bool[numVertices];
        for (int i=0; i < edges.Count; i++)
        {
            if (edges[i].Count == 0)
                verticesNoLeavingEdge.Add(i);
            for (int j=0; j < edges[i].Count; j++)
            {
                verticesEnteringEdge[edges[i][j]] = true;
            }
        }
        for (int i=0; i<verticesNoLeavingEdge.Count; i++)
        {
            if (!verticesEnteringEdge[verticesNoLeavingEdge[i]])
                return verticesNoLeavingEdge[i];
        }
        return -1;
    }

    public bool TerminateF1F2F3()
    {   // O(VE)
        bool[] hasEntering = new bool[numVertices];
        bool[] hasLeaving  = new bool[numVertices];
        for (int i=0; i < edges.Count; i++)
        {
            if (edges[i].Count == 0)
                return true;
            hasLeaving[i] = true;
            for (int j = 0; j < edges[i].Count; j++)
            {
                hasEntering[edges[i][j]] = true;
            }
        }
        for (int i=0; i < numVertices; i++)
        {
            if (!(hasEntering[i] || hasLeaving[i]))
                return true;
        }
        return false;
    }

    public List<int[]> GetAllEdges()
    {   // O(E)
        List<int[]> retEdges = new List<int[]>();
        for (int i=0; i < edges.Count; i++)
            for (int j=0; j < edges[i].Count; j++)
                retEdges.Add(new int[2] { i, edges[i][j] });
        return retEdges;
    }

    public List<int[]> GetAllEdgesOfNode(int vertex)
    {   // O(E)
        List<int[]> retEdges = new List<int[]>();
        for (int i = 0; i < edges.Count; i++)
            for (int j = 0; j < edges[i].Count; j++)
                if (i == vertex || j == vertex)
                    retEdges.Add(new int[2] { i, edges[i][j] });
        return retEdges;
    }

    public List<int> GetAllOutwardEdgesOfNode(int vertex) { return edges[vertex]; }

    public void PrintEdges()
    {
        Console.WriteLine("Node x has edges going to: [y,z,w,...]");
        for (int i=0; i<edges.Count; i++)
        {
            Console.Write(i + ": ");
            for (int j = 0; j < edges[i].Count; j++)
                Console.Write(edges[i][j] + ", ");
            Console.WriteLine();
        }
    }
}

//public class Edge
//{
//    public int origin { get; }
//    public int destination { get; }
//    public Edge(int u, int v)
//    {
//        origin = u;
//        destination = v;
//    }
//}