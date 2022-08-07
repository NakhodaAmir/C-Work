# Time Complexity

| n | Total number of elements in the heap |
| ------------- | ------------- |

| Operation  | Paring Heap | D-Ary Heap |
| ------------- | ------------- | ------------ |
| Count     | <p align='center'>**Θ**(1)</p> | <p align='center'>**Θ**(1)</p> |
| Peek    | <p align='center'>**Θ**(1)</p> | <p align='center'>**Θ**(1)</p> |
| IsEmpty     | <p align='center'>**Θ**(1)</p> | <p align='center'>**Θ**(1)</p> |
| Insert  | <p align='center'>**Θ**(1)</p>  | <p align='center'>**Θ**(log<sub>d</sub> n)</p> |
| Extract Min  | <p align='center'>**O**(n)<sup>[0](#actualtime)</sup>/**O**(log n)<sup>[1](#amortizedtime)</sup> | <p align='center'>**Θ**(d log<sub>d)</sub> n</p> |
| Contains     | <p align='center'>**Θ**(1)</p> | <p align='center'>**Θ**(1)</p> |
| Update (Decrease Key for Min-Heap/ Increase Key for Max-Heap) | <p align='center'>**O**(1)<sup>[0](#actualtime)</sup>/**o**(log n)<sup>[1](#amortizedtime) [2](#lowerupperbound)</sup></p> | <p align='center'>**Θ**(log<sub>d</sub> n)</p> |
| Meld | <p align='center'>**Θ**(1)</p> | Nil |

<a name="actualtime">0</a>: Actual Time
<br><a name="amortizedtime">1</a>: Amortized Time
<br><a name="lowerupperbound">2</a>: Lower bound of Ω(loglog n). Upper bound O(2<sup>2&radic;loglog n</sup>)
