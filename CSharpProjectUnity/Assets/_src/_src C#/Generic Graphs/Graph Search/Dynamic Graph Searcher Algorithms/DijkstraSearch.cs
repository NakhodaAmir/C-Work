namespace MirJan
{
    namespace GenericGraphs
    {
        namespace GraphSearch
        {
            namespace DynamicGraphSearch
            {
                public class DijkstraSearch<Node, Type> : DynamicGraphSearcher<Node, Type> where Node : GraphSearchableNode<Type>
                {
                    public DijkstraSearch(IGraphSearchable<Node, Type> graph, bool retracePathInclSource = true) : base(graph, retracePathInclSource) { }

                    protected override void AlgorithmSpecificImplementation(Node neighbour)
                    {
                        float gCost = CurrentNode.GCost + graph.NodeTraversalCost(CurrentNode.Location.Value, neighbour.Value);
                        float hCost = 0;

                        AlgorithmCommonImplementation(neighbour, gCost, hCost);
                    }
                }
            }
        }
    }
}
