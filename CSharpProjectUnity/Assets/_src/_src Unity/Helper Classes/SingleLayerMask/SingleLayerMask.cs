namespace MirJan
{
    namespace Unity
    {
        namespace Helpers
        {
            using UnityEngine;

            [System.Serializable]
            public class SingleLayerMask
            {
                [SerializeField]
                private int layerIndex = 0;
                public int LayerIndex { get { return layerIndex; } }

                public void Set(int layerIndex)
                {
                    if(layerIndex > 0 && layerIndex < 32)
                    {
                        this.layerIndex = layerIndex;
                    }
                }

                public int Value { get { return 1 << layerIndex; } }
            }
        }
    }
}

