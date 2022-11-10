using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DFSHamilton
{
    public static Solution HasHamiltonPath(AdjGraph original, int initialNode)
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

        while (partialPath.Count <= numVertices)
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
            if (partialPath.Contains(partialPath.Last()) && partialPath.Last() != initialNode && partialPath.Count != numVertices - 1)
            {   // Illegal cycle detected
                failVertex = true;
            }
            if (partialPath.Count >= numVertices && partialPath.Last() != initialNode)
                failVertex = true;
            if (failVertex)
            {
                partialPath.RemoveAt(partialPath.Count - 1);
                searchIndex--;
                continue;
            }

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
            partialPath.Add(potentialNodes[searchIndex].Dequeue()); // And extend path to first of these
            searchIndex++;

        }
        return Solution.Fail;
    }
}
