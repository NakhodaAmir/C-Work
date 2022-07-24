namespace MirJan
{
    namespace GenericGraphs
    {
        namespace GraphSearch
        {
            public interface IGraphSearchable<Node, Type> : GraphTraversal.IGraphTraversable<Node, Type> where Node : GraphSearchableNode<Type>
            {
                public float NodeTraversalCost(Type valueA, Type valueB);

                public float HeuristicCost(Type valueA, Type valueB);
            }
        }
    }
}