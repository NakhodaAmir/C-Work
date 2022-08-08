namespace MirJan
{
    namespace Helpers
    {
        using System;

        public static class CMath
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
        }
    }
}
