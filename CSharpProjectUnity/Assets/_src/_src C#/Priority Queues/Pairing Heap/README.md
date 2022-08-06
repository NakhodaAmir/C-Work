| n | Total number of elements in the heap |
| ------------- | ------------- |

| Operation  | Time Complexity | Description |
| ------------- | ------------- | ------------ |
| Count     | <p align='center'>1</p> | Returns the number of elements in the heap |
| Peek    | <p align='center'>1</p> | Returns the smallest element in a Min-Heap or the largest element in a Max-Heap |
| IsEmpty     | <p align='center'>1</p> | Returns whether the heap is empty or not |
| Insert  | <p align='center'>1</p>  | Inserts and element into the heap |
| Extract Min  | <p align='center'>log n</p> | Removes and returns the smallest element in a Min-Heap or the largest element in a Max-Heap |
| Contains     | <p align='center'>1</p> | Returns whether the heap contains an element or not |
| Update (Decrease Key for Min-Heap/ Increase Key for Max-Heap) | <p align='center'>log n</p> | Updates an element in the heap |
| Meld | <p align='center'>log n</p> | Melds two heaps into one heap |
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
