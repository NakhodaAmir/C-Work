# Pairing Heap

| n | Total number of elements in the heap |
| ------------- | ------------- |

| Operation  | Time Complexity | Description |
| ------------- | ------------- | ------------ |
| Count     | <p align='center'>$Θ$(1)</p> | Returns the number of elements in the heap |
| Peek    | <p align='center'>$Θ$(1)</p> | Returns the smallest element in a Min-Heap or the largest element in a Max-Heap |
| IsEmpty     | <p align='center'>$Θ$(1)</p> | Returns whether the heap is empty or not |
| Insert  | <p align='center'>$Θ$(1)</p>  | Inserts and element into the heap |
| Extract Min  | <p align='center'>$O(n)$<sup>[0](#actualtime)</sup><br>$O(log n)$<sup>[1](#amortizedtime)</sup> | Removes and returns the smallest element in a Min-Heap or the largest element in a Max-Heap |
| Contains     | <p align='center'>$Θ$(1)</p> | Returns whether the heap contains an element or not |
| Update (Decrease Key for Min-Heap/ Increase Key for Max-Heap) | <p align='center'>$O$(1)<sup>[0](#actualtime)</sup><br>$o(log n)$<sup>[1](#amortizedtime) [2](#lowerupperbound)</sup></p> | Updates an element in the heap |
| Meld | <p align='center'>$Θ$(1)</p> | Melds two heaps into one heap |

<a name="actualtime">0</a>: Actual Time
<br><a name="amortizedtime">1</a>: Amortized Time
<br><a name="lowerupperbound">2</a>: Lower bound of Ω(loglog n). Upper bound O(2<sup>2&radic;loglog n</sup>)

# Example Code
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
