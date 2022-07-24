namespace MirJan
{
    namespace GenericGraphs
    {
        namespace GraphSearch
        {
            namespace DynamicGraphSearch
            {
                public class GreedyBestFirstSearch<Node, Type> : DynamicGraphSearcher<Node, Type> where Node : GraphSearchableNode<Type>
                {
                    public GreedyBestFirstSearch(IGraphSearchable<Node, Type> graph, bool retracePathInclSource = true) : base(graph, retracePathInclSource) { }

                    protected override void AlgorithmSpecificImplementation(Node neighbour)
                    {
                        float gCost = 0;
                        float hCost = graph.HeuristicCost(TargetNode.Value, neighbour.Value);

                        AlgorithmCommonImplementation(neighbour, gCost, hCost);
                    }
                }
            }
        }
    }
}
