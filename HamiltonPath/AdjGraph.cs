using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        edges = new List<List<int>>(numVertices);
        for (int i = 0; i < numVertices; i++)
        {
            edges[i] = new List<int>();
            for (int j = 0; j < original.edges[i].Count; j++)
                edges[i].Add(original.edges[i][j]);
        }
    }

    public void AddEdgeDirected(int entering, int leaving)
    {
        if (UVExist(entering, leaving))
            return;
        edges[entering].Add(leaving);
    }

    public void AddEdgeUni(int entering, int leaving)
    {
        AddEdgeDirected(entering, leaving);
        AddEdgeDirected(leaving,  entering);
    }

    public void RemoveEdgeDirected(int entering, int leaving)
    {
        for (int i=0; i < edges[entering].Count; i++)
        {
            if (edges[entering][i] == leaving)
            {
                edges[entering].RemoveAt(i);
                return;
            }
        }
    }

    public void RemoveEdgeUni(int entering, int leaving)
    {
        RemoveEdgeDirected(entering, leaving);
        RemoveEdgeDirected(leaving,  entering);
    }

    public int GetOutwardDegree(int vertex) { return edges[vertex].Count; }

    public bool UVExist(int u, int v)
    {
        for (int i=0; i < edges[u].Count; i++)
        {
            if (edges[u][i] == v)
                return true;
        }
        return false;
    }

    public bool UVBiDirectional(int u, int v)
    {
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

    public bool TerminateF1F2()
    {
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
    {
        List<int[]> retEdges = new List<int[]>();
        for (int i=0; i < edges.Count; i++)
            for (int j=0; j < edges[i].Count; j++)
                retEdges.Add(new int[2] { i, edges[i][j] });
        return retEdges;
    }

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