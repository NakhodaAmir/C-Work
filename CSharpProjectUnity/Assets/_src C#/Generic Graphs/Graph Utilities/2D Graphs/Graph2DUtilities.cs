namespace MirJan
{
    namespace GenericGraphs
    {
        namespace GraphUtilites
        {
            public static class Graph2DUtilities
            {
                public const int HORIZONTAL_COST = 10;

                public static float EuclideanDistance(int nodeAX, int nodeAY, int nodeBX, int nodeBY)
                {
                    DeltaDistance(nodeAX, nodeAY, nodeBX, nodeBY, out float dX, out float dY);

                    return (float)(HORIZONTAL_COST * System.Math.Sqrt(dX * dX + dY * dY));
                }

                public static void DeltaDistance(int nodeAX, int nodeAY, int nodeBX, int nodeBY, out float dX, out float dY)
                {
                    dX = System.Math.Abs(nodeAX - nodeBX);
                    dY = System.Math.Abs(nodeAY - nodeBY);
                }
            }
        }
    }
}
