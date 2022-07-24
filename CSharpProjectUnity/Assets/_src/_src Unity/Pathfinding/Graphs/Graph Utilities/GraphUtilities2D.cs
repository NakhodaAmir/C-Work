namespace MirJan
{
    namespace Unity
    {
        namespace PathFinding
        {
            namespace Graphs
            {
                namespace Utilities
                {
                    public static class GraphUtilities2D
                    {
                        private const int HORIZONTAL_VERTICAL_COST = 10;
                        private const int DIAGONAL_COST = 14;

                        public static float EuclideanDistance(UnityEngine.Vector2Int nodeA, UnityEngine.Vector2Int nodeB)
                        {
                            DeltaDistance(nodeA, nodeB, out float dX, out float dY);

                            return (float)(HORIZONTAL_VERTICAL_COST * System.Math.Sqrt(dX * dX + dY * dY));
                        }

                        private static void DeltaDistance(UnityEngine.Vector2Int nodeA, UnityEngine.Vector2Int nodeB, out float dX, out float dY)
                        {
                            dX = System.Math.Abs(nodeA.x - nodeB.x);
                            dY = System.Math.Abs(nodeA.y - nodeB.y);
                        }

                        public static class GridUtilities
                        {
                            public static float ManhattenDistance(UnityEngine.Vector2Int nodeA, UnityEngine.Vector2Int nodeB)
                            {
                                DeltaDistance(nodeA, nodeB, out float dX, out float dY);

                                return HORIZONTAL_VERTICAL_COST * (dX + dY);
                            }

                            public static float OctileDistance(UnityEngine.Vector2Int nodeA, UnityEngine.Vector2Int nodeB)
                            {
                                DeltaDistance(nodeA, nodeB, out float dX, out float dY);

                                if (dX > dY) return HORIZONTAL_VERTICAL_COST * (dX - dY) + DIAGONAL_COST * dY;

                                return HORIZONTAL_VERTICAL_COST * (dY - dX) + DIAGONAL_COST * dX;
                            }

                            public static float ChebyshevDistance(UnityEngine.Vector2Int nodeA, UnityEngine.Vector2Int nodeB)
                            {
                                DeltaDistance(nodeA, nodeB, out float dX, out float dY);

                                if (dX > dY) return HORIZONTAL_VERTICAL_COST * (dX - dY) + HORIZONTAL_VERTICAL_COST * dY;

                                return HORIZONTAL_VERTICAL_COST * (dY - dX) + HORIZONTAL_VERTICAL_COST * dX;
                            }
                        }
                    }
                }
            }
        }       
    }
}
