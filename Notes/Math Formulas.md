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
In the Euclidean plane, let point ***p*** have Cartesian coordinates (***p***<sub>1</sub>, ***p***<sub>2</sub>) and let point ***q*** have coordinates (***q***<sub>1</sub>, ***q***<sub>2</sub>). Then the distance between them, ***d***, is given by:
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
#### 3 Dimension
In the Euclidean plane, let point ***p*** have Cartesian coordinates (***p***<sub>1</sub>, ***p***<sub>2</sub>, ***p***<sub>3</sub>) and let point ***q*** have coordinates (***q***<sub>1</sub>, ***q***<sub>2</sub>, ***q***<sub>3</sub>). Then the distance between them, ***d***, is given by:
```math
d(p, q) = \sqrt{(q1 - p1)^2 + (q2 - p2)^2 + (q3 - p3)^2}
```
```cs
//Pseudocode
//Points p and q are represented as Vector3 variables where (p1, p2, p3) and (q1, q2, q3) = (p.x, p.y, p.z)
//and (q.x, q.y, q.z) respectively.
public float Distance(Vector3 p, Vector3 q)
{
  return  Math.Sqrt(((q.x - p.x) * (q.x - p.x) + (q.y - p.y) * (q.y - p.y) + (q.z - p.z) * (q.z - p.z)));
}
```
### Manhattan Distance
Also known as the taxicab geometry is a form of geometry in which the usual distance function or metric of Euclidean geometry is replaced by a new metric in which the distance between two points is the sum of the absolute differences of their Cartesian coordinates.
#### 2 Dimension
In the Euclidean plane, let point ***p*** have Cartesian coordinates (***p***<sub>1</sub>, ***p***<sub>2</sub>) and let point ***q*** have coordinates (***q***<sub>1</sub>, ***q***<sub>2</sub>). Then the distance between them, ***d***, is given by:
```math
d(p, q) = |p1 - q1| + |p2 - q2|
```
```cs
//Pseudocode
//Points p and q are represented as Vector2 variables where (p1, p2) and (q1, q2) = (p.x, p.y) and (q.x, q.y) respectively.
public float Distance(Vector2 p, Vector2 q)
{
  return  Math.Abs(p.x - q.x) + Math.Abs(p.y - q.y);
}
```
#### 3 Dimension
In the Euclidean plane, let point ***p*** have Cartesian coordinates (***p***<sub>1</sub>, ***p***<sub>2</sub>, ***p***<sub>3</sub>) and let point ***q*** have coordinates (***q***<sub>1</sub>, ***q***<sub>2</sub>, ***q***<sub>3</sub>). Then the distance between them, ***d***, is given by:
```math
d(p, q) = |p1 - q1| + |p2 - q2| + |p3 - q3|
```
```cs
//Pseudocode
//Points p and q are represented as Vector3 variables where (p1, p2, p3) and (q1, q2, q3) = (p.x, p.y, p.z)
//and (q.x, q.y, q.z) respectively.
public float Distance(Vector3 p, Vector3 q)
{
  return  Math.Abs(p.x - q.x) + Math.Abs(p.y - q.y) + Math.Abs(p.z - q.z);
}
```
### Chebyshev Distance
Chebyshev distance is a distance metric which is the maximum absolute distance in one dimension of two N dimensional points.
#### 2 Dimenstion
In the Euclidean plane, let point ***p*** have Cartesian coordinates (***p***<sub>1</sub>, ***p***<sub>2</sub>) and let point ***q*** have coordinates (***q***<sub>1</sub>, ***q***<sub>2</sub>). Then the distance between them, ***d***, is given by:
```math
d(p, q) = max(|p1 - q1|, |p2 - q2|)
```
```cs
//Pseudocode
//Points p and q are represented as Vector2 variables where (p1, p2) and (q1, q2) = (p.x, p.y) and (q.x, q.y) respectively.
public float Distance(Vector2 p, Vector2 q)
{
  return  Math.Max(Math.Abs(p.x - q.x), Math.Abs(p.y - q.y));
}
```
#### 3 Dimension
In the Euclidean plane, let point ***p*** have Cartesian coordinates (***p***<sub>1</sub>, ***p***<sub>2</sub>, ***p***<sub>3</sub>) and let point ***q*** have coordinates (***q***<sub>1</sub>, ***q***<sub>2</sub>, ***q***<sub>3</sub>). Then the distance between them, ***d***, is given by:
```math
d(p, q) = max(|p1 - q1|, |p2 - q2|, |p3 - q3|)
```
```cs
//Pseudocode
//Points p and q are represented as Vector3 variables where (p1, p2, p3) and (q1, q2, q3) = (p.x, p.y, p.z)
//and (q.x, q.y, q.z) respectively.
public float Distance(Vector3 p, Vector3 q)
{
  return  Math.Max(Math.Abs(p.x - q.x) + Math.Abs(p.y - q.y) + Math.Abs(p.z - q.z));
}
```
### Octile Distance
Octile distance is the distance between two points where the respective lengths of cardinal and ordanal moves are 1 and $\sqrt{2}$ in 2 dimensions.

In the Euclidean plane, let point ***p*** have Cartesian coordinates (***p***<sub>1</sub>, ***p***<sub>2</sub>) and let point ***q*** have coordinates (***q***<sub>1</sub>, ***q***<sub>2</sub>). Then the distance between them, ***d***, is given by:
```math
d(p, q) = \sqrt{2} * min(|p1 - q1|, |p2 - q2|) + ||p1 - q1| - |p2 - q2||
```
```cs
//Pseudocode
//Points p and q are represented as Vector2 variables where (p1, p2) and (q1, q2) = (p.x, p.y) and (q.x, q.y) respectively.
public float Distance(Vector2 p, Vector2 q)
{
  return  Math.Sqrt(2) * Math.Min(Math.Abs(p.x - q.x), Math.Abs(p.y - q.y)) + Math.Abs(Math.Abs(p.x - q.x) - Math.Abs(p.y - q.y));
}
```
### Sexvigintile Distance
Sexvigintile distance, which builds upon the octile distance, is the distance between two points in 3 dimensional space.

Given two points, ***p*** and ***q***, which are distance x∆, y∆, and z∆ apart in each of the three dimensions respectively let,
```math
dmax = max(x∆, y∆, z∆)
```
```math
dmin = min(x∆, y∆, z∆)
```
```math
dmid = \lbrace x∆, y∆, z∆ \rbrace - \lbrace dmax, dmin \rbrace
```
Then the distance between these points, ***d***, is given by:
```math
d(p, q) = (\sqrt{3} - \sqrt{2}) * dmin + (\sqrt{2} - 1) * dmid + dmax
```
```cs
public float Distance(Vector3 p, Vector3 q)
{
    float dX = x∆;
    float dY = y∆;
    float dZ = z∆;
     
    //Function sorts the 3 delta distances and returns dmin, dmid and dmax which are the minimum delta distance, 
    //middle delta distance and max delta distance respectively
    Sort3(dX, dY, dZ, out float dmin, out float dmid, out float dmax);

    return (Math.Sqrt(3) - Math.Sqrt(2)) * dmin + (Math.Sqrt(2) - 1) * dmid + dmax;
}
```
#### References
https://webdocs.cs.ualberta.ca/~nathanst/papers/voxels.pdf
