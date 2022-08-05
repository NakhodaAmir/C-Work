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
                        #region Constant Variables
                        private const int HORIZONTAL_VERTICAL_COST = 10;
                        private const int DIAGONAL_COST = 14;
                        #endregion

                        #region Public Static Methods
                        public static float EuclideanDistance(UnityEngine.Vector2Int nodeA, UnityEngine.Vector2Int nodeB)
                        {
                            DeltaDistance(nodeA, nodeB, out float dX, out float dY);

                            return (float)(HORIZONTAL_VERTICAL_COST * System.Math.Sqrt(dX * dX + dY * dY));
                        }
                        #endregion

                        #region Private Static Methods
                        private static void DeltaDistance(UnityEngine.Vector2Int nodeA, UnityEngine.Vector2Int nodeB, out float dX, out float dY)
                        {
                            dX = System.Math.Abs(nodeA.x - nodeB.x);
                            dY = System.Math.Abs(nodeA.y - nodeB.y);
                        }
                        #endregion

                        #region Grid Utilities
                        public static class GridUtilities
                        {
                            #region Public Static Methods
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
                            #endregion
                        }
                        #endregion
                    }
                }
            }
        }       
    }
}
