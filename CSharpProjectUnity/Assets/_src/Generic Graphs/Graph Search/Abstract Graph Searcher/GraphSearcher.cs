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
                #endregion

                #region Variables
                protected readonly IGraphSearchable<Node, Type> graph;
                #endregion

                #region Constructor
                public GraphSearcher(IGraphSearchable<Node, Type> graph) : base()
                {
                    TargetNode = null;

                    CurrentNode = null;

                    this.graph = graph;
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
                #endregion

                #region Protected & Private Methods
                protected override void Reset()
                {
                    TargetNode = null;

                    CurrentNode = null;

                    base.Reset();
                }
                #endregion
            }
        }
    }
}
