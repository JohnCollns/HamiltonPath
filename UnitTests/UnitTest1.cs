namespace UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Assert.Equal(1, 1);
        }
    }

    public class GraphTests
    {
        [Fact]
        public void AddDirectedEdge()
        {
            AdjGraph g = new AdjGraph(2);
            g.AddEdgeDirected(0, 1);
        }
        [Fact]
        public void AddUniEdge()
        {
            AdjGraph g = new AdjGraph(2);
            g.AddEdgeUni(0, 1);
        }
        [Fact]
        public void AddFewEdges()
        {
            AdjGraph g = new AdjGraph(4);
            g.AddEdgeDirected(0, 1);
            g.AddEdgeDirected(1, 2);
            g.AddEdgeDirected(1, 0);
            g.AddEdgeDirected(3, 2);
            g.AddEdgeDirected(0, 3);
        }
        [Fact]
        public void HasEdgeWorks()
        {
            AdjGraph g = new AdjGraph(2);
            g.AddEdgeUni(0, 1);
            Assert.True(g.UVExist(0, 1));
        }
        [Fact]
        public void HasEdgeWorksAfterDelete()
        {
            AdjGraph g = new AdjGraph(2);
            g.AddEdgeUni(0, 1);
            g.RemoveEdgeDirected(0, 1);
            Assert.False(g.UVExist(0, 1));
        }
        [Fact]
        public void HasEdgeBidirectional()
        {
            AdjGraph g = new AdjGraph(2);
            g.AddEdgeUni(0, 1);
            g.AddEdgeUni(1, 0);
            Assert.True(g.UVBiDirectional(0, 1));
        }
        [Fact]
        public void DeleteEdgeWorking()
        {
            AdjGraph g = new AdjGraph(2);
            g.AddEdgeUni(0, 1);
            g.AddEdgeUni(1, 0);
            g.RemoveEdgeDirected(0, 1);
            Assert.False(g.UVExist(0, 1));
        }
        [Fact]
        public void FindsIsolateIn1Graph()
        {
            var g = new AdjGraph(1);
            Assert.Equal(0, g.FindIsolatedVertex());
        }
        [Fact]
        public void FindsIsolateInSmallGraph()
        {
            AdjGraph g = new AdjGraph(5);
            g.AddEdgeDirected(0, 1);
            g.AddEdgeDirected(1, 2);
            g.AddEdgeDirected(1, 0);
            g.AddEdgeDirected(3, 2);
            g.AddEdgeDirected(0, 3);
            Assert.Equal(4, g.FindIsolatedVertex());
        }
        [Fact]
        public void DoNotFindIsolate()
        {
            AdjGraph g = new AdjGraph(2);
            g.AddEdgeUni(0, 1);
            g.AddEdgeUni(1, 0);
            Assert.Equal(-1, g.FindIsolatedVertex());
        }
        [Fact]
        public void TerminateF1F2In1Graph()
        {
            var g = new AdjGraph(1);
            Assert.True(g.TerminateF1F2F3());
        }
        [Fact]
        public void TerminateF1F2InSmallGraph()
        {
            AdjGraph g = new AdjGraph(5);
            g.AddEdgeDirected(0, 1);
            g.AddEdgeDirected(1, 2);
            g.AddEdgeDirected(1, 0);
            g.AddEdgeDirected(3, 2);
            g.AddEdgeDirected(0, 3);
            Assert.True(g.TerminateF1F2F3());
        }
        [Fact]
        public void TerminateF1F2InArray()
        {
            var g = new AdjGraph(3);
            g.AddEdgeDirected(0, 1);
            g.AddEdgeDirected(1, 2);
            Assert.True(g.TerminateF1F2F3());
        }
        [Fact]
        public void TerminateF1F2InTree()
        {
            var g = new AdjGraph(5);
            g.AddEdgeDirected(0, 1);
            g.AddEdgeDirected(1, 2);
            g.AddEdgeDirected(0, 3);
            g.AddEdgeDirected(3, 4);
            Assert.True(g.TerminateF1F2F3());
        }
        [Fact]
        public void DoNotTerminateF1F2InSmallGraph()
        {
            AdjGraph g = new AdjGraph(5);
            g.AddEdgeDirected(0, 1);
            g.AddEdgeDirected(1, 2);
            g.AddEdgeDirected(1, 0);
            g.AddEdgeDirected(3, 2);
            g.AddEdgeDirected(0, 3);
            g.AddEdgeUni(4, 0);
            g.AddEdgeDirected(2,3);
            Assert.False(g.TerminateF1F2F3());
        }
        [Fact]
        public void GetAllEdgesCorrectLength1()
        {
            var g = new AdjGraph(5);
            g.AddEdgeDirected(0, 1);
            g.AddEdgeDirected(1, 2);
            g.AddEdgeDirected(0, 3);
            g.AddEdgeDirected(3, 4);
            Assert.Equal(4, g.GetAllEdges().Count);
        }
        [Fact]
        public void GetAllEdgesCorrectLength2()
        {
            var g = new AdjGraph(5);
            g.AddEdgeDirected(0, 1);
            g.AddEdgeUni(1, 2);
            g.AddEdgeUni(0, 3);
            g.AddEdgeDirected(3, 4);
            Assert.Equal(6, g.GetAllEdges().Count);
        }
        [Fact]
        public void DegreeCorrect0()
        {
            var g = new AdjGraph(1);
            Assert.Equal(0, g.GetDegree(0));
        }
        [Fact]
        public void DegreeCorrect1()
        {
            var g = new AdjGraph(2);
            g.AddEdgeDirected(0, 1);
            Assert.Equal(1, g.GetDegree(0));
        }
        [Fact]
        public void DegreeCorrect2()
        {
            var g = new AdjGraph(2);
            g.AddEdgeDirected(0, 1);
            g.AddEdgeDirected(1, 0);
            Assert.Equal(1, g.GetDegree(0));
        }
        [Fact]
        public void DegreeCorrect3()
        {
            var g = new AdjGraph(3);
            g.AddEdgeDirected(0, 1);
            g.AddEdgeUni(1, 2);
            Assert.Equal(2, g.GetDegree(1));
        }
        [Fact]
        public void DegreeCorrect4()
        {
            var g = new AdjGraph(3);
            g.AddEdgeDirected(0, 1);
            g.AddEdgeUni(1, 2);
            Assert.Equal(1, g.GetDegree(2));
        }
        [Fact]
        public void DegreeCorrect5()
        {
            var g = new AdjGraph(5);
            g.AddEdgeUni(0, 1);
            g.AddEdgeUni(0, 2);
            g.AddEdgeDirected(0, 3);
            g.AddEdgeDirected(4, 0);
            Assert.Equal(4, g.GetDegree(0));
        }
        [Fact]
        public void DegreeCorrect6()
        {
            var g = new AdjGraph(3);
            g.AddEdgeDirected(0, 1);
            g.AddEdgeUni(1, 2);
            Assert.Equal(2, g.GetDegree(1));
        }
    }

    public class HamiltonCircuitTests
    {
        [Fact]
        public void SimpleTriangle()
        {
            AdjGraph g = new AdjGraph(3);
            g.AddEdgeDirected(0, 1);
            g.AddEdgeDirected(1, 2);
            g.AddEdgeDirected(2, 0);
            RubinSearch search = new RubinSearch();
            Assert.True(search.HasHamiltonCycle(g));
        }
        [Fact]
        public void SimpleUniTriangle()
        {
            AdjGraph g = new AdjGraph(3);
            g.AddEdgeUni(0, 1);
            g.AddEdgeUni(1, 2);
            g.AddEdgeUni(2, 0);
            RubinSearch search = new RubinSearch();
            Assert.True(search.HasHamiltonCycle(g));
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
            RubinSearch search = new RubinSearch();
            Assert.False(search.HasHamiltonCycle(g));
        }

    }
}