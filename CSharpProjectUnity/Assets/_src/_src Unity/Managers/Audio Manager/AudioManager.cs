namespace MirJan
{
    namespace Unity
    {
        namespace Managers
        {
            using System;
            using System.Collections;
            using System.Collections.Generic;
            using UnityEditor;
            using UnityEngine;
            using MirJan.Helpers;

            public class AudioManager : Manager<AudioManager>
            {
                [SerializeField]
                AudioTrack[] tracks;

                readonly Dictionary<AUDIOTYPES, Pair<AudioObject, AudioTrack>> audioDictionary = new Dictionary<AUDIOTYPES, Pair<AudioObject, AudioTrack>>();
                readonly Dictionary<AUDIOTYPES, IEnumerator> jobDictionary = new Dictionary<AUDIOTYPES, IEnumerator>();

                [HideInInspector]
                public string audioTypesTextInfo = "";

                [Serializable]
                private class AudioObject
                {
                    public string name;
                    public AUDIOTYPES type;
                    public AudioClip clip;
                }

                [Serializable]
                private class AudioTrack
                {
                    [HideInInspector]
                    public AudioSource source;

                    public AudioObject[] audios;
                }

                private class AudioJob
                {
                    public AUDIOACTIONTYPES action;
                    public AUDIOTYPES type;
                    public bool fade;
                    public float delay;
                    public float fadeDuration;

                    public AudioJob(AUDIOACTIONTYPES action, AUDIOTYPES type, float delay, bool fade, float fadeDuration)
                    {
                        this.action = action;
                        this.type = type;
                        this.fade = fade;
                        this.delay = delay;
                        this.fadeDuration = fadeDuration;
                    }
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

                    GenerateAudioDictionary();
                }

                #region Public Methods
                public void Play(AUDIOTYPES type, float delay = 0f, bool fade = false, float fadeDuration = 1)
                {
                    AddJob(new AudioJob(AUDIOACTIONTYPES.START, type, delay, fade, fadeDuration));
                }

                public void Stop(AUDIOTYPES type, float delay = 0f, bool fade = false, float fadeDuration = 1)
                {
                    AddJob(new AudioJob(AUDIOACTIONTYPES.STOP, type, delay, fade, fadeDuration));
                }

                public void Restart(AUDIOTYPES type, float delay = 0f, bool fade = false, float fadeDuration = 1)
                {
                    AddJob(new AudioJob(AUDIOACTIONTYPES.RESTART, type, delay, fade, fadeDuration));
                }
                #endregion

                #region Private Methods
                private void Dispose()
                {
                    foreach (KeyValuePair<AUDIOTYPES, IEnumerator> keyValuePair in jobDictionary)
                    {
                        IEnumerator job = keyValuePair.Value;

                        StopCoroutine(job);
                    }
                }

                private void GenerateAudioDictionary()
                {
                    if (tracks.Length == 0) return;

                    foreach (AudioTrack track in tracks)
                    {
                        track.source = AddComponent<AudioSource>();

                        foreach (AudioObject audio in track.audios)
                        {
                            if (audio.type != AUDIOTYPES.NONE && !audioDictionary.ContainsKey(audio.type))
                            {
                                audioDictionary.Add(audio.type, new Pair<AudioObject, AudioTrack>(audio, track));
                            }
                        }
                    }
                }

                private void AddJob(AudioJob job)
                {
                    RemoveConflictingJobs(job.type);

                    IEnumerator jobRunner = RunAudioJob(job);

                    jobDictionary.Add(job.type, jobRunner);

                    StartCoroutine(jobRunner);
                }

                private void RemoveConflictingJobs(AUDIOTYPES type)
                {
                    if (jobDictionary.ContainsKey(type)) RemoveJob(type);

                    AUDIOTYPES conflictAudio = AUDIOTYPES.NONE;

                    foreach (KeyValuePair<AUDIOTYPES, IEnumerator> keyValuePair in jobDictionary)
                    {
                        AUDIOTYPES audioType = keyValuePair.Key;
                        AudioTrack audioTrackInUse = (AudioTrack)jobDictionary[audioType];
                        AudioTrack audioTrackNeeded = (AudioTrack)jobDictionary[type];

                        if (audioTrackNeeded.source = audioTrackInUse.source)
                        {
                            conflictAudio = audioType;
                        }
                    }

                    if (conflictAudio != AUDIOTYPES.NONE)
                    {
                        RemoveJob(conflictAudio);
                    }
                }

                private void RemoveJob(AUDIOTYPES type)
                {
                    if (jobDictionary.ContainsKey(type))
                    {
                        IEnumerator runningJob = jobDictionary[type];

                        StopCoroutine(runningJob);

                        jobDictionary.Remove(type);
                    }
                }

                private IEnumerator RunAudioJob(AudioJob job)
                {
                    yield return new WaitForSeconds(job.delay);

                    Pair<AudioObject, AudioTrack> pair = audioDictionary[job.type];

                    AudioObject audioObject = pair.GetFirstValue;

                    AudioTrack track = pair.GetSecondValue;

                    track.source.clip = audioObject.clip;

                    switch (job.action)
                    {
                        case AUDIOACTIONTYPES.START:
                            track.source.Play();
                            break;

                        case AUDIOACTIONTYPES.STOP:
                            if (!job.fade)
                            {
                                track.source.Stop();
                            }
                            break;

                        case AUDIOACTIONTYPES.RESTART:
                            if (!job.fade)
                            {
                                track.source.Stop();
                                track.source.Play();
                            }
                            break;
                    }

                    if (job.fade)
                    {
                        int loopCount = job.action == AUDIOACTIONTYPES.START || job.action == AUDIOACTIONTYPES.STOP ? 1 : 2;

                        for (int i = 0; i < loopCount; i++)
                        {
                            float initialVolume = job.action == AUDIOACTIONTYPES.START ? 0 : job.action == AUDIOACTIONTYPES.STOP ? 1 : 1 - i;
                            float targetVolume = initialVolume == 0 ? 1 : 0;
                            float fadeDuration = job.fadeDuration;
                            float timer = 0;

                            while (timer <= fadeDuration)
                            {
                                track.source.volume = Mathf.Lerp(initialVolume, targetVolume, timer / fadeDuration);

                                timer += Time.deltaTime;

                                yield return null;
                            }

                            if (job.action == AUDIOACTIONTYPES.STOP || (job.action == AUDIOACTIONTYPES.RESTART && i == 0))
                            {
                                track.source.Stop();
                            }

                            if (job.action == AUDIOACTIONTYPES.RESTART && i == 1)
                            {
                                track.source.Play();
                            }
                        } 
                    }

                    jobDictionary.Remove(job.type);

                    yield return null;
                }
                #endregion

                #region Editor Methods
                public void GenerateAudioTypes()
                {
                    List<string> audioObjects = new List<string>();

                    if (tracks == null)
                    {
                        Debug.LogError("No tracks!");
                        return;
                    }

                    foreach (AudioTrack track in tracks)
                    {
                        foreach (AudioObject audio in track.audios)
                        {
                            if (audio.name == default)
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

                    GenerateEnum(audioObjects, 9, path, out audioTypesTextInfo);
                }
                #endregion
            }
        }
    }
}