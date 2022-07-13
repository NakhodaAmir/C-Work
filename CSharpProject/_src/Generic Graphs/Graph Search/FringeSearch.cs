namespace MirJan
{
    namespace GenericGraphs
    {
        namespace GraphSearch
        {
            public class FringeSearch<Node, Type> : GraphSearcher<Node, Type> where Node : GraphSearchableNode<Type>
            {
                #region Properties
                public Dictionary<GraphSearcherNode<Type>, GraphSearcherNode<Type>> Cache { get; private set; }
                #endregion

                #region Variables
                protected LinkedList<GraphSearcherNode<Type>> fringeList;

                float fLimit;
                #endregion

                #region Constructor
                public FringeSearch(IGraphSearchable<Node, Type> graph) : base(graph)
                {
                    fringeList = new LinkedList<GraphSearcherNode<Type>>();

                    Cache = new Dictionary<GraphSearcherNode<Type>, GraphSearcherNode<Type>>();

                    fLimit = 0;
                }
                #endregion

                #region Protected & Private Methods
                protected override AlgorithmStatus StepMethod()
                {
                    if (fringeList.Count == 0)
                    {
                        Status = SearchStatus.FAILED;
                        OnFail?.Invoke();
                        return Status;
                    }

                    float fMin = float.MaxValue;

                    for (var listedNode = fringeList.First; listedNode != null;)
                    {
                        CurrentNode = listedNode.Value;

                        CurrentNode.HCost = graph.HeuristicCost(CurrentNode.Location.Value, TargetNode.Value);

                        if (CurrentNode.FCost > fLimit)
                        {
                            fMin = Math.Min(CurrentNode.FCost, fMin);

                            listedNode = listedNode.Next;
                            continue;
                        }

                        if (Equals(CurrentNode.Location.Value, TargetNode.Value))
                        {
                            Status = AlgorithmStatus.SUCCEEDED;
                            OnTargetFound?.Invoke(CurrentNode);
                            OnSuccess?.Invoke();
                            return Status;
                        }

                        List<Node> neighbours = graph.GetNeighbourNodes((Node)CurrentNode.Location);

                        neighbours.Reverse();

                        foreach (Node neighbour in neighbours)
                        {
                            float neighbourGCost = CurrentNode.GCost + graph.NodeTraversalCost(CurrentNode.Location.Value, neighbour.Value);

                            if (Cache.ContainsKey(neighbour.GraphSearcherNode) && neighbourGCost >= neighbour.GraphSearcherNode.GCost) continue;

                            var listedNeighbour = fringeList.Find(neighbour.GraphSearcherNode);

                            if (listedNeighbour != null)
                            {
                                fringeList.Remove(listedNeighbour);
                                fringeList.AddAfter(fringeList.Find(CurrentNode), listedNeighbour);
                            }
                            else
                            {
                                fringeList.AddAfter(fringeList.Find(CurrentNode), neighbour.GraphSearcherNode);
                            }

                            neighbour.GraphSearcherNode.GCost = neighbourGCost;
                            Cache.Remove(neighbour.GraphSearcherNode);
                            Cache.Add(neighbour.GraphSearcherNode, CurrentNode);
                        }
                        var lastNode = listedNode;
                        listedNode = lastNode.Next;
                        fringeList.Remove(lastNode);
                    }
                    fLimit = fMin;
                    return base.StepMethod();
                }
                protected override void InitializeMethod()
                {
                    fringeList.AddFirst(CurrentNode);

                    Cache.Add(CurrentNode, null);

                    fLimit = graph.HeuristicCost(CurrentNode.Location.Value, TargetNode.Value);
                }

                protected override void Reset()
                {
                    fringeList.Clear();

                    Cache.Clear();

                    fLimit = 0;

                    base.Reset();
                }
                #endregion
            }
        }
    }
}
