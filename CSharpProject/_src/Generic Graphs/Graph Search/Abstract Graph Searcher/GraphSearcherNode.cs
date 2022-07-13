namespace MirJan
{
    namespace GenericGraphs
    {
        namespace GraphSearch
        {
            using MirJan.PriorityQueues;
            public class GraphSearcherNode<Type> : IPairingHeap<GraphSearcherNode<Type>>
            {
                public GraphSearchableNode<Type> Location { get; set; }
                public GraphSearcherNode<Type> Parent { get; set; }
                public float FCost { get { return GCost + HCost; } }
                public float GCost { get; set; }
                public float HCost { get; set; }
                public PairingHeap<GraphSearcherNode<Type>>.PairingHeapNode HeapNode { get; set; }

                public GraphSearcherNode(GraphSearchableNode<Type> location, GraphSearcherNode<Type> parent, float gCost, float hCost)
                {
                    SetProperties(location, parent, gCost, hCost);
                }

                public int CompareTo(GraphSearcherNode<Type> other)
                {
                    int compare = FCost.CompareTo(other.FCost);

                    if (compare == 0) compare = HCost.CompareTo(other.HCost);

                    return compare;
                }

                public void SetProperties(GraphSearchableNode<Type> location, GraphSearcherNode<Type> parent, float gCost, float hCost)
                {
                    Location = location;
                    Parent = parent;
                    GCost = gCost;
                    HCost = hCost;

                    Location.GraphSearcherNode = this;
                }
            }
        }
    }
}
