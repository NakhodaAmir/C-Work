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
                #region Properties
                public int LayerIndex { get { return layerIndex; } }

                public int Value { get { return 1 << layerIndex; } }
                #endregion

                #region Variables
                [SerializeField]
                private int layerIndex = 0;
                #endregion


                #region Public Methods
                public void Set(int layerIndex)
                {
                    if(layerIndex > 0 && layerIndex < 32)
                    {
                        this.layerIndex = layerIndex;
                    }
                }
                #endregion
            }
        }
    }
}

