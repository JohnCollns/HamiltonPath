using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class RubinSearch
{
    #region Rules
    /* 
    Search Rules
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
F1. Fail if any vertex becomes isolated, t h a t is, has no incident arc.
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

    public bool HasHamiltonCycle(AdjGraph g, int initialIndex)
    {


        return false;
    }

    public bool HasHamiltonCycle(AdjGraph g) { return HasHamiltonCycle(g, 0); }
}
