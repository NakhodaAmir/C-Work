namespace MirJan
{
    namespace GenericGraphs
    {
        namespace GraphTraversal
        {
            public class DepthFirstSearch<Node, Type> : GraphTraverser<Node, Type> where Node : GraphTraversableNode<Type>
            {
                #region Delegates
                public DelegateOnNode OnAddToStack { get; set; }
                #endregion

                #region Variables
                protected Stack<Node> stack;
                #endregion

                #region Constructor
                public DepthFirstSearch(IGraphTraversable<Node, Type> graph) : base(graph)
                {
                    stack = new Stack<Node>();
                }
                #endregion

                #region Protected & Private Methods
                protected override AlgorithmStatus StepMethod()
                {
                    if (stack.Count == 0)
                    {
                        Status = AlgorithmStatus.SUCCEEDED;
                        OnSuccess?.Invoke();
                        return Status;
                    }

                    CurrentNode = stack.Pop();
                    OnChangeCurrentNode?.Invoke(CurrentNode);

                    if (!visited.Contains(CurrentNode))
                    {
                        visited.Add(CurrentNode);
                        OnVisited?.Invoke(CurrentNode);
                    }

                    foreach (Node neighbour in graph.GetNeighbourNodes(CurrentNode))
                    {
                        if (!visited.Contains(neighbour))
                        {
                            stack.Push(neighbour);
                            OnAddToStack?.Invoke(neighbour);

                            OnGetNeighbour?.Invoke(CurrentNode, neighbour);
                        }
                    }

                    return base.StepMethod();
                }
                protected override void InitializeMethod()
                {
                    stack.Push(CurrentNode);
                    OnChangeCurrentNode?.Invoke(CurrentNode);
                }

                protected override void Reset()
                {
                    stack.Clear();

                    base.Reset();
                }
                #endregion
            }
        }
    }
}
