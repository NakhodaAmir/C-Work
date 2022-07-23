namespace MirJan
{
    namespace GenericGraphs
    {
        namespace GraphSearch
        {
            public class FringeSearch<Node, Type> : GraphSearcher<Node, Type> where Node : GraphSearchableNode<Type>
            {
                #region Properties
                public System.Collections.Generic.Dictionary<GraphSearcherNode<Type>, GraphSearcherNode<Type>> Cache { get; private set; }
                #endregion

                #region Variables
                public System.Collections.Generic.LinkedList<GraphSearcherNode<Type>> FringeList { get;  private set; }

                float fLimit;
                #endregion

                #region Constructor
                public FringeSearch(IGraphSearchable<Node, Type> graph) : base(graph)
                {
                    FringeList = new System.Collections.Generic.LinkedList<GraphSearcherNode<Type>>();

                    Cache = new System.Collections.Generic.Dictionary<GraphSearcherNode<Type>, GraphSearcherNode<Type>>();

                    fLimit = 0;
                }
                #endregion

                #region Protected & Private Methods
                protected override AlgorithmStatus StepMethod()
                {
                    if (FringeList.Count == 0)
                    {
                        Status = SearchStatus.FAILED;
                        OnFail?.Invoke();
                        return Status;
                    }

                    float fMin = float.MaxValue;

                    for (var listedNode = FringeList.First; listedNode != null;)
                    {
                        CurrentNode = listedNode.Value;

                        CurrentNode.HCost = graph.HeuristicCost(CurrentNode.Location.Value, TargetNode.Value);

                        if (CurrentNode.FCost > fLimit)
                        {
                            fMin = System.Math.Min(CurrentNode.FCost, fMin);

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

                        System.Collections.Generic.List<Node> neighbours = graph.GetNeighbourNodes((Node)CurrentNode.Location);

                        neighbours.Reverse();

                        foreach (Node neighbour in neighbours)
                        {
                            float neighbourGCost = CurrentNode.GCost + graph.NodeTraversalCost(CurrentNode.Location.Value, neighbour.Value) + neighbour.GraphSearcherNode.PCost;

                            if (Cache.ContainsKey(neighbour.GraphSearcherNode) && neighbourGCost >= neighbour.GraphSearcherNode.GCost) continue;

                            var listedNeighbour = FringeList.Find(neighbour.GraphSearcherNode);

                            if (listedNeighbour != null)
                            {
                                FringeList.Remove(listedNeighbour);
                                FringeList.AddAfter(FringeList.Find(CurrentNode), listedNeighbour);
                            }
                            else
                            {
                                FringeList.AddAfter(FringeList.Find(CurrentNode), neighbour.GraphSearcherNode);
                            }

                            neighbour.GraphSearcherNode.GCost = neighbourGCost;
                            Cache.Remove(neighbour.GraphSearcherNode);
                            Cache.Add(neighbour.GraphSearcherNode, CurrentNode);
                        }
                        var lastNode = listedNode;
                        listedNode = lastNode.Next;
                        FringeList.Remove(lastNode);
                    }
                    fLimit = fMin;
                    return base.StepMethod();
                }
                protected override void InitializeMethod()
                {
                    FringeList.AddFirst(CurrentNode);

                    Cache.Add(CurrentNode, null);

                    fLimit = graph.HeuristicCost(CurrentNode.Location.Value, TargetNode.Value);
                }

                protected override void Reset()
                {
                    FringeList.Clear();

                    Cache.Clear();

                    fLimit = 0;

                    base.Reset();
                }
                #endregion
            }
        }
    }
}
