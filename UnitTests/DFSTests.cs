using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class DFSTests
    {
        [Fact]
        public void SimpleTriangleDirected()
        {
            AdjGraph g = new AdjGraph(3);
            g.AddEdgeDirected(0, 1);
            g.AddEdgeDirected(1, 2);
            g.AddEdgeDirected(2, 0);
            Assert.True(DFSHamilton.HasHamiltonCycle(g,0).hasHamiltonCycle);
        }
        [Fact]
        public void SimpleTriangleUni()
        {
            AdjGraph g = new AdjGraph(3);
            g.AddEdgeUni(0, 1);
            g.AddEdgeUni(1, 2);
            g.AddEdgeUni(2, 0);
            Assert.True(DFSHamilton.HasHamiltonCycle(g, 0).hasHamiltonCycle);
        }
        [Fact]
        public void LoopWithOneLeaf()
        {
            AdjGraph g = new AdjGraph(5);
            g.AddEdgeDirected(0, 1);
            g.AddEdgeDirected(1, 2);
            g.AddEdgeDirected(2, 3);
            g.AddEdgeDirected(3, 0);
            g.AddEdgeDirected(1, 4);
            Assert.False(DFSHamilton.HasHamiltonCycle(g, 0).hasHamiltonCycle);
        }
        [Fact]
        public void BoxWithCenterDir()
        {
            AdjGraph g = new AdjGraph(5);
            g.AddEdgeDirected(0, 1);
            g.AddEdgeDirected(1, 2); g.AddEdgeDirected(1, 4);
            g.AddEdgeDirected(2, 3);
            g.AddEdgeDirected(3, 0);
            g.AddEdgeDirected(4, 2);
            Assert.True(DFSHamilton.HasHamiltonCycle(g, 0).hasHamiltonCycle);
        }
        [Fact]
        public void BoxWithCenterUni()
        {
            AdjGraph g = new AdjGraph(5);
            g.AddEdgeUni(0, 1);
            g.AddEdgeUni(1, 2); g.AddEdgeUni(1, 4);
            g.AddEdgeUni(2, 3);
            g.AddEdgeUni(3, 0);
            g.AddEdgeUni(4, 2);
            Assert.True(DFSHamilton.HasHamiltonCycle(g, 0).hasHamiltonCycle);
        }
        [Fact]
        public void ComplexExampleUni1()
        {
            AdjGraph g = new AdjGraph(20);
            g.AddEdgeUni(0, 1); g.AddEdgeUni(0, 4); g.AddEdgeUni(0, 19);
            g.AddEdgeUni(1, 2); g.AddEdgeUni(1, 11);
            g.AddEdgeUni(2, 3); g.AddEdgeUni(2, 9);
            g.AddEdgeUni(3, 4); g.AddEdgeUni(3, 7);
            g.AddEdgeUni(4, 5);
            g.AddEdgeUni(5, 6); g.AddEdgeUni(5, 18);
            g.AddEdgeUni(6, 7); g.AddEdgeUni(6, 16);
            g.AddEdgeUni(7, 8);
            g.AddEdgeUni(8, 9); g.AddEdgeUni(8, 15);
            g.AddEdgeUni(9, 10);
            g.AddEdgeUni(10, 11); g.AddEdgeUni(10, 14);
            g.AddEdgeUni(11, 12);
            g.AddEdgeUni(12, 13); g.AddEdgeUni(12, 19);
            g.AddEdgeUni(13, 14); g.AddEdgeUni(13, 17);
            g.AddEdgeUni(14, 15);
            g.AddEdgeUni(15, 16);
            g.AddEdgeUni(16, 17);
            g.AddEdgeUni(17, 18);
            g.AddEdgeUni(18, 19);
            Solution sol = DFSHamilton.HasHamiltonCycle(g, 0);
            Assert.True(sol.hasHamiltonCycle);
            List<int> expected = new List<int>();
            for (int i = 0; i < 20; i++)
                expected.Add(i);
            expected.Add(0);
            Assert.Equal(expected, sol.solutionPath);
        }

        public class DFSPathTests
        {
            [Fact]
            public void SimpleTriangleDirected()
            {
                AdjGraph g = new AdjGraph(3);
                g.AddEdgeDirected(0, 1);
                g.AddEdgeDirected(1, 2);
                g.AddEdgeDirected(2, 0);
                Assert.True(DFSHamilton.HasHamiltonPath(g, 0).hasHamiltonCycle);
            }
            [Fact]
            public void SimpleTriangleUni()
            {
                AdjGraph g = new AdjGraph(3);
                g.AddEdgeUni(0, 1);
                g.AddEdgeUni(1, 2);
                g.AddEdgeUni(2, 0);
                Assert.True(DFSHamilton.HasHamiltonPath(g, 0).hasHamiltonCycle);
            }
            [Fact]
            public void LoopWithOneLeaf()
            {
                AdjGraph g = new AdjGraph(5);
                g.AddEdgeUni(0, 1);
                g.AddEdgeUni(1, 2);
                g.AddEdgeUni(2, 3);
                g.AddEdgeUni(3, 0);
                g.AddEdgeUni(1, 4);
                Assert.True(DFSHamilton.HasHamiltonPath(g, 0).hasHamiltonCycle);
            }
            [Fact]
            public void BoxWithCenterDir()
            {
                AdjGraph g = new AdjGraph(5);
                g.AddEdgeDirected(0, 1);
                g.AddEdgeDirected(1, 2); g.AddEdgeDirected(1, 4);
                g.AddEdgeDirected(2, 3);
                g.AddEdgeDirected(3, 0);
                g.AddEdgeDirected(4, 2);
                Assert.True(DFSHamilton.HasHamiltonPath(g, 0).hasHamiltonCycle);
            }
            [Fact]
            public void BoxWithCenterUni()
            {
                AdjGraph g = new AdjGraph(5);
                g.AddEdgeUni(0, 1);
                g.AddEdgeUni(1, 2); g.AddEdgeUni(1, 4);
                g.AddEdgeUni(2, 3);
                g.AddEdgeUni(3, 0);
                g.AddEdgeUni(4, 2);
                Assert.True(DFSHamilton.HasHamiltonPath(g, 0).hasHamiltonCycle);
            }
            [Fact]
            public void ComplexExampleUni1()
            {
                AdjGraph g = new AdjGraph(20);
                g.AddEdgeUni(0, 1); g.AddEdgeUni(0, 4); g.AddEdgeUni(0, 19);
                g.AddEdgeUni(1, 2); g.AddEdgeUni(1, 11);
                g.AddEdgeUni(2, 3); g.AddEdgeUni(2, 9);
                g.AddEdgeUni(3, 4); g.AddEdgeUni(3, 7);
                g.AddEdgeUni(4, 5);
                g.AddEdgeUni(5, 6); g.AddEdgeUni(5, 18);
                g.AddEdgeUni(6, 7); g.AddEdgeUni(6, 16);
                g.AddEdgeUni(7, 8);
                g.AddEdgeUni(8, 9); g.AddEdgeUni(8, 15);
                g.AddEdgeUni(9, 10);
                g.AddEdgeUni(10, 11); g.AddEdgeUni(10, 14);
                g.AddEdgeUni(11, 12);
                g.AddEdgeUni(12, 13); g.AddEdgeUni(12, 19);
                g.AddEdgeUni(13, 14); g.AddEdgeUni(13, 17);
                g.AddEdgeUni(14, 15);
                g.AddEdgeUni(15, 16);
                g.AddEdgeUni(16, 17);
                g.AddEdgeUni(17, 18);
                g.AddEdgeUni(18, 19);
                Solution sol = DFSHamilton.HasHamiltonPath(g, 0);
                Assert.True(sol.hasHamiltonCycle);
                List<int> expected = new List<int>();
                for (int i = 0; i < 20; i++)
                    expected.Add(i);
                expected.Add(0);
                Assert.Equal(expected, sol.solutionPath);
            }
        }
    }
}
