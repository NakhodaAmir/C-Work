namespace MirJan
{
    namespace Helpers
    {
        using System;

        public static class CMath
        {
            public const double Sqrt2 = 1.41421356237309504880168872420969807856967187537694807317667973799073247846210703885038753432764157273501384623091229702492483605585073721264412149709993583141322266592750559275579995050115278206057147010955997160597027453459686201472851;
            public const double Sqrt3 = 1.73205080756887729352744634150587236694280525381038062805580697945193301690880003708114618675724857567562614141540670302996994509499895247881165551209437364852809323190230558206797482010108467492326501531234326690332288665067225466892183;

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
        }
    }
}
