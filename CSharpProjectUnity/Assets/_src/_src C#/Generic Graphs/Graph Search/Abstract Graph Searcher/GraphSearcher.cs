namespace MirJan
{
    namespace GenericGraphs
    {
        namespace GraphSearch
        {
            abstract public class GraphSearcher<Node, Type> : GraphAlgorithm<Node> where Node : GraphSearchableNode<Type>
            {
                #region Enumeration Class
                public class SearchStatus : AlgorithmStatus
                {
                    public static readonly SearchStatus FAILED = new SearchStatus(nameof(FAILED));

                    protected SearchStatus(string name) : base(name) { }
                }
                #endregion
                #region Delegates
                public DelegateOnStatus OnFail { get; set; }

                public delegate void DelegateOnNode(GraphSearcherNode<Type> node);
                public DelegateOnNode OnChangeCurrentNode { get; set; }
                public DelegateOnNode OnTargetFound { get; set; }
                #endregion

                #region Properties
                public Node TargetNode { get; protected set; }
                public GraphSearcherNode<Type> CurrentNode { get; protected set; }

                public System.Collections.Generic.List<Node> PathList { get; protected set; }

                public bool IsFailed { get { return Status.Equals(SearchStatus.FAILED); } }
                #endregion

                #region Variables
                protected readonly IGraphSearchable<Node, Type> graph;

                protected readonly bool retracePathInclSource;
                #endregion

                #region Constructor
                public GraphSearcher(IGraphSearchable<Node, Type> graph, bool retracePathInclSource = true) : base()
                {
                    TargetNode = null;

                    CurrentNode = null;

                    PathList = new System.Collections.Generic.List<Node>();

                    this.graph = graph;

                    this.retracePathInclSource = retracePathInclSource;
                }
                #endregion

                #region Public Methods
                public bool Initialize(Node sourceNode, Node targetNode)
                {
                    if (!InitializeStart()) return false;

                    SourceNode = sourceNode;
                    TargetNode = targetNode;

                    CurrentNode = new GraphSearcherNode<Type>(SourceNode, null, 0, graph.HeuristicCost(SourceNode.Value, TargetNode.Value), SourceNode.GraphSearcherNode.PCost);
                    OnChangeCurrentNode?.Invoke(CurrentNode);

                    InitializeMethod();

                    return InitializeEnd();
                }

                public void RetracePath()
                {
                    var currentNode = CurrentNode;

                    var exclNode = retracePathInclSource ? null : SourceNode.GraphSearcherNode;

                    while (currentNode != exclNode)
                    {
                        PathList.Add((Node)currentNode.Location);
                        currentNode = RetraceImplementation(currentNode);
                    }

                    PathList.Reverse();
                }
                #endregion

                #region Protected & Private Methods
                protected override void Reset()
                {
                    TargetNode = null;

                    CurrentNode = null;

                    PathList.Clear();

                    base.Reset();
                }

                protected virtual GraphSearcherNode<Type> RetraceImplementation(GraphSearcherNode<Type> node)
                {
                    return node.Parent;
                }
                #endregion
            }
        }
    }
}
