# Asymptotic Analysis
Asymptotic analysis of an algorithm refers to defining the mathematical boundation/framing of its run-time performance. Using asymptotic analysis, we can very well conclude the best case, average case, and worst case scenario of an algorithm.

Asymptotic analysis is input bound i.e., if there's no input to the algorithm, it is concluded to work in a constant time. Other than the "input" all other factors are considered constant.

Asymptotic notations
- Big-O (**O**)  notation (Worst Case)
- Little-o (**o**) notation
- Big-Omega (**Ω**) notation (Best Case)
- Little-omega (**ω**) notation
- Big-Theta (**Θ**) notation (Average Case)

## <a name="onotation">O-notation</a>
Big-O notation represents the tight upper bound of the running time of an algorithm, even though, as written, it can also be a loose upper-bound. Big-O measures the longest amount of time an algorithm can possibly take to complete. Thus, it gives the worst-case complexity of an algorithm.
### o-notation
To make Big-O's role as a tight upper-bound more clear, Little-o notationm is used to describe an upper-bound that cannot be asymptotically tight. Thus, Little-o means loose upper bound of the running time of an algorithm. Little-o is a rough estimate of the maximum order of growth whereas Big-Ο may be the actual order of growth. 
## Ω-notation
Big Omega notation represents the tight lower bound of the running time of an algorithm. It measures the best amount of time an algorithm can possibly take to complete. Thus, it provides the best case complexity of an algorithm.
### ω-notation
Big-Omega is the tight lower bound notation and Little-omega describes the loose lower bound. Little-omega is a rough estimate of the order of growth whereas Big-Omega may represent the exact order of growth. We use little-omega to denote a lower bound that is not asymptotically tight.

## Relationship between **O, o, Ω, ω**
| f(n) | The running time of an algorithm |
| -| - |

![Image](/../main/Resources/Analysis-of-Algorithms-little-o-omega.png)

## Θ-notation
Big Theta notation encloses the function from above and below. Since it represents the upper and the lower bound of the running time of an algorithm, it is used for analyzing the average-case complexity of an algorithm.

## References
https://www.programiz.com/dsa/asymptotic-notations#:~:text=Theta%20Notation%20(%CE%98%2Dnotation),case%20complexity%20of%20an%20algorithm.
https://www.tutorialspoint.com/data_structures_algorithms/asymptotic_analysis.htm#:~:text=Asymptotic%20analysis%20refers%20to%20computing,as%20g(n2).
https://www2.cs.arizona.edu/classes/cs345/summer14/files/bigO.pdf
https://www.geeksforgeeks.org/analysis-of-algorithems-little-o-and-little-omega-notations/

# Amortized Analysis
Amortized analysis is a method of analyzing algorithms that can help us determine an upper bound on the complexity of an algorithm. This is particular useful when analyzing operations on data structures, when they involve slow, rarely occurring operations and fast, more common operations. With this disparity between each operations’ complexity, it is difficult to get a tight bound on the overall complexity of a sequence of operations using worst-case analysis. Amortized analysis provides us with a way of averaging the slow and fast operations together to obtain a tight upper bound on the overall algorithm runtime.

Amortized analysis differs from average-case analysis in that probability is not involved; an amortized analysis guarantees the average performance of each operation in the worst case . 

There are three main types of amortized analysis: 
- Aggregate analysis
- Accounting method
- Potential method.
## References
http://www2.hawaii.edu/~nodari/teaching/s16/notes/notes01.pdf
https://fdocuments.net/document/binary-heap-d-ary-heap-binomial-heap-amortized-analysis-amortized-complexity.html?page=19

# Algorithm Analysis
## O-notation Algorithm Analysis
![<Image](/../main/Resources/Big-o%20Notation%20Algorithm%20Analysis.png)
## Common Asymptotic Notations
|  Asymptotic Notations | Time Complexity |
| ---- | ---- |
| Constant | O(1) |
| Logarithmic | O(log n) |
| Linear | O(n) |
| Quadratic | O(n<sup>2</sup>) |
