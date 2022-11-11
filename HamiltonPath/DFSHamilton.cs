using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DFSHamilton
{
    public static Solution HasHamiltonCycle(AdjGraph original, int initialNode)
    {
        AdjGraph g = new AdjGraph(original);
        int numVertices = g.numVertices;
        int searchIndex = 0;
        List<int> partialPath = new List<int>();
        partialPath.Add(initialNode);
        Queue<int>[] potentialNodes = new Queue<int>[original.numVertices];
        for (int i = 0; i < original.numVertices; i++)
            potentialNodes[i] = new Queue<int>();

        foreach (int outgoingEdge in g.GetAllOutwardEdgesOfNode(partialPath[searchIndex]))
            potentialNodes[searchIndex].Enqueue(outgoingEdge);

        while (partialPath.Count <= numVertices + 1)
        {
            if (searchIndex < 0)                    // Check failure
                return Solution.Fail;

            if (searchIndex == g.numVertices - 1)   // Check success
                foreach (int outgoingEdge in g.GetAllOutwardEdgesOfNode(partialPath[searchIndex]))
                    if (outgoingEdge == initialNode)
                        return new Solution(true, partialPath);

            // check for failure
            // created an incorrect cycle in partial path
            // reached limit
            // if failure, decrement searchIndex, pop from queue and continue
            bool failVertex = false;
            do
            {
                List<int> retrace = new List<int>();
                for (int i = 0; i < partialPath.Count; i++)
                {
                    bool result = retrace.Contains(partialPath[i]);
                    if (retrace.Contains(partialPath[i]))
                    {  // && partialPath[i] != initialNode && partialPath.Count != numVertices + 1
                        if (partialPath[i] == initialNode && partialPath.Count == numVertices + 1)
                            return new Solution(true, partialPath);
                        else
                        {
                            failVertex = true;
                            break;
                        }
                    }
                    retrace.Add(partialPath[i]);
                }

                if (failVertex)
                {
                    partialPath.RemoveAt(partialPath.Count - 1);
                    searchIndex--;
                    if (potentialNodes[searchIndex].Count > 0)
                    {
                        partialPath.Add(potentialNodes[searchIndex].Dequeue()); // And extend path to first of these
                        if (retrace.Contains(partialPath.Last())) {
                            continue;
                        }
                        searchIndex++;
                        failVertex = false;
                    }
                    else
                    {
                        partialPath.RemoveAt(partialPath.Count - 1);
                        searchIndex--;
                    }
                }
            } while (failVertex);
            //if (partialPath.Contains(partialPath.Last()) && partialPath.Last() != initialNode && partialPath.Count != numVertices - 1)
            //{   // Illegal cycle detected
            //    failVertex = true;
            //}
            if (partialPath.Count >= numVertices && partialPath.Last() != initialNode)
                failVertex = true;
            

            // List successors
            potentialNodes[searchIndex] = new Queue<int>();
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
            if (potentialNodes[searchIndex].Count > 0)
            {
                partialPath.Add(potentialNodes[searchIndex].Dequeue()); // And extend path to first of these
                searchIndex++;
            }
            else
            {
                while (potentialNodes[searchIndex].Count == 0)
                {
                    partialPath.RemoveAt(partialPath.Count - 1);
                    searchIndex--;
                    if (searchIndex < 0)
                        return Solution.Fail;
                }
            }

        }
        return Solution.Fail;
    }

    public static Solution HasHamiltonPath(AdjGraph original, int initialVertex)
    {
        AdjGraph g = new AdjGraph(original);
        int newNode = g.AddNode();
        // Add a vertex that connects to all other vertices to use a cycle test to determine if a path exists. 
        for (int i=0; i < newNode - 2; i++)
        {
            g.AddEdgeUni(i, newNode - 1);
        }

        Solution sol = HasHamiltonCycle(g, initialVertex);
        if (sol.hasHamiltonCycle)
        {
            sol.solutionPath.RemoveAt(sol.solutionPath.Count - 1);
            sol.solutionPath.RemoveAt(sol.solutionPath.Count - 1);
            return sol;
        }
        else // If it does not have a hamilton path from this vertex, it may from the next, and so on. Try all initial vertices
        {
            initialVertex++;
            while (initialVertex < g.numVertices - 1)
            {
                sol = HasHamiltonCycle(g, initialVertex);
                if (sol.hasHamiltonCycle)
                {
                    sol.solutionPath.RemoveAt(sol.solutionPath.Count - 1);
                    sol.solutionPath.RemoveAt(sol.solutionPath.Count - 1);
                    return sol;
                }
                initialVertex++;
            }
        }
        return Solution.Fail;
    }
}
