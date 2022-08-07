# Path Finding
- [**Path Finder**](PathFinder.cs)
<br>Path finding class that make use of the graph search algorithm, Fringe Search. Returns a path, a list of Vector3 waypoints, to the [**Path Finding Agent**](PathFindingAgent.cs).
- [**Path Finder Manager**](PathFinderManager.cs)
<br>Base class of all [**Graph**](Graphs) classes.
- [**Path Finding Agent**](PathFindingAgent.cs)
<br>Inherit this class to create a [**Path Finding Agent**](PathFindingAgent.cs). Request a path while specifying a target position. Once a path is found, a list of Vector3 waypoints will be returned.
- [**Path Request**](PathRequest.cs)
<br> Struct that contains the:
  - Start Position; to define where the [**Path Finder**](PathFinder.cs) will start.
  - Target Position; to define what the [**Path Finder**](PathFinder.cs) will search for.
  - Call Back Function; to define where to return the path found by the [**Path Finder**](PathFinder.cs).
- [**Path Request Manager**](PathRequestManager.cs)
<br>Manages all requests that have been sent by the [**Path Finding Agent**](PathFindingAgent.cs)(s). Allocates these requests into seperate [**Path Finding Thread**](PathfindingThread.cs) equally. Number of [**Path Finding Thread**](PathfindingThread.cs)s that will be used is defined by the user from the [**Path Finder Manager**](PathFinderManager.cs).
- [**Path Finding Thread**](PathfindingThread.cs)
<br>A thread that uses the [**Path Finder**](PathFinder.cs). [**Path Request**](PathRequest.cs)s, sent by the [**Path Request Manager**](PathRequestManager.cs), requested by the [**Path Finding Agent**](PathFindingAgent.cs)(s) will be used as input data for the [**Path Finder**](PathFinder.cs).
- [**Graphs**](Graphs)
<br>Graphs that can be used for path finding.
  - [**Grid Graph**](Graphs/GridGraph.cs)
