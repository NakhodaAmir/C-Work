using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MirJan.Unity.Managers;
public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            AudioManager.Instance.Restart(MirJan.Unity.Managers.AudioTypes.INVESTIGATIONMUSIC, 0, true);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            AudioManager.Instance.Loop(MirJan.Unity.Managers.AudioTypes.TRUMPET);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            AudioManager.Instance.Loop(MirJan.Unity.Managers.AudioTypes.TRUMPET, 0, true);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            AudioManager.Instance.Play(MirJan.Unity.Managers.AudioTypes.ENCHANTINGMUSIC, 0, true);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            AudioManager.Instance.Play(MirJan.Unity.Managers.AudioTypes.INVESTIGATIONMUSIC, 0, true);
        }
        //////////////////////////////////////////////
        if (Input.GetKeyDown(KeyCode.A))
        {
            AudioManager.Instance.Stop(MirJan.Unity.Managers.AudioTypes.SFX1, 2);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            AudioManager.Instance.Stop(MirJan.Unity.Managers.AudioTypes.SFX2);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            AudioManager.Instance.Stop(MirJan.Unity.Managers.AudioTypes.TRUMPET);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            AudioManager.Instance.Stop(MirJan.Unity.Managers.AudioTypes.ENCHANTINGMUSIC);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            AudioManager.Instance.Stop(MirJan.Unity.Managers.AudioTypes.INVESTIGATIONMUSIC);
        }
        ///////////////////////////////////////
        if (Input.GetKeyDown(KeyCode.Z))
        {
            AudioManager.Instance.Restart(MirJan.Unity.Managers.AudioTypes.SFX1);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            AudioManager.Instance.Restart(MirJan.Unity.Managers.AudioTypes.SFX2);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            AudioManager.Instance.Restart(MirJan.Unity.Managers.AudioTypes.TRUMPET);
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            AudioManager.Instance.Restart(MirJan.Unity.Managers.AudioTypes.ENCHANTINGMUSIC, 0, true);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            AudioManager.Instance.Restart(MirJan.Unity.Managers.AudioTypes.INVESTIGATIONMUSIC, 0, true);
        }
    }
}
