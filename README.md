# [Source Code](CSharpProjectUnity/Assets/_src)
## [C# Specific Code](CSharpProjectUnity/Assets/_src/_src%20C%23)
- [**Generic Graphs**](CSharpProjectUnity/Assets/_src/_src%20C%23/Generic%20Graphs)
    - [***Abstract Graph Algorithm***](CSharpProjectUnity/Assets/_src/_src%20C%23/Generic%20Graphs/Abstract%20Graph%20Algorithm)
      <br>The generic abstract class used to create the ***Graph Algorithms***.
    - [***Abstract Graph Nodes***](CSharpProjectUnity/Assets/_src/_src%20C%23/Generic%20Graphs/Abstract%20Graph%20Nodes)
      <br>Represents a vertex of the graph data structure. The type of node is relative to the type of graph denoted by its [***Graph Interface***](CSharpProjectUnity/Assets/_src/_src%20C%23/Generic%20Graphs/Graph%20Interfaces).
       - [***GraphSearchableNode***](CSharpProjectUnity/Assets/_src/_src%20C%23/Generic%20Graphs/Abstract%20Graph%20Nodes/GraphSearchableNode.cs)
        <br> Present in graphs inheriting the [***Graph Interface: IGraphSearchable***](CSharpProjectUnity/Assets/_src/_src%20C%23/Generic%20Graphs/Graph%20Interfaces/IGraphSearchable.cs).
        - [***GraphTraversableNode***](CSharpProjectUnity/Assets/_src/_src%20C%23/Generic%20Graphs/Abstract%20Graph%20Nodes/GraphTraversableNode.cs)
        <br> Present in graphs inheriting the [***Graph Interface: IGraphTraversable***](CSharpProjectUnity/Assets/_src/_src%20C%23/Generic%20Graphs/Graph%20Interfaces/IGraphTraversable.cs).
    - [***Graph Interfaces***](CSharpProjectUnity/Assets/_src/_src%20C%23/Generic%20Graphs/Graph%20Interfaces)
      <br> Interface used to create a graph data structure.
      - [***IGraphSearchable***](CSharpProjectUnity/Assets/_src/_src%20C%23/Generic%20Graphs/Graph%20Interfaces/IGraphSearchable.cs)
      <br> Graphs inheriting from this interface has access to the [***Graph Algorithms: Graph Search Algorithms***](CSharpProjectUnity/Assets/_src/_src%20C%23/Generic%20Graphs/Graph%20Search) and [***Graph Algorithms: Graph Traverse Algorithms***](CSharpProjectUnity/Assets/_src/_src%20C%23/Generic%20Graphs/Graph%20Traversal).
      - [***IGraphTraversable***](CSharpProjectUnity/Assets/_src/_src%20C%23/Generic%20Graphs/Graph%20Interfaces/IGraphTraversable.cs)
      <br> Graphs inheriting from this interface has access to the [***Graph Algorithms: Graph Traverse Algorithms***](CSharpProjectUnity/Assets/_src/_src%20C%23/Generic%20Graphs/Graph%20Traversal).
    - ***Graph Algorithms***
      - [***Graph Search Algorithms***](CSharpProjectUnity/Assets/_src/_src%20C%23/Generic%20Graphs/Graph%20Search)
        - [***Abstract Graph Search Algorithm***](CSharpProjectUnity/Assets/_src/_src%20C%23/Generic%20Graphs/Graph%20Search/Abstract%20Graph%20Searcher)
        <br>The generic abstract class used to create the [***Graph Search Algorithms***](CSharpProjectUnity/Assets/_src/_src%20C%23/Generic%20Graphs/Graph%20Search).
        - [***Dynamic Graph Search Algorithms***](CSharpProjectUnity/Assets/_src/_src%20C%23/Generic%20Graphs/Graph%20Search/Dynamic%20Graph%20Searcher%20Algorithms)
          - [***Abstract Dynamic Graph Search Algorithm***](CSharpProjectUnity/Assets/_src/_src%20C%23/Generic%20Graphs/Graph%20Search/Dynamic%20Graph%20Searcher%20Algorithms/Abstract%20Dynamic%20Graph%20Searcher)
          <br>The generic abstract class used to create the [***Dynamic Graph Search Algorithms***](CSharpProjectUnity/Assets/_src/_src%20C%23/Generic%20Graphs/Graph%20Search/Dynamic%20Graph%20Searcher%20Algorithms)
          - [***A Star Search***](CSharpProjectUnity/Assets/_src/_src%20C%23/Generic%20Graphs/Graph%20Search/Dynamic%20Graph%20Searcher%20Algorithms/AStarSearch.cs)
          - [***Dijkstra Search***](CSharpProjectUnity/Assets/_src/_src%20C%23/Generic%20Graphs/Graph%20Search/Dynamic%20Graph%20Searcher%20Algorithms/DijkstraSearch.cs)
          - [***Greedy Best First Search***](CSharpProjectUnity/Assets/_src/_src%20C%23/Generic%20Graphs/Graph%20Search/Dynamic%20Graph%20Searcher%20Algorithms/GreedyBestFirstSearch.cs)
        - [***Fringe Search***](CSharpProjectUnity/Assets/_src/_src%20C%23/Generic%20Graphs/Graph%20Search/FringeSearch.cs)
      - [***Graph Traverse Algorithms***](C-Work/tree/main/CSharpProjectUnity/Assets/_src/_src%20C%23/Generic%20Graphs/Graph%20Traversal)
        - [***Abstract Graph Traverser Algorithm***](CSharpProjectUnity/Assets/_src/_src%20C%23/Generic%20Graphs/Graph%20Traversal/Abstract%20Graph%20Traverser)
        <br>The generic abstract class used to create the [***Graph Traverse Algorithms***](C-Work/tree/main/CSharpProjectUnity/Assets/_src/_src%20C%23/Generic%20Graphs/Graph%20Traversal).
        - [***Breadth First Search***](CSharpProjectUnity/Assets/_src/_src%20C%23/Generic%20Graphs/Graph%20Traversal/BreadthFirstSearch.cs)
        - [***Depth First Search***](CSharpProjectUnity/Assets/_src/_src%20C%23/Generic%20Graphs/Graph%20Traversal/DepthFirstSearch.cs)
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
- [**Helper Classes**](CSharpProjectUnity/Assets/_src/_src%20C%23/Helper%20Classes)
  - [***Enumeration Class***](CSharpProjectUnity/Assets/_src/_src%20C%23/Helper%20Classes/Enumeration%20Class)
  ```cs
  using MirJan.Helpers;
    
    public class ExampleEnumeration : Enumeration
    {
        public static readonly ExampleEnumeration EXAMPLE_ONE = new ExampleEnumeration(nameof(EXAMPLE_ONE));
        public static readonly ExampleEnumeration EXAMPLE_TWO = new ExampleEnumeration(nameof(EXAMPLE_TWO));
        
        private ExampleEnumeration(string name) : base(name) { }
    }
  ```
  - [***Finite State Machine & State***](CSharpProjectUnity/Assets/_src/_src%20C%23/Helper%20Classes/Finite%20State%20Machine)
    - [***Finite State Machine***](CSharpProjectUnity/Assets/_src/_src%20C%23/Helper%20Classes/Finite%20State%20Machine/FiniteStateMachine.cs)
    - [***State***](CSharpProjectUnity/Assets/_src/_src%20C%23/Helper%20Classes/Finite%20State%20Machine/FiniteStateMachineState.cs)
    <br>The state used by the [***Finite State Machine***](CSharpProjectUnity/Assets/_src/_src%20C%23/Helper%20Classes/Finite%20State%20Machine/FiniteStateMachine.cs).
   ```cs
    using MirJan.Helpers;

    public class Example
    {
        public FiniteStateMachine FSM = new FiniteStateMachine();

        void ExampleMethod()
        {
            FSM.Add(0, new ExampleState(FSM));
        }
    }

    public class ExampleState : FiniteStateMachineState
    {
        public ExampleState(FiniteStateMachine fsm) : base(fsm) { }

        public ovveride void Enter() { }
        public ovveride void Exit() { }
        public ovveride void Update() { }
        public ovveride void FixedUpdate() { }
        public ovveride void LateUpdate() { }
    }
   ```
- [**Priority Queues**](CSharpProjectUnity/Assets/_src/_src%20C%23/Priority%20Queues)
  - [***D-Ary Heap***](CSharpProjectUnity/Assets/_src/_src%20C%23/Priority%20Queues/DAryHeap.cs)
  ```cs
  using MirJan.PriorityQueues;
  
  public class Example
  {
    DaryHeap<Number> dAryHeap = new DaryHeap(10);
  }
  
  public class Number : IDaryHeap<Number>
  {
    public int number; 
    
    public int HeapIndex {get; set;}
    
    public int CompareTo(Number other)
    {
        return number.CompareTo(other.number);
    }
  }
  ```
  - [***Pairing Heap***](CSharpProjectUnity/Assets/_src/_src%20C%23/Priority%20Queues/PairingHeap.cs)
  ```cs
  using MirJan.PriorityQueues;
  
  public class Example
  {
    PairingHeap<Number> pairingHeap = new PairingHeap();
  }
  
  public class Number : IPairingHeap<Number>
  {
    public int number; 
    
    public PairingHeap<Number>.PairingHeapNode HeapNode {get; set;}
    
    public int CompareTo(Number other)
    {
        return number.CompareTo(other.number);
    }
  }
  ```
## [Unity Code](CSharpProjectUnity/Assets/_src/_src%20Unity)
- [**Helper Classes**](CSharpProjectUnity/Assets/_src/_src%20Unity/Helper%20Classes)
- [**Path Finding**](CSharpProjectUnity/Assets/_src/_src%20Unity/Pathfinding)
