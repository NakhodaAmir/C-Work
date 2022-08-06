- [***Abstract Graph Algorithm***](Abstract%20Graph%20Algorithm)
      <br>The generic abstract class used to create the ***Graph Algorithms***.
- [***Abstract Graph Nodes***](Abstract%20Graph%20Nodes)
<br>Represents a vertex of the graph data structure. The type of node is relative to the type of graph denoted by its [***Graph Interface***](Graph%20Interfaces).
  - [***GraphSearchableNode***](Abstract%20Graph%20Nodes/GraphSearchableNode.cs)
      <br> Present in graphs inheriting the [***IGraphSearchable***](Graph%20Interfaces/IGraphSearchable.cs).
  - [***GraphTraversableNode***](Abstract%20Graph%20Nodes/GraphTraversableNode.cs)
    <br> Present in graphs inheriting the [***IGraphTraversable***](Graph%20Interfaces/IGraphTraversable.cs).
- [***Graph Interfaces***](Graph%20Interfaces)
<br> Interface used to create a graph data structure.
  - [***IGraphSearchable***](Graph%20Interfaces/IGraphSearchable.cs)
    <br> Graphs inheriting from this interface has access to the [***Graph Search Algorithms***](Graph%20Search) and [***Graph Traverse Algorithms***](Graph%20Traversal).
  - [***IGraphTraversable***](Graph%20Interfaces/IGraphTraversable.cs)
    <br> Graphs inheriting from this interface has access to the [***Graph Traverse Algorithms***](Graph%20Traversal).
- ***Graph Algorithms***
  - [***Graph Search Algorithms***](Graph%20Search)
    - [***Abstract Graph Search Algorithm***](Graph%20Search/Abstract%20Graph%20Searcher)
    <br>The generic abstract class used to create the [***Graph Search Algorithms***](Graph%20Search).
      - [***Dynamic Graph Search Algorithms***](Graph%20Search/Dynamic%20Graph%20Searcher%20Algorithms)
        - [***Abstract Dynamic Graph Search Algorithm***](Graph%20Search/Dynamic%20Graph%20Searcher%20Algorithms/Abstract%20Dynamic%20Graph%20Searcher)
        <br>The generic abstract class used to create the [***Dynamic Graph Search Algorithms***](Graph%20Search/Dynamic%20Graph%20Searcher%20Algorithms)
        - [***A\* Search***](Graph%20Search/Dynamic%20Graph%20Searcher%20Algorithms/AStarSearch.cs)
        - [***Dijkstra Search***](Graph%20Search/Dynamic%20Graph%20Searcher%20Algorithms/DijkstraSearch.cs)
        - [***Greedy Best First Search***](Graph%20Search/Dynamic%20Graph%20Searcher%20Algorithms/GreedyBestFirstSearch.cs)
      - [***Fringe Search***](Graph%20Search/FringeSearch.cs)
  - [***Graph Traverse Algorithms***](Graph%20Traversal)
    - [***Abstract Graph Traverser Algorithm***](Graph%20Traversal/Abstract%20Graph%20Traverser)
    <br>The generic abstract class used to create the [***Graph Traverse Algorithms***](Graph%20Traversal).
      - [***Breadth First Search***](Graph%20Traversal/BreadthFirstSearch.cs)
      - [***Depth First Search***](Graph%20Traversal/DepthFirstSearch.cs)
```cs
  using MirJan.GenericGraphs;
  
  public class ExampleGraph : IGraphSearchable<ExampleGraph.Node, Vector2>
  {
    Node SourceNode;
    Node TargetNode;
    
    public void ExampleMethod()
    {
        AStarSearch aStarSearch = new AStarSearch<Node, Vector2>(this);
        
        aStarSearch.Initialize(SourceNode, TargetNode);
        
        while (aStarSearch.IsRunning) aStarSearch.Step();
        
        
        DijkstraSearch dijkstraSearch = new DijkstraSearch<Node, Vector2>(this);
        
        dijkstraSearch.Initialize(SourceNode, TargetNode);
        
        while (dijkstraSearch.IsRunning) dijkstraSearch.Step();
        
        
        GreedyBestFirstSearch greedyBestFirstSearch = new GreedyBestFirstSearch<Node, Vector2>(this);
        
        greedyBestFirstSearch.Initialize(SourceNode, TargetNode);
        
        while (greedyBestFirstSearch.IsRunning) greedyBestFirstSearch.Step();
        
        
        FringeSearch fringeSearch = new FringeSearch<Node, Vector2>(this);
        
        fringeSearch.Initialize(SourceNode, TargetNode);
        
        while (fringeSearch.IsRunning) fringeSearch.Step();
        
        
        BreadthFirstSearch breadthFirstSearch = new BreadthFirstSearch<Node, Vector2>(this);
        
        breadthFirstSearch.Initialize(SourceNode);
        
        while (breadthFirstSearch.IsRunning) breadthFirstSearch.Step();
        
        
        DepthFirstSearch depthFirstSearch = new DepthFirstSearch<Node, Vector2>(this);
        
        depthFirstSearch.Initialize(SourceNode);
        
        while (depthFirstSearch.IsRunning) depthFirstSearch.Step();
    }
    
    public List<Node> GetNeighbourNodes(Node node)
    {
        throw new NotImplementedException(); 
    }
    
    public float HeuristicCost(Vector2 valueA, Vector2 valueB)
    {
        throw new NotImplementedException();
    }
    
    public float NodeTraversalCost(Vector2 valueA, Vector2 valueB)
    {
        throw new NotImplementedException();
    }
    
    public class Node : GraphSearchableNode<Vector2>
    {
        public Node(Vector2 value) : base(value) { }
    }
  }
  ```
