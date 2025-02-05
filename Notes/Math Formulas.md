# Math Formulas
## Physical Distances
Distance is a numerical measurement of how far apart two objects or points are.
### Euclidean Distance
The Euclidean distance between two points in euclidean space is the length of a line segment between the two points.
#### 1 Dimension
The distance between any two points on the real line is the absolute value of the numerical difference of their coordinates. Thus if $p$ and $q$ are two points on the real line, then the distance between them, $d$, is given by:
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
In the Euclidean plane, let point $p$ have Cartesian coordinates $(p_1, p_2)$ and let point $q$ have coordinates $(q_1, q_2)$. Then the distance between them, $d$, is given by:
```math
d(p, q) = \sqrt{(q_1 - p_1)^2 + (q_2 - p_2)^2}
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
In the Euclidean plane, let point $p$ have Cartesian coordinates $(p_1, p_2, p_3)$ and let point $q$ have coordinates $(q_1, q_2, q_3)$. Then the distance between them, $d$, is given by:
```math
d(p, q) = \sqrt{(q_1 - p_1)^2 + (q_2 - p_2)^2 + (q_3 - p_3)^2}
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
In the Euclidean plane, let point $p$ have Cartesian coordinates $(p_1, p_2)$ and let point $q$ have coordinates $(q_1, q_2)$. Then the distance between them, $d$, is given by:
```math
d(p, q) = |p_1 - q_1| + |p_2 - q_2|
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
In the Euclidean plane, let point $p$ have Cartesian coordinates $(p_1, p_2, p_3)$ and let point $q$ have coordinates $(q_1, q_2, q_3)$. Then the distance between them, $d$, is given by:
```math
d(p, q) = |p_1 - q_1| + |p_2 - q_2| + |p_3 - q_3|
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
In the Euclidean plane, let point $p$ have Cartesian coordinates $(p_1, p_2)$ and let point $q$ have coordinates $(q_1, q_2)$. Then the distance between them, $d$, is given by:
```math
d(p, q) = max(|p_1 - q_1|, |p_2 - q_2|)
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
In the Euclidean plane, let point $p$ have Cartesian coordinates $(p_1, p_2, p_3)$ and let point $q$ have coordinates $(q_1, q_2, q_3)$. Then the distance between them, $d$, is given by:
```math
d(p, q) = max(|p_1 - q_1|, |p_2 - q_2|, |p_3 - q_3|)
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

In the Euclidean plane, let point $p$ have Cartesian coordinates $(p_1, p_2)$ and let point $q$ have coordinates $(q_1, q_2)$. Then the distance between them, $d$, is given by:
```math
d(p, q) = max(|p_1 - q_1|, |p_2 - q_2|) + (\sqrt{2} - 1) * min(|p_1 - q_1|, |p_2 - q_2|)
```
```cs
//Pseudocode
//Points p and q are represented as Vector2 variables where (p1, p2) and (q1, q2) = (p.x, p.y) and (q.x, q.y) respectively.
public float Distance(Vector2 p, Vector2 q)
{
  return Math.Max(Math.Abs(p.x - q.x), Math.Abs(p.y - q.y)) + (Math.Sqrt(2) - 1) * Math.Min(Math.Abs(p.x - q.x), Math.Abs(p.y - q.y));
}
```
This formula can be generalized, as shown below,
```math
d(p, q) = max(|p_1 - q_1|, |p_2 - q_2|) + (D - 1) * min(|p_1 - q_1|, |p_2 - q_2|)
```
Where when $D = 1$, the formula will regress to the Chebyshev distance.
<p align="center"><img src="/../main/Resources/ChebyshevDistance.PNG"></p>
<br>When $D = 2$, the formula will turn into the Manhattan distance.
<p align="center"><img src="/../main/Resources/ManhattanDistance.PNG"></p>
<br> Octile distance is when $D = \sqrt2$
<p align="center"><img src="/../main/Resources/OctileDistance.png"></p>

### Sexvigintile Distance
Sexvigintile distance builds upon the Octile distance for 3 dimensional space.

Given two points, $p$ and $q$, which are distance $x∆$, $y∆$, and $z∆$ apart in each of the three dimensions respectively let,
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
//Pseudocode
//Points p and q are represented as Vector3 variables where (p1, p2, p3) and (q1, q2, q3) = (p.x, p.y, p.z)
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
This formula can be generalized, as shown below,
```math
d(p, q) = (D_2 - D_1) * dmin + (D_1 - 1) * dmid + dmax
```
Where when $D_2 = 1$ and $D_1 = 1$, the formula will regress to the Chebyshev distance.
<br>When $D_2 = 3$ and $D_1 = 2$, the formula will turn into the Manhattan distance.
<br>Sexvigintile distance is when $D_2 = \sqrt3$ and $D_1 = \sqrt2$
#### References
https://webdocs.cs.ualberta.ca/~nathanst/papers/voxels.pdf
### Relationship of Distances
#### 2 Dimensions
<p align="center">Scale</p>
<p align="center"><img src="/../main/Resources/2DScale.PNG"></p>
<p align="center">Relationship on a 2 dimensional graph</p>
<p align="center"><img src="/../main/Resources/RelationshipOf2DDistances.PNG"></p>

#### 3 Dimensions
<p align="center">Scale</p>
<p align="center"><img src="/../main/Resources/3DScale.PNG"></p>
<p align="center">Relationship on a 3 dimensional graph</p>

&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ![RelationshipOf3DDistances](https://user-images.githubusercontent.com/107838446/183699924-b9443992-30d3-4dc7-b174-14ac8c97b4a8.gif)

<p align="center">Euclidean = 3.00, Manhattan = 5.00, Chebyshev = 2.00, Sexvigintile = 3.15</p>

## Kinematics
### SUVAT Equations
The SUVAT Equations describe motion in a given direction when acceleration is a constant. The SUVAT equations are:
```math
v = u + at
```
```math
s = ut + {{at^2}\over 2}
```
```math
v^2 = u^2 + 2as
```
```math
s = {{v + u}\over 2}t
```
```math
s = vt - {{at^2}\over 2}
```
Where $s$ is displacement, $u$ is initial velocity, $v$ is final velocity, $a$ is acceleration and $t$ is total time.
