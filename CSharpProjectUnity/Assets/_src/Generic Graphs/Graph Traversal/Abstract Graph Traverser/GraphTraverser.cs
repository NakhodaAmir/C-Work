namespace MirJan
{
    namespace GenericGraphs
    {
        namespace GraphTraversal
        {
            abstract public class GraphTraverser<Node, Type> : GraphAlgorithm<Node> where Node : GraphTraversableNode<Type>
            {
                #region Delegates
                public delegate void DelegateOnNode(Node node);
                public DelegateOnNode OnVisited { get; set; }
                public DelegateOnNode OnChangeCurrentNode { get; set; }

                public delegate void DelegateOnNeighbour(Node node, Node neighbour);
                public DelegateOnNeighbour OnGetNeighbour { get; set; }
                #endregion

                #region Properties
                public Node CurrentNode { get; protected set; }
                #endregion

                #region Variables
                protected readonly IGraphTraversable<Node, Type> graph;

                protected System.Collections.Generic.HashSet<Node> visited;
                #endregion

                #region Constructor
                public GraphTraverser(IGraphTraversable<Node, Type> graph) : base()
                {
                    CurrentNode = null;

                    this.graph = graph;

                    visited = new System.Collections.Generic.HashSet<Node>();
                }
                #endregion

                #region Public Methods
                public bool Initialize(Node sourceNode)
                {
                    if (!InitializeStart()) return false;

                    SourceNode = sourceNode;

                    CurrentNode = SourceNode;
                    OnChangeCurrentNode?.Invoke(CurrentNode);

                    InitializeMethod();

                    return InitializeEnd();
                }
                #endregion

                #region Protected & Private Methods
                protected override void Reset()
                {
                    CurrentNode = null;

                    visited.Clear();

                    base.Reset();
                }
                #endregion
            }
        }
    }
}
