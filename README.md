# [Source Code](CSharpProjectUnity/Assets/_src)
## [C# Specific Code](CSharpProjectUnity/Assets/_src/_src%20C%23)
- [**Generic Graphs**](CSharpProjectUnity/Assets/_src/_src%20C%23/Generic%20Graphs)
  <br>***Contents***
    - [***Abstract Graph Algorithm***](CSharpProjectUnity/Assets/_src/_src%20C%23/Generic%20Graphs/Abstract%20Graph%20Algorithm)
      <br>The generic abstract class used to create the ***Graph Algorithms***.
    - [***Abstract Graph Nodes***](CSharpProjectUnity/Assets/_src/_src%20C%23/Generic%20Graphs/Abstract%20Graph%20Nodes)
      <br>Represents a vertex of the graph data structure. The type of node is relative to the type of graph denoted by its [***Graph Interface***]().
       - [***GraphSearchableNode***](CSharpProjectUnity/Assets/_src/_src%20C%23/Generic%20Graphs/Abstract%20Graph%20Nodes/GraphSearchableNode.cs)
        <br> Present in graphs inheriting the [***Graph Interface: IGraphSearchable***](CSharpProjectUnity/Assets/_src/_src%20C%23/Generic%20Graphs/Graph%20Interfaces/IGraphTraversable.cs).
        - [***GraphTraversableNode***](CSharpProjectUnity/Assets/_src/_src%20C%23/Generic%20Graphs/Abstract%20Graph%20Nodes/GraphTraversableNode.cs)
        <br> Present in graphs inheriting the [***Graph Interface: IGraphTraversable***](CSharpProjectUnity/Assets/_src/_src%20C%23/Generic%20Graphs/Graph%20Interfaces/IGraphTraversable.cs).
    - [***Graph Interfaces***](CSharpProjectUnity/Assets/_src/_src%20C%23/Generic%20Graphs/Graph%20Interfaces)
      <br> Interface used to create a graph data structure.
      - [***IGraphSearchable***](CSharpProjectUnity/Assets/_src/_src%20C%23/Generic%20Graphs/Graph%20Interfaces/IGraphTraversable.c)
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
- [**Helper Class**](CSharpProjectUnity/Assets/_src/_src%20C%23/Helper%20Classes)
- [**Priority Queues**](CSharpProjectUnity/Assets/_src/_src%20C%23/Priority%20Queues)
## [Unity Code](CSharpProjectUnity/Assets/_src/_src%20Unity)
- [**Helper Class**](CSharpProjectUnity/Assets/_src/_src%20Unity/Helper%20Classes)
- [**Path Finding**](CSharpProjectUnity/Assets/_src/_src%20Unity/Pathfinding)
