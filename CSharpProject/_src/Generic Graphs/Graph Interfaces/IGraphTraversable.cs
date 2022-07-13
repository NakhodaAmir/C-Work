namespace MirJan
{
    namespace GenericGraphs
    {
        namespace GraphTraversal
        {
            using System.Collections.Generic;

            public interface IGraphTraversable<Node, Type> where Node : GraphTraversableNode<Type>
            {
                public List<Node> GetNeighbourNodes(Node node);
            }
        }
    }
}
