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
                    public PairingHeap<GraphSearcherNode<Type>> OpenList { get; protected set; }
                    public System.Collections.Generic.HashSet<GraphSearcherNode<Type>> ClosedList { get; protected set; }
                    #endregion

                    #region Constructor
                    public DynamicGraphSearcher(IGraphSearchable<Node, Type> graph) : base(graph)
                    {
                        OpenList = new PairingHeap<GraphSearcherNode<Type>>();

                        ClosedList = new System.Collections.Generic.HashSet<GraphSearcherNode<Type>>();
                    }
                    #endregion

                    #region Protected & Private Methods
                    protected override AlgorithmStatus StepMethod()
                    {
                        if (OpenList.IsEmpty)
                        {
                            Status = SearchStatus.FAILED;
                            OnFail?.Invoke();
                            return Status;
                        }

                        ClosedList.Add(CurrentNode);
                        OnAddToClosedList?.Invoke(CurrentNode);

                        CurrentNode = OpenList.ExtractMin();
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
                            if (ClosedList.Contains(neighbour.GraphSearcherNode)) continue;

                            AlgorithmSpecificImplementation(neighbour);
                        }

                        return base.StepMethod();
                    }
                    protected override void InitializeMethod()
                    {
                        OpenList.Insert(CurrentNode);
                        OnAddToOpenList?.Invoke(CurrentNode);
                    }

                    protected abstract void AlgorithmSpecificImplementation(Node neighbour);

                    protected void AlgorithmCommonImplementation(Node neighbour, float gCost, float hCost)
                    {
                        gCost += neighbour.GraphSearcherNode.PCost;

                        if (gCost < neighbour.GraphSearcherNode.GCost || !OpenList.Contains(neighbour.GraphSearcherNode))
                        {
                            neighbour.GraphSearcherNode.SetProperties(neighbour, CurrentNode, gCost, hCost);

                            if (!OpenList.Contains(neighbour.GraphSearcherNode))
                            {
                                OpenList.Insert(neighbour.GraphSearcherNode);
                                OnAddToOpenList?.Invoke(CurrentNode);
                            }
                            else
                            {
                                OpenList.Update(neighbour.GraphSearcherNode);
                            }
                        }
                    }

                    protected override void Reset()
                    {
                        OpenList = new PairingHeap<GraphSearcherNode<Type>>();

                        ClosedList.Clear();

                        base.Reset();
                    }
                    #endregion
                }
            }
        }
    }
}
