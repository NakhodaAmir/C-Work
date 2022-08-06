- [***Abstract Graph Algorithm***](Abstract%20Graph%20Algorithm)
      <br>The generic abstract class used to create the [***Graph Search Algorithms***](Graph%20Search) and [***Graph Traverse Algorithms***](Graph%20Traversal).
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
- [***Graph Search Algorithms***](Graph%20Search)
    <br> Algorithms that will search, through the graph data structure, from a specified source node, for a specified target node. The algorithm will be successfull once it has found the target node.
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
    <br> Algorithms that will traverse the whole graph data structure from a specified source node. The algorithm will be successfull once the whole graph data structure has been traversed.
    - [***Abstract Graph Traverser Algorithm***](Graph%20Traversal/Abstract%20Graph%20Traverser)
    <br>The generic abstract class used to create the [***Graph Traverse Algorithms***](Graph%20Traversal).
      - [***Breadth First Search***](Graph%20Traversal/BreadthFirstSearch.cs)
      - [***Depth First Search***](Graph%20Traversal/DepthFirstSearch.cs)
- ***Generic Graphs Diagram***
<br>![Image](/../main/Resources/Generic%20Graphs%20Overview.PNG)
```cs
  using MirJan.GenericGraphs;
  
  public class ExampleGraph : IGraphSearchable<ExampleGraph.Node, Vector2>
  {
    public Node SourceNode = new Node(new Vector2(0, 0));
    public Node TargetNode = new Node(new Vector2(1, 0));
    
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
  
  public class OtherExampleClass
  {
      ExampleGraph exampleGraph = new ExampleGraph();
      
      public void FindPath()
      {
            //The below example is similar with the Dijkstra Search, Greedy Best First Search and Fringe Search algorithms
            AStarSearch aStarSearch = new AStarSearch<ExampleGraph.Node, Vector2>(exampleGraph);
            aStarSearch.Initialize(exampleGraph.SourceNode, exampleGraph.TargetNode);
            
            while(aStarSearch.IsRunning) aStarSearch.Step();
            
            //aStarSearch has finished
            
            //The below example is similar with the Depth First Search algorithm
            BreadthFirstSearch breadthFirstSearch = new BreadthFirstSearch<ExampleGraph.Node, Vector2>(exampleGraph);
            breadthFirstSearch.Initialize(exampleGraph.SourceNode);
            
            while(breadthFirstSearch.IsRunning) breadthFirstSearch.Step();
            
            //breadthFirstSearch has finished
      }
  }
  ```
