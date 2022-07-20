namespace MirJan
{
    namespace GenericGraphs
    {
        namespace GraphUtilites
        {
            public static class GridUtilities
            {
                private const int DIAGONAL_COST = 14;

                public static float ManhattenDistance(int nodeAX, int nodeAY, int nodeBX, int nodeBY)
                {
                    Graph2DUtilities.DeltaDistance(nodeAX, nodeAY, nodeBX, nodeBY, out float dX, out float dY);

                    return Graph2DUtilities.HORIZONTAL_COST * (dX + dY);
                }

                public static float OctileDistance(int nodeAX, int nodeAY, int nodeBX, int nodeBY)
                {
                    Graph2DUtilities.DeltaDistance(nodeAX, nodeAY, nodeBX, nodeBY, out float dX, out float dY);

                    if (dX > dY) return Graph2DUtilities.HORIZONTAL_COST * (dX - dY) + DIAGONAL_COST * dY;
                    
                    return Graph2DUtilities.HORIZONTAL_COST * (dY - dX) + DIAGONAL_COST * dX;
                }

                public static float ChebyshevDistance(int nodeAX, int nodeAY, int nodeBX, int nodeBY)
                {
                    Graph2DUtilities.DeltaDistance(nodeAX, nodeAY, nodeBX, nodeBY, out float dX, out float dY);

                    if (dX > dY) return Graph2DUtilities.HORIZONTAL_COST * (dX - dY) + Graph2DUtilities.HORIZONTAL_COST * dY;

                    return Graph2DUtilities.HORIZONTAL_COST * (dY - dX) + Graph2DUtilities.HORIZONTAL_COST * dX;
                }
            }
        }
    }
}

