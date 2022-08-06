# [Source Code](CSharpProjectUnity/Assets/_src)
## [C# Specific Code](CSharpProjectUnity/Assets/_src/_src%20C%23)
- [**Generic Graphs**](CSharpProjectUnity/Assets/_src/_src%20C%23/Generic%20Graphs)
- [**Helper Classes**](CSharpProjectUnity/Assets/_src/_src%20C%23/Helper%20Classes)
  - [***Enumeration Class***](CSharpProjectUnity/Assets/_src/_src%20C%23/Helper%20Classes/Enumeration%20Class)
  - [***Finite State Machine & State***](CSharpProjectUnity/Assets/_src/_src%20C%23/Helper%20Classes/Finite%20State%20Machine)
    - [***Finite State Machine***](CSharpProjectUnity/Assets/_src/_src%20C%23/Helper%20Classes/Finite%20State%20Machine/FiniteStateMachine.cs)
    - [***State***](CSharpProjectUnity/Assets/_src/_src%20C%23/Helper%20Classes/Finite%20State%20Machine/FiniteStateMachineState.cs)
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
