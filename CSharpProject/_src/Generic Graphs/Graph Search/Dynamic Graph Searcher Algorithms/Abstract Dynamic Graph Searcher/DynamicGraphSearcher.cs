namespace MirJan
{
    namespace GenericGraphs
    {
        namespace GraphSearch
        {
            namespace DynamicGraphSearch
            {
                using MirJan.PriorityQueues;

                abstract public class DynamicGraphSearcher<Node, Type> : GraphSearcher<Node, Type> where Node : GraphSearchableNode<Type>
                {
                    #region Delegates
                    public DelegateOnNode OnAddToOpenList { get; set; }
                    public DelegateOnNode OnAddToClosedList { get; set; }
                    #endregion

                    #region Variables
                    protected PairingHeap<GraphSearcherNode<Type>> openList;
                    protected System.Collections.Generic.HashSet<GraphSearcherNode<Type>> closedList;
                    #endregion

                    #region Constructor
                    public DynamicGraphSearcher(IGraphSearchable<Node, Type> graph) : base(graph)
                    {
                        openList = new PairingHeap<GraphSearcherNode<Type>>();

                        closedList = new System.Collections.Generic.HashSet<GraphSearcherNode<Type>>();
                    }
                    #endregion

                    #region Protected & Private Methods
                    protected override AlgorithmStatus StepMethod()
                    {
                        if (openList.IsEmpty)
                        {
                            Status = SearchStatus.FAILED;
                            OnFail?.Invoke();
                            return Status;
                        }

                        closedList.Add(CurrentNode);
                        OnAddToClosedList?.Invoke(CurrentNode);

                        CurrentNode = openList.ExtractMin();
                        OnChangeCurrentNode?.Invoke(CurrentNode);

                        if (Equals(CurrentNode.Location.Value, TargetNode.Value))
                        {
                            Status = AlgorithmStatus.SUCCEEDED;
                            OnTargetFound?.Invoke(CurrentNode);
                            OnSuccess?.Invoke();
                            return Status;
                        }

                        foreach (Node neighbour in graph.GetNeighbourNodes((Node)CurrentNode.Location))
                        {
                            if (closedList.Contains(neighbour.GraphSearcherNode)) continue;

                            AlgorithmSpecificImplementation(neighbour);
                        }

                        return base.StepMethod();
                    }
                    protected override void InitializeMethod()
                    {
                        openList.Insert(CurrentNode);
                        OnAddToOpenList?.Invoke(CurrentNode);
                    }

                    protected abstract void AlgorithmSpecificImplementation(Node neighbour);

                    protected void AlgorithmCommonImplementation(Node neighbour, float gCost, float hCost)
                    {
                        if (gCost < neighbour.GraphSearcherNode.GCost || !openList.Contains(neighbour.GraphSearcherNode))
                        {
                            neighbour.GraphSearcherNode.SetProperties(neighbour, CurrentNode, gCost, hCost);

                            if (!openList.Contains(neighbour.GraphSearcherNode))
                            {
                                openList.Insert(neighbour.GraphSearcherNode);
                                OnAddToOpenList?.Invoke(CurrentNode);
                            }
                            else
                            {
                                openList.Update(neighbour.GraphSearcherNode);
                            }
                        }
                    }

                    protected override void Reset()
                    {
                        openList = new PairingHeap<GraphSearcherNode<Type>>();

                        closedList.Clear();

                        base.Reset();
                    }
                    #endregion
                }
            }
        }
    }
}
