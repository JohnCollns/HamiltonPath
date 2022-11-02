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
        edges = new List<List<int>>(numVertices);
        for (int i = 0; i < numVertices; i++)
            edges[i] = new List<int>();
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

    public int GetDegree(int vertex) { return edges[vertex].Count; }
}
