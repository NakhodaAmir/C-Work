namespace MirJan
{
    namespace GenericGraphs
    {
        namespace GraphSearch
        {
            abstract public class GraphSearchableNode<Type> : GraphTraversal.GraphTraversableNode<Type>
            {
                public GraphSearcherNode<Type> GraphSearcherNode { get; set; }

                public GraphSearchableNode(Type value, float pCost = 0) : base(value)
                {
                    GraphSearcherNode = new GraphSearcherNode<Type>(this, null, 0, 0, pCost);
                }
            }
        }
    }
}

