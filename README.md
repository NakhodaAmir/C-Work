# [Source Code](CSharpProjectUnity/Assets/_src)
## [C# Specific Code](CSharpProjectUnity/Assets/_src/_src%20C%23)
- [**Generic Graphs**](CSharpProjectUnity/Assets/_src/_src%20C%23/Generic%20Graphs)
    - Create graph data structures with the [**Generic Graph Interfaces**](CSharpProjectUnity/Assets/_src/_src%20C%23/Generic%20Graphs/Graph%20Interfaces) and the [**Abstract Generic Graph Nodes**](CSharpProjectUnity/Assets/_src/_src%20C%23/Generic%20Graphs/Abstract%20Graph%20Nodes).
    - Available Graph Algorithms:
      - [**A\* Search**](CSharpProjectUnity/Assets/_src/_src%20C%23/Generic%20Graphs/Graph%20Search/Dynamic%20Graph%20Searcher%20Algorithms/AStarSearch.cs)
      - [**Dijkstra Search**](CSharpProjectUnity/Assets/_src/_src%20C%23/Generic%20Graphs/Graph%20Search/Dynamic%20Graph%20Searcher%20Algorithms/DijkstraSearch.cs)
      - [**Greedy Best First Search**](CSharpProjectUnity/Assets/_src/_src%20C%23/Generic%20Graphs/Graph%20Search/Dynamic%20Graph%20Searcher%20Algorithms/GreedyBestFirstSearch.cs)
      - [**Fringe Search**](CSharpProjectUnity/Assets/_src/_src%20C%23/Generic%20Graphs/Graph%20Search/FringeSearch.cs)
      - [**Breadth First Search**](CSharpProjectUnity/Assets/_src/_src%20C%23/Generic%20Graphs/Graph%20Traversal/BreadthFirstSearch.cs)
      - [**Depth First Search**](CSharpProjectUnity/Assets/_src/_src%20C%23/Generic%20Graphs/Graph%20Traversal/DepthFirstSearch.cs)
- [**Helper Classes**](CSharpProjectUnity/Assets/_src/_src%20C%23/Helper%20Classes)
  - [**Enumeration Class**](CSharpProjectUnity/Assets/_src/_src%20C%23/Helper%20Classes/Enumeration%20Class)
  - [**Finite State Machine & State**](CSharpProjectUnity/Assets/_src/_src%20C%23/Helper%20Classes/Finite%20State%20Machine)
    - [**Finite State Machine**](CSharpProjectUnity/Assets/_src/_src%20C%23/Helper%20Classes/Finite%20State%20Machine/FiniteStateMachine.cs)
    - [**State**](CSharpProjectUnity/Assets/_src/_src%20C%23/Helper%20Classes/Finite%20State%20Machine/FiniteStateMachineState.cs)
- [**Priority Queues**](CSharpProjectUnity/Assets/_src/_src%20C%23/Priority%20Queues)
  - [**D-Ary Heap**](CSharpProjectUnity/Assets/_src/_src%20C%23/Priority%20Queues/D-Ary%20Heap)
  - [**Pairing Heap**](CSharpProjectUnity/Assets/_src/_src%20C%23/Priority%20Queues/Pairing%20Heap)
## [Unity Code](CSharpProjectUnity/Assets/_src/_src%20Unity)
- [**Helper Classes**](CSharpProjectUnity/Assets/_src/_src%20Unity/Helper%20Classes)
  - [**Single Layer Mask**](CSharpProjectUnity/Assets/_src/_src%20Unity/Helper%20Classes/Single%20Layer%20Mask)
  - [**Singletons**](CSharpProjectUnity/Assets/_src/_src%20Unity/Helper%20Classes/Singletons)
    - Create Singletons with the [Persistent Singleton](CSharpProjectUnity/Assets/_src/_src%20Unity/Helper%20Classes/Singletons/PersistentSingleton.cs) and the [Singleton](CSharpProjectUnity/Assets/_src/_src%20Unity/Helper%20Classes/Singletons/Singleton.cs) class.
- [**Path Finding**](CSharpProjectUnity/Assets/_src/_src%20Unity/Pathfinding)
    - Create path finding agents with the [**Path Finding Agent**](CSharpProjectUnity/Assets/_src/_src%20Unity/Pathfinding/PathFindingAgent.cs) class.
    - Multithreaded Path Finding, using Fringe Search, for 3D environments.
    - Available Path Finding [**Graphs**](CSharpProjectUnity/Assets/_src/_src%20Unity/Pathfinding/Graphs):
        - [**Square Grid Graph**](CSharpProjectUnity/Assets/_src/_src%20Unity/Pathfinding/Graphs/SquareGridGraph.cs)
# [**Notes**](Notes)
## [**Data Structures & Algorithms**](Notes/Data%20Structures%20and%20Algorithms.md)
- [**Asymptotic Analysis**](Notes/Data%20Structures%20and%20Algorithms.md#asymptotic-analysis)
  - [**Big-O Notation**](Notes/Data%20Structures%20and%20Algorithms.md#o-notation)
  - [**Little-o Notation**](Notes/Data%20Structures%20and%20Algorithms.md#o-notation-1)
  - [**Big-Omega Notation**](Notes/Data%20Structures%20and%20Algorithms.md#ω-notation)
  - [**Little-omega Notation**](Notes/Data%20Structures%20and%20Algorithms.md#ω-notation-1)
  - [**Big-Theta Notation**](Notes/Data%20Structures%20and%20Algorithms.md#θ-notation)
- [**Amortized Analysis**](Notes/Data%20Structures%20and%20Algorithms.md#amortized-analysis)
- [**Algorithm Analysis**](Notes/Data%20Structures%20and%20Algorithms.md#algorithm-analysis)
## [**Math Formulas**](Notes/Math%20Formulas.md)
- [**Physical Distance**](Notes/Math%20Formulas.md#physical-distances)
<br> Formulas to calculate the physical distance between two points in euclidean space.
    - [**Euclidean Distance**](Notes/Math%20Formulas.md#euclidean-distance)
    <br>1 - 3 Dimensions
    - [**Manhattan Distance**](Notes/Math%20Formulas.md#manhattan-distance)
    <br>2 - 3 Dimensions
    - [**Chebyshev Distance**](Notes/Math%20Formulas.md#chebyshev-distance)
    <br>2 - 3 Dimensions
    - [**Octile Distance**](Notes/Math%20Formulas.md#octile-distance)
    <br>2 Dimensions
    - [**Sexvigintile Distance**](Notes/Math%20Formulas.md#sexvigintile-distance)
    <br>3 Dimensions
## [**How Tos**](Notes/How%20Tos.md)
- [**Thread Safe Fully Lazy Singleton**](Notes/How%20Tos.md#ThreadSafeFullyLazySingleton)
