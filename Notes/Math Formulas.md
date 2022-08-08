# Math Formulas
## Physical Distances
Distance is a numerical measurement of how far apart two objects or points are.

### Euclidean Distance
The Euclidean distance between two points in euclidean space is the length of a line segment between the two points.
#### 1 Dimension
The distance between any two points on the real line is the absolute value of the numerical difference of their coordinates. Thus if ***p*** and ***q*** are two points on the real line, then the distance between them, ***d***, is given by:
```math
d(p, q) = |p - q|
```
```cs
//Pseudocode
public float Distance(float p, float q)
{
  return Math.Abs(p - q);
}
```
#### 2 Dimension
In the Euclidean plane, let point ***p*** have Cartesian coordinates (***p***<sub>1</sub>, ***p***<sub>2</sub>) and let point **q*** have coordinates (***q***<sub>1</sub>, ***q***<sub>2</sub>). Then the distance between them, ***d***, is given by:
```math
d(p, q) = \sqrt{(q1 - p1)^2 + (q2 - p2)^2}
```
```cs
//Pseudocode
//Points p and q are represented as Vector2 variables where (p1, p2) and (q1, q2) = (p.x, p.y) and (q.x, q.y) respectively.
public float Distance(Vector2 p, Vector2 q)
{
  return  Math.Sqrt(((q.x - p.x) * (q.x - p.x) + (q.y - p.y) * (q.y - p.y)));
}
```
### Manhattan Distance

### Chebyshev Distance
