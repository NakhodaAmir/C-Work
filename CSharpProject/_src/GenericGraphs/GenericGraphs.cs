namespace MirJan
{
    namespace GenericGraphs
    {
        using PriorityQueues.PairingHeap;

        #region Graph Interface
        public interface IGraph<T, G> where T : GraphNode<T, G>// T is the graph node, G is graph value type
        {
            float GetDistance(G valueA, G valueB);//Gets the Distance between two nodes
            List<T> GetNeighbourNodes(T node);//Gets the neighbours of two nodes
        }
        #endregion

        #region Abstract Graph Node
        abstract public class GraphNode<T, G> where T : GraphNode<T, G>// T is the graph node, G is graph value type
        {
            public G Value { get; set; }

            public PathFinding.PathFinder<T, G>.PathFinderNode PathFinderNode { get; set; }

            public GraphNode(G value)
            {
                Value = value;

                PathFinderNode = new PathFinding.PathFinder<T, G>.PathFinderNode((T)this, null, 0, 0);
            }
        }
        #endregion

        namespace PathFinding
        {
            #region A Star Path Finding
            public class AStarPathFinder<T, G> : PathFinder<T, G> where T : GraphNode<T, G>
            {
                public AStarPathFinder(IGraph<T, G> graph) : base(graph) { }

                protected override void AlgorithmSpecificImplementation(T neighbour)
                {
                    float gCost = CurrentNode.GCost + Graph.GetDistance(CurrentNode.Location.Value, neighbour.Value);
                    float hCost = Graph.GetDistance(Goal.Value, neighbour.Value);

                    AlgorithmCommonImplementation(neighbour, gCost, hCost);
                }
            }
            #endregion

            #region Dijkstra Path Finding
            public class DijkstraPathFinder<T, G> : PathFinder<T, G> where T : GraphNode<T, G>
            {
                public DijkstraPathFinder(IGraph<T, G> graph) : base(graph) { }

                protected override void AlgorithmSpecificImplementation(T neighbour)
                {
                    float gCost = CurrentNode.GCost + Graph.GetDistance(CurrentNode.Location.Value, neighbour.Value);
                    float hCost = 0;

                    AlgorithmCommonImplementation(neighbour, gCost, hCost);
                }
            }
            #endregion

            #region Greedy Best First Path Finding
            public class GreedyBestFirstPathFinder<T, G> : PathFinder<T, G> where T : GraphNode<T, G>
            {
                public GreedyBestFirstPathFinder(IGraph<T, G> graph) : base(graph) { }

                protected override void AlgorithmSpecificImplementation(T neighbour)
                {
                    float gCost = 0;
                    float hCost = Graph.GetDistance(Goal.Value, neighbour.Value);

                    AlgorithmCommonImplementation(neighbour, gCost, hCost);
                }
            }
            #endregion

            #region Abstract Path Finder
            abstract public class PathFinder<T, G> where T : GraphNode<T, G>
            {
                #region Enums
                public enum PathFinderStatus
                {
                    NOT_INITIALIZE,
                    SUCCESS,
                    FAILURE,
                    RUNNING
                }
                #endregion

                #region Delegates
                public delegate void DelegatePathFinderNode(PathFinderNode node);

                public DelegatePathFinderNode onChangeCurrentNode;
                public DelegatePathFinderNode onAddToOpenList;
                public DelegatePathFinderNode onAddToClosedList;
                public DelegatePathFinderNode onGoalFound;

                public delegate void DelegateNoArgument();

                public DelegateNoArgument onStarted;
                public DelegateNoArgument onRunning;
                public DelegateNoArgument onFailure;
                public DelegateNoArgument onSuccess;
                #endregion

                #region Properties
                public PathFinderStatus Status { get; private set; }

                public T Start { get; private set; }
                public T Goal { get; private set; }
                public PathFinderNode CurrentNode { get; private set; }
                #endregion

                #region Variables
                protected readonly IGraph<T, G> Graph;

                protected PairingHeap<PathFinderNode> OpenList;
                protected HashSet<PathFinderNode> ClosedList;
                #endregion

                #region Constructor
                public PathFinder(IGraph<T, G> graph)
                {
                    Graph = graph;

                    Status = PathFinderStatus.NOT_INITIALIZE;

                    Start = Goal = null;

                    CurrentNode = null;

                    OpenList = new PairingHeap<PathFinderNode>();

                    ClosedList = new HashSet<PathFinderNode>();
                }
                #endregion

                #region Path Finder Node
                public class PathFinderNode : IPairingHeap<PathFinderNode>
                {
                    public PathFinderNode Parent { get; set; }
                    public T Location { get; private set; }

                    public float FCost { get { return GCost + HCost; } }
                    public float GCost { get; set; }
                    public float HCost { get; set; }

                    public PairingHeap<PathFinderNode>.PairingHeapNode HeapNode { get; set; }

                    public PathFinderNode(T location, PathFinderNode parent, float gCost, float hCost)
                    {
                        Location = location;
                        Parent = parent;
                        GCost = gCost;
                        HCost = hCost;

                        Location.PathFinderNode = this;
                    }

                    public int CompareTo(PathFinderNode other)
                    {
                        int compare = FCost.CompareTo(other.FCost);

                        if (compare == 0) compare = HCost.CompareTo(other.HCost);

                        return compare;
                    }
                }
                #endregion

                #region Public Methods
                public bool Initialize(T start, T goal)
                {
                    if (Status == PathFinderStatus.RUNNING) return false;

                    Reset();

                    Start = start;
                    Goal = goal;

                    CurrentNode = new PathFinderNode(Start, null, 0f, Graph.GetDistance(Start.Value, Goal.Value));

                    OpenList.Insert(CurrentNode);

                    onChangeCurrentNode?.Invoke(CurrentNode);
                    onStarted?.Invoke();

                    Status = PathFinderStatus.RUNNING;
                    return true;
                }

                public PathFinderStatus Step()
                {
                    if (OpenList.IsEmpty)
                    {
                        Status = PathFinderStatus.FAILURE;
                        onFailure?.Invoke();
                        return Status;
                    }

                    ClosedList.Add(CurrentNode);

                    onAddToClosedList?.Invoke(CurrentNode);

                    CurrentNode = OpenList.ExtractMin();

                    onChangeCurrentNode?.Invoke(CurrentNode);


                    if (Equals(CurrentNode.Location.Value, Goal.Value))
                    {
                        Status = PathFinderStatus.SUCCESS;
                        onGoalFound?.Invoke(CurrentNode);
                        onSuccess?.Invoke();
                        return Status;
                    }

                    foreach (T neighbour in Graph.GetNeighbourNodes(CurrentNode.Location))
                    {
                        if (ClosedList.Contains(neighbour.PathFinderNode)) continue;

                        AlgorithmSpecificImplementation(neighbour);
                    }

                    Status = PathFinderStatus.RUNNING;
                    onRunning?.Invoke();
                    return Status;
                }
                #endregion

                #region Protected & Private Methods
                abstract protected void AlgorithmSpecificImplementation(T neighbour);

                protected void AlgorithmCommonImplementation(T neighbour, float gCost, float hCost)
                {
                    if (gCost < neighbour.PathFinderNode.GCost || !OpenList.Contains(neighbour.PathFinderNode))
                    {
                        neighbour.PathFinderNode = new PathFinderNode(neighbour, CurrentNode, gCost, hCost);

                        if (!OpenList.Contains(neighbour.PathFinderNode))
                        {
                            OpenList.Insert(neighbour.PathFinderNode);
                        }
                        else
                        {
                            OpenList.Update(neighbour.PathFinderNode);
                        }
                    }
                }

                void Reset()
                {
                    CurrentNode = null;

                    OpenList = new PairingHeap<PathFinderNode>();

                    ClosedList.Clear();

                    Status = PathFinderStatus.NOT_INITIALIZE;
                }
                #endregion
            }
            #endregion
        }

        namespace GraphTraversal
        {
            #region Depth First Search
            public class DepthFirstSearch<T, G> : GraphTraverser<T, G> where T : GraphNode<T, G>
            {
                #region Delegates
                public DelegateGraphNode onAddToStack;
                #endregion

                #region Variables
                protected Stack<T> Stack;
                #endregion

                #region Constructor
                public DepthFirstSearch(IGraph<T, G> graph) : base(graph)
                {
                    Stack = new Stack<T>();
                }
                #endregion

                #region Public Methods
                public override GraphTraverserStatus Step()
                {
                    if (Stack.Count == 0)
                    {
                        Status = GraphTraverserStatus.SUCCESS;
                        onStopped?.Invoke();
                        return Status;
                    }

                    CurrentNode = Stack.Pop();
                    onChangeCurrentNode?.Invoke(CurrentNode);

                    if (!Visited.Contains(CurrentNode))
                    {
                        Visited.Add(CurrentNode);
                        onVisited?.Invoke(CurrentNode);
                    }

                    foreach (T neighbour in Graph.GetNeighbourNodes(CurrentNode))
                    {
                        if (!Visited.Contains(neighbour))
                        {
                            Stack.Push(neighbour);
                            onAddToStack?.Invoke(neighbour);

                            onGetNeighbour?.Invoke(CurrentNode, neighbour);
                        }
                    }

                    Status = GraphTraverserStatus.RUNNING;
                    onRunning?.Invoke();
                    return Status;
                }
                #endregion

                #region Protected & Private Methods
                protected override void AlgorithmSpecificInitializationImplementation(T node)
                {
                    Stack.Push(node);
                    onAddToStack?.Invoke(node);
                }

                protected override void Reset()
                {
                    Stack.Clear();

                    base.Reset();
                }
                #endregion
            }
            #endregion

            #region Breadth First Search
            public class BreadthFirstSearch<T, G> : GraphTraverser<T, G> where T : GraphNode<T, G>
            {
                #region Delegates
                public DelegateGraphNode onAddToQueue;
                #endregion

                #region Variables
                protected Queue<T> Queue;
                #endregion

                #region Constructor
                public BreadthFirstSearch(IGraph<T, G> graph) : base(graph)
                {
                    Queue = new Queue<T>();
                }
                #endregion

                #region Public Methods
                public override GraphTraverserStatus Step()
                {
                    if (Queue.Count == 0)
                    {
                        Status = GraphTraverserStatus.SUCCESS;
                        onStopped?.Invoke();
                        return Status;
                    }

                    CurrentNode = Queue.Dequeue();
                    onChangeCurrentNode?.Invoke(CurrentNode);

                    foreach (T neighbour in Graph.GetNeighbourNodes(CurrentNode))
                    {
                        if (!Visited.Contains(neighbour))
                        {
                            Visited.Add(neighbour);
                            onVisited?.Invoke(neighbour);

                            Queue.Enqueue(neighbour);
                            onAddToQueue?.Invoke(neighbour);

                            onGetNeighbour?.Invoke(CurrentNode, neighbour);
                        }
                    }

                    Status = GraphTraverserStatus.RUNNING;
                    onRunning?.Invoke();
                    return Status;
                }
                #endregion

                #region Protected & Private Methods
                protected override void AlgorithmSpecificInitializationImplementation(T node)
                {
                    Visited.Add(node);
                    onVisited?.Invoke(node);

                    Queue.Enqueue(node);
                    onAddToQueue?.Invoke(node);
                }

                protected override void Reset()
                {
                    Queue.Clear();

                    base.Reset();
                }
                #endregion
            }

            #endregion

            #region Abstract Graph Traverser
            abstract public class GraphTraverser<T, G> where T : GraphNode<T, G>
            {
                #region Enum
                public enum GraphTraverserStatus
                {
                    NOT_INITIALIZE,
                    SUCCESS,
                    RUNNING
                }
                #endregion

                #region Delegates
                public delegate void DelegateGraphNode(T node);

                public DelegateGraphNode onVisited;
                public DelegateGraphNode onChangeCurrentNode;

                public delegate void DelegateGraphNeighbourNode(T node, T neighbour);

                public DelegateGraphNeighbourNode onGetNeighbour;

                public delegate void DelegateNoArgument();

                public DelegateNoArgument onStarted;
                public DelegateNoArgument onRunning;
                public DelegateNoArgument onStopped;
                #endregion

                #region Properties
                public T CurrentNode { get; set; }

                public GraphTraverserStatus Status { get; protected set; }
                #endregion

                #region Variables
                protected readonly IGraph<T, G> Graph;

                protected HashSet<T> Visited;
                #endregion

                #region Constructor
                public GraphTraverser(IGraph<T, G> graph)
                {
                    Graph = graph;

                    Status = GraphTraverserStatus.NOT_INITIALIZE;

                    CurrentNode = null;

                    Visited = new HashSet<T>();
                }
                #endregion

                #region Public Methods
                public bool Initialize(T sourceNode)
                {
                    if (Status == GraphTraverserStatus.RUNNING) return false;

                    Reset();

                    CurrentNode = sourceNode;
                    onChangeCurrentNode?.Invoke(CurrentNode);

                    AlgorithmSpecificInitializationImplementation(CurrentNode);

                    onStarted?.Invoke();

                    Status = GraphTraverserStatus.RUNNING;
                    return true;
                }

                abstract public GraphTraverserStatus Step();
                #endregion

                #region Protected & Private Methods
                abstract protected void AlgorithmSpecificInitializationImplementation(T node);

                virtual protected void Reset()
                {
                    CurrentNode = null;

                    Visited.Clear();

                    Status = GraphTraverserStatus.NOT_INITIALIZE;
                }
                #endregion
            }
            #endregion
        }
    }
}
