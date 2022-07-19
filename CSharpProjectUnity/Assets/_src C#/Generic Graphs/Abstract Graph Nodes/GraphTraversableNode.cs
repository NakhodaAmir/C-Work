namespace MirJan
{
    namespace GenericGraphs
    {
        namespace GraphTraversal
        {
            abstract public class GraphTraversableNode<Type>
            {
                public Type Value { get; private set; }

                public GraphTraversableNode(Type value)
                {
                    Value = value;
                }
            }
        }
    }
}