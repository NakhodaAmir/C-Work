namespace MirJan
{
    namespace GenericGraphs
    {
        namespace GraphTraversal
        {
            public class BreadthFirstSearch<Node, Type> : GraphTraverser<Node, Type> where Node : GraphTraversableNode<Type>
            {
                #region Delegates
                public DelegateOnNode OnAddToQueue { get; set; }
                #endregion

                #region Variables
                protected System.Collections.Generic.Queue<Node> queue;
                #endregion

                #region Constructor
                public BreadthFirstSearch(IGraphTraversable<Node, Type> graph) : base(graph)
                {
                    queue = new System.Collections.Generic.Queue<Node>();
                }
                #endregion

                #region Protected & Private Methods
                protected override AlgorithmStatus StepMethod()
                {
                    if (queue.Count == 0)
                    {
                        Status = AlgorithmStatus.SUCCEEDED;
                        OnSuccess?.Invoke();
                        return Status;
                    }

                    CurrentNode = queue.Dequeue();
                    OnChangeCurrentNode?.Invoke(CurrentNode);

                    foreach (Node neighbour in graph.GetNeighbourNodes(CurrentNode))
                    {
                        if (!visited.Contains(neighbour))
                        {
                            visited.Add(neighbour);
                            OnVisited?.Invoke(neighbour);

                            queue.Enqueue(neighbour);
                            OnAddToQueue?.Invoke(neighbour);

                            OnGetNeighbour?.Invoke(CurrentNode, neighbour);
                        }
                    }

                    return base.StepMethod();
                }
                protected override void InitializeMethod()
                {
                    visited.Add(CurrentNode);
                    OnVisited?.Invoke(CurrentNode);

                    queue.Enqueue(CurrentNode);
                    OnAddToQueue?.Invoke(CurrentNode);
                }

                protected override void Reset()
                {
                    queue.Clear();

                    base.Reset();
                }
                #endregion
            }
        }
    }
}
