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
