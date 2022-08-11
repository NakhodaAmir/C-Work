namespace MirJan
{
    namespace Unity
    {
        namespace Managers
        {
            using System.Collections;
            using System.Collections.Generic;
            using UnityEngine;
            using Helpers;
            using System;
            using UnityEditor;
            using System.IO;

            [CreateAssetMenu()]
            public class AudioManager : Manager<AudioManager>
            {
                [SerializeField]
                AudioTrack[] tracks;

                Hashtable audioTable = new Hashtable();
                Hashtable jobTable = new Hashtable();

                [HideInInspector]
                public string audioTypesTextInfo = "";

                [Serializable]
                private class AudioObject
                {
                    public string name;
                    public AudioClip clip;
                }

                [Serializable]
                private class AudioTrack
                {
                    public AudioSource source;
                    public AudioObject[] audios;        
                }

                private class AudioJob
                {
                    public AUDIOACTIONTYPES action;
                    public AUDIOTYPES type;
                }

                private enum AUDIOACTIONTYPES
                {
                    START,
                    STOP,
                    RESTART
                }

                protected override void Initialize()
                {
                    base.Initialize();

                }

                #region Editor Methods
                public void GenerateAudioTypes()
                {
                    List<string> audioObjects = new List<string>();

                    foreach (AudioTrack track in tracks)
                    {
                        foreach (AudioObject audio in track.audios)
                        {
                            if(audio.name == default)
                            {
                                Debug.LogError("AudioObject name is required!");
                                return;
                            }

                            if (audioObjects.Contains(audio.name))
                            {
                                Debug.LogError("AudioObject name must be unique!");
                                return;
                            }

                            audioObjects.Add(audio.name);
                        }
                    }

                    string path = AssetDatabase.GetAssetPath(MonoScript.FromScriptableObject(this));
                    path = path.Replace("/AudioManager.cs", "/AUDIOTYPES.cs");

                    GenerateEnum(audioObjects, 8, path, out audioTypesTextInfo);
                }
                #endregion
            }
        }
    }
}

