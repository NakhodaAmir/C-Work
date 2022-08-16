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
            using MirJan.Unity.Helpers;

            public class AudioManager : Manager<AudioManager>
            {
                [SerializeField]
                AudioTrack[] tracks;

                readonly Dictionary<AudioTypes, Pair<AudioObject, AudioTrack>> audioDictionary = new Dictionary<AudioTypes, Pair<AudioObject, AudioTrack>>();
                readonly Dictionary<AudioTypes, IEnumerator> jobDictionary = new Dictionary<AudioTypes, IEnumerator>();

                [HideInInspector]
                public string audioTypesTextInfo = "";

                [Serializable]
                private class AudioObject
                {
                    public string name;
                    public AudioClip clip;
                    [Range(0f, 1f)]
                    public float volume = 1;
                    [Range(-3, 3f)]
                    public float pitch = 1;

                    [HideInInspector]
                    public AudioTypes type;
                }

                [Serializable]
                private class AudioTrack
                {
                    public AudioObject[] audios;

                    [HideInInspector]
                    public AudioSource source;
                }

                private class AudioJob
                {
                    public AudioActionType action;
                    public AudioTypes type;
                    public bool fade;
                    public float delay;
                    public float fadeDuration;

                    public AudioJob(AudioActionType action, AudioTypes type, float delay, bool fade, float fadeDuration)
                    {
                        this.action = action;
                        this.type = type;
                        this.fade = fade;
                        this.delay = delay;
                        this.fadeDuration = fadeDuration;
                    }
                }

                private enum AudioActionType
                {
                    START,
                    STOP,
                    RESTART,
                    LOOP
                }

                protected override void Initialize()
                {
                    base.Initialize();

                    GenerateAudioDictionary();
                }

                protected override void Terminate()
                {
                    base.Terminate();

                    Dispose();
                }

                #region Public Methods
                public void Play(AudioTypes type, float delay = 0f, bool fade = false, float fadeDuration = 1)
                {
                    AddJob(new AudioJob(AudioActionType.START, type, delay, fade, fadeDuration));
                }

                public void Stop(AudioTypes type, float delay = 0f, bool fade = false, float fadeDuration = 1)
                {
                    AddJob(new AudioJob(AudioActionType.STOP, type, delay, fade, fadeDuration));
                }

                public void Restart(AudioTypes type, float delay = 0f, bool fade = false, float fadeDuration = 1)
                {
                    AddJob(new AudioJob(AudioActionType.RESTART, type, delay, fade, fadeDuration));
                }

                public void Loop(AudioTypes type, float delay = 0f, bool fade = false, float fadeDuration = 1)
                {
                    AddJob(new AudioJob(AudioActionType.LOOP, type, delay, fade, fadeDuration));
                }
                #endregion

                #region Private Methods
                private void Dispose()
                {
                    foreach (KeyValuePair<AudioTypes, IEnumerator> keyValuePair in jobDictionary)
                    {
                        IEnumerator job = keyValuePair.Value;

                        StopCoroutine(job);
                    }
                }

                private void GenerateAudioDictionary()
                {
                    if (tracks == null || tracks.Length == 0) return;

                    foreach (AudioTrack track in tracks)
                    {
                        if(track.audios == null || track.audios.Length == 0) continue;

                        track.source = AddComponent<AudioSource>();
                        track.source.playOnAwake = false;

                        foreach (AudioObject audio in track.audios)
                        {
                            if (audio.type != AudioTypes.NONE && !audioDictionary.ContainsKey(audio.type))
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

                private void RemoveConflictingJobs(AudioTypes type)
                {
                    if (jobDictionary.ContainsKey(type)) RemoveJob(type);

                    AudioTypes conflictAudio = AudioTypes.NONE;

                    AudioTrack audioTrackNeeded = audioDictionary[type].GetSecondValue;

                    foreach (KeyValuePair<AudioTypes, IEnumerator> keyValuePair in jobDictionary)
                    {
                        AudioTrack audioTrackInUse = audioDictionary[keyValuePair.Key].GetSecondValue;

                        if (audioTrackNeeded.source == audioTrackInUse.source)
                        {
                            conflictAudio = keyValuePair.Key;
                            break;
                        }
                    }

                    if (conflictAudio != AudioTypes.NONE)
                    {
                        RemoveJob(conflictAudio);
                    }
                }

                private void RemoveJob(AudioTypes type)
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
                    yield return CoroutineHelper.WaitForSeconds(job.delay);

                    Pair<AudioObject, AudioTrack> pair = audioDictionary[job.type];

                    AudioObject audioObject = pair.GetFirstValue;

                    AudioTrack track = pair.GetSecondValue;

                    track.source.clip = audioObject.clip;
                    track.source.volume = audioObject.volume;
                    track.source.pitch = audioObject.pitch;
                    track.source.loop = false;

                    switch (job.action)
                    {
                        case AudioActionType.START:
                            track.source.Play();
                            break;

                        case AudioActionType.STOP:
                            if (!job.fade)
                            {
                                track.source.Stop();
                            }
                            break;

                        case AudioActionType.RESTART:
                            if (!job.fade)
                            {
                                track.source.Stop();
                                track.source.Play();
                            }
                            break;

                        case AudioActionType.LOOP:
                            if (!job.fade)
                            {
                                track.source.loop = true;
                                track.source.Play();
                            }
                            break;
                    }

                    if (job.fade)
                    {
                        int loopCount = job.action == AudioActionType.START || job.action == AudioActionType.STOP ? 1 : 2;

                        for (int i = 0; i < loopCount; i++)
                        {
                            if (track.source.isPlaying)
                            {
                                float initialVolume = job.action == AudioActionType.START ? 0 : job.action == AudioActionType.STOP ? 1 : 1 - i;
                                float targetVolume = initialVolume == 0 ? audioObject.volume : 0;
                                float fadeDuration = job.fadeDuration;
                                float timer = 0;

                                while (timer <= fadeDuration)
                                {
                                    track.source.volume = Mathf.Lerp(initialVolume, targetVolume, timer / fadeDuration);

                                    timer += Time.deltaTime;

                                    yield return null;
                                }

                                if (job.action == AudioActionType.STOP || ((job.action == AudioActionType.RESTART || job.action == AudioActionType.LOOP) && i == 0))
                                {
                                    track.source.Stop();
                                }
                            }                            

                            if ((job.action == AudioActionType.RESTART || job.action == AudioActionType.LOOP) && i == 0)
                            {
                                track.source.Play();
                            }

                            if(job.action == AudioActionType.LOOP && i == 1)
                            {
                                yield return CoroutineHelper.WaitForSeconds(track.source.clip.length - job.fadeDuration * 2);

                                i = -1;
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

                    string path = AssetDatabase.GetAssetPath(MonoScript.FromScriptableObject(this));
                    path = path.Replace("/AudioManager.cs", "/AUDIOTYPES.cs");

                    if (tracks == null || tracks.Length == 0)
                    {
                        GenerateEnum(audioObjects, 9, path, out audioTypesTextInfo);
                        return;
                    }

                    foreach (AudioTrack track in tracks)
                    {
                        foreach (AudioObject audio in track.audios)
                        {
                            if (audio.name == "")
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

                            audio.name = audio.name.ToUpper();
                        }
                    }

                    GenerateEnum(audioObjects, 9, path, out audioTypesTextInfo);

                    foreach (AudioTrack track in tracks)
                    {
                        foreach (AudioObject audio in track.audios)
                        {
                            foreach(AudioTypes audioType in Enum.GetValues(typeof(AudioTypes)))
                            {
                                if (audio.name == audioType.ToString())
                                {
                                    audio.type = audioType;
                                }
                            }
                        }
                    }
                }
                #endregion
            }
        }
    }
}