namespace MirJan
{
    namespace GenericGraphs
    {
        namespace GraphSearch
        {
            namespace DynamicGraphSearch
            {
                public class AStarSearch<Node, Type> : DynamicGraphSearcher<Node, Type> where Node : GraphSearchableNode<Type>
                {
                    public AStarSearch(IGraphSearchable<Node, Type> graph) : base(graph) { }

                    protected override void AlgorithmSpecificImplementation(Node neighbour)
                    {
                        float gCost = CurrentNode.GCost + graph.NodeTraversalCost(CurrentNode.Location.Value, neighbour.Value);
                        float hCost = graph.HeuristicCost(TargetNode.Value, neighbour.Value);

                        AlgorithmCommonImplementation(neighbour, gCost, hCost);
                    }
                }
            }
        }
    }
}