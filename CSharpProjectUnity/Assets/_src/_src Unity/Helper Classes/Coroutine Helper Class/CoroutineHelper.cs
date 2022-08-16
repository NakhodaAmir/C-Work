namespace MirJan
{
    namespace Unity
    {
        namespace Helpers
        {
            using System.Collections.Generic;
            using UnityEngine;

            public static class CoroutineHelper
            {
                class FloatComparer : IEqualityComparer<float>
                {
                    bool IEqualityComparer<float>.Equals(float x, float y)
                    {
                        return x == y;
                    }
                    int IEqualityComparer<float>.GetHashCode(float obj)
                    {
                        return obj.GetHashCode();
                    }
                }
                static readonly Dictionary<float, WaitForSeconds> _timeInterval = new Dictionary<float, WaitForSeconds>(100, new FloatComparer());

                static readonly WaitForEndOfFrame _endOfFrame = new WaitForEndOfFrame();
                public static WaitForEndOfFrame EndOfFrame
                {
                    get { return _endOfFrame; }
                }

                static readonly WaitForFixedUpdate _fixedUpdate = new WaitForFixedUpdate();
                public static WaitForFixedUpdate FixedUpdate
                {
                    get { return _fixedUpdate; }
                }

                public static WaitForSeconds WaitForSeconds(float seconds)
                {
                    if (!_timeInterval.TryGetValue(seconds, out WaitForSeconds wfs))
                        _timeInterval.Add(seconds, wfs = new WaitForSeconds(seconds));
                    return wfs;
                }
            }
        }
    }
}

