| n | Total number of elements in the heap |
| ------------- | ------------- |

| d | The degree of the heap. Represents the number of children a node, in the heap, has |
| ------------- | ------------- |

| Operation  | Time Complexity | Description |
| ------------- | ------------- | ------------ |
| Count     | <p align='center'>Θ(1)</p> | Returns the number of elements in the heap |
| Peek    | <p align='center'>Θ(1)</p> | Returns the smallest element in a Min-Heap or the largest element in a Max-Heap |
| IsEmpty     | <p align='center'>Θ(1)</p> | Returns whether the heap is empty or not |
| Insert  | <p align='center'>Θ(log<sub>d</sub> n)</p>  | Inserts and element into the heap |
| Extract Min  | <p align='center'>Θ(d log<sub>d)</sub> n</p> | Removes and returns the smallest element in a Min-Heap or the largest element in a Max-Heap |
| Contains     | <p align='center'>Θ(1)</p> | Returns whether the heap contains an element or not |
| Update (Decrease Key for Min-Heap/ Increase Key for Max-Heap) | <p align='center'>Θ(log<sub>d</sub> n)</p> | Updates an element in the heap |

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
