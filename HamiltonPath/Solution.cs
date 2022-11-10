using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public struct Solution
{
    public bool hasHamiltonCycle;
    public List<int>? solutionPath;

    public Solution(bool pass, List<int>? path)
    {
        hasHamiltonCycle = pass;
        solutionPath = path;
    }

    public static Solution Fail => new Solution(false, null);
}
