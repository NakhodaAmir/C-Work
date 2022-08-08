namespace MirJan
{
    namespace Unity
    {
        namespace Helpers
        {
            using System;
            using UnityEngine;

            public static class MoreMath
            {
                public static int Max(int x, int y, int z)
                {
                    return Math.Max(x, Math.Max(y, z));
                }

                public static float Max(float x, float y, float z)
                {
                    return Math.Max(x, Math.Max(y, z));
                }

                public static int Min(int x, int y, int z)
                {
                    return Math.Min(x, Math.Min(y, z));
                }

                public static float Min(float x, float y, float z)
                {
                    return Math.Min(x, Math.Min(y, z));
                }

                public static void Sort3(float x, float y, float z, out float min, out float mid, out float max)
                {
                    if (x > y)
                    {
                        if (y > z)
                        {
                            max = x;
                            mid = y;
                            min = z;
                        }
                        else//dY is the smallest
                        {
                            min = y;

                            if (x > z)
                            {
                                max = x;
                                mid = z;
                            }
                            else//dX is the middle
                            {
                                max = z;
                                mid = x;
                            }
                        }
                    }
                    else//dY is bigger than dX
                    {
                        if (x > z)
                        {
                            max = y;
                            mid = x;
                            min = z;
                        }
                        else//dX is the smallest
                        {
                            min = x;

                            if (y > z)
                            {
                                max = y;
                                mid = z;
                            }
                            else
                            {
                                max = z;
                                mid = y;
                            }
                        }
                    }
                }

                public static void Sort3(int x, int y, int z, out int min, out int mid, out int max)
                {
                    if (x > y)
                    {
                        if (y > z)
                        {
                            max = x;
                            mid = y;
                            min = z;
                        }
                        else//dY is the smallest
                        {
                            min = y;

                            if (x > z)
                            {
                                max = x;
                                mid = z;
                            }
                            else//dX is the middle
                            {
                                max = z;
                                mid = x;
                            }
                        }
                    }
                    else//dY is bigger than dX
                    {
                        if (x > z)
                        {
                            max = y;
                            mid = x;
                            min = z;
                        }
                        else//dX is the smallest
                        {
                            min = x;

                            if (y > z)
                            {
                                max = y;
                                mid = z;
                            }
                            else
                            {
                                max = z;
                                mid = y;
                            }
                        }
                    }
                }

                #region Public Static Methods 3D
                public static float EuclideanDistance3D(Vector3 nodeA, Vector3 nodeB)
                {
                    DeltaDistance3D(nodeA, nodeB, out float dX, out float dY, out float dZ);

                    return (float)Math.Sqrt(dX * dX + dY * dY + dZ * dZ);
                }

                public static float ManhattanDistance3D(Vector3 nodeA, Vector3 nodeB)
                {
                    DeltaDistance3D(nodeA, nodeB, out float dX, out float dY, out float dZ);

                    return dX + dY + dZ;
                }

                public static float SexvigintileDistance(Vector3 nodeA, Vector3 nodeB)
                {
                    DeltaDistance3D(nodeA, nodeB, out float dX, out float dY, out float dZ);

                    Sort3(dX, dY, dZ, out float dMin, out float dMid, out float dMax);

                    return (float)(Math.Sqrt(3) - Math.Sqrt(2)) * dMin + ((float)Math.Sqrt(2) - 1) * dMid + dMax;
                }

                public static float ScaledSexvigintileDistance(Vector3 nodeA, Vector3 nodeB)
                {
                    DeltaDistance3D(nodeA, nodeB, out float dX, out float dY, out float dZ);

                    Sort3(dX, dY, dZ, out float dMin, out float dMid, out float dMax);

                    return 3 * dMin + 4 * dMid + dMax;
                }

                public static float ChebyshevDistance3D(Vector3 nodeA, Vector3 nodeB)
                {
                    DeltaDistance3D(nodeA, nodeB, out float dX, out float dY, out float dZ);

                    return Max(dX, dY, dZ);
                }
                #endregion

                #region Private Static Methods 3D
                public static void DeltaDistance3D(Vector3 nodeA, Vector3 nodeB, out float dX, out float dY, out float dZ)
                {
                    DeltaDistance2D(nodeA, nodeB, out dX, out dY);

                    dZ = Math.Abs(nodeA.z - nodeB.z);
                }
                #endregion

                #region Public Static Methods 2D
                public static float EuclideanDistance2D(Vector2 nodeA, Vector2 nodeB)
                {
                    DeltaDistance2D(nodeA, nodeB, out float dX, out float dY);

                    return (float)Math.Sqrt(dX * dX + dY * dY);
                }

                public static float ManhattanDistance2D(Vector2 nodeA, Vector2 nodeB)
                {
                    DeltaDistance2D(nodeA, nodeB, out float dX, out float dY);

                    return dX + dY;
                }

                public static float OctileDistance2D(Vector2 nodeA, Vector2 nodeB)
                {
                    DeltaDistance2D(nodeA, nodeB, out float dX, out float dY);

                    return Math.Max(dX, dY) + ((float)Math.Sqrt(2) - 1) * Math.Min(dX, dY);
                }

                public static float ScaledOctileDistance(Vector2 nodeA, Vector2 nodeB)
                {
                    DeltaDistance2D(nodeA, nodeB, out float dX, out float dY);

                    return 10 * Math.Max(dX, dY) + 4 * Math.Min(dX, dY);
                }

                public static float ChebyshevDistance2D(Vector2 nodeA, Vector2 nodeB)
                {
                    DeltaDistance2D(nodeA, nodeB, out float dX, out float dY);

                    return Math.Max(dX, dY);
                }
                #endregion

                #region Private Static Methods 2D
                private static void DeltaDistance2D(Vector2 nodeA, Vector2 nodeB, out float dX, out float dY)
                {
                    dX = Math.Abs(nodeA.x - nodeB.x);
                    dY = Math.Abs(nodeA.y - nodeB.y);
                }
                #endregion
            }
        }
    }
}
