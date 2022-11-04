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
            Assert.True(g.TerminateF1F2());
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
            Assert.True(g.TerminateF1F2());
        }
        [Fact]
        public void TerminateF1F2InArray()
        {
            var g = new AdjGraph(3);
            g.AddEdgeDirected(0, 1);
            g.AddEdgeDirected(1, 2);
            Assert.True(g.TerminateF1F2());
        }
        [Fact]
        public void TerminateF1F2InTree()
        {
            var g = new AdjGraph(5);
            g.AddEdgeDirected(0, 1);
            g.AddEdgeDirected(1, 2);
            g.AddEdgeDirected(0, 3);
            g.AddEdgeDirected(3, 4);
            Assert.True(g.TerminateF1F2());
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
            Assert.False(g.TerminateF1F2());
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
    }
}