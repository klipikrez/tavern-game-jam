using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [System.Serializable]
    public class AudioAudi
    {
        public string name;
        public AudioSource source;
        public GameObject obj;
        public Coroutine coroutine;
        public AudioAudi(AudioSource source, GameObject obj, Coroutine coroutine, string name)
        {
            this.name = name;
            this.source = source;
            this.obj = obj;
            this.coroutine = coroutine;
        }
        public AudioAudi()
        {

        }
    }

    [System.NonSerialized]
    public Dictionary<string, AudioClip> audioDictionary = new Dictionary<string, AudioClip>();

    Coroutine voiceCorutine;
    AudioSource VoiceLineSource;
    [System.NonSerialized]
    public AudioSource musicSource;
    Coroutine switchMusicCorutine;
    //public Dictionary<string, AudioAudi> PlayingAudio = new Dictionary<string, AudioAudi>();

    /*[UDictionary.Split(50, 50)]
    public UDictionary2 PlayingAudio;
    [System.Serializable]
    public class UDictionary2 : UDictionary<System.Guid, AudioAudi> { }*/
    public Dictionary<System.Guid, AudioAudi> PlayingAudio = new Dictionary<System.Guid, AudioAudi>();
    public static AudioManager Instance { get; private set; }


    private void Awake()
    {
        Instance = this;
        Object[] allAudios = Resources.LoadAll("Audio", typeof(AudioClip));

        foreach (AudioClip clip in allAudios)
        {
            audioDictionary.Add(clip.name, clip);
        }
    }
    /********************************************************/
    /*mozes da dodas da mozes da mu prosleds vise stringova,*/
    /*i on da odaberte jedan random string iz datih ponuda  */
    /********************************************************/
    private void Start()
    {
        transform.position = Vector3.zero;

    }






    public System.Guid PlayAudioClip(string audioClipName, float volume = 1, int priority = 128)
    {
        System.Guid id = System.Guid.NewGuid();
        PlayingAudio.Add(id, new AudioAudi());
        PlayingAudio[id].name = audioClipName;
        PlayingAudio[id].coroutine = StartCoroutine(Play(audioDictionary[audioClipName], volume, priority, id));
        return id;
    }

    public void StopAudio(System.Guid name)
    {
        if (PlayingAudio.ContainsKey(name))
            foreach (KeyValuePair<System.Guid, AudioAudi> emmiter in PlayingAudio)
            {
                if (name == emmiter.Key)
                {
                    if (emmiter.Value.coroutine != null)
                    {
                        StopCoroutine(emmiter.Value.coroutine);
                    }
                    if (emmiter.Value.source != null)
                    {
                        Destroy(emmiter.Value.source);
                    }
                    if (emmiter.Value.obj != null)
                    {
                        Destroy(emmiter.Value.obj);
                    }
                    PlayingAudio.Remove(emmiter.Key);
                    break;
                }
            }
    }

    public System.Guid PlayAudioClipLooping(string audioClipName, float volume = 1)
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioDictionary[audioClipName];
        audioSource.volume = volume;
        audioSource.loop = true;
        audioSource.Play();
        System.Guid id = System.Guid.NewGuid();
        PlayingAudio.Add(id, new AudioAudi(audioSource, null, null, audioClipName));
        return id;
    }











    IEnumerator Play(AudioClip audio, float volume, int priority, System.Guid id)
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.clip = audio;
        audioSource.volume = volume;
        audioSource.priority = priority;
        PlayingAudio[id].source = audioSource;
        audioSource.Play();
        yield return new WaitForSeconds(audio.length);
        audioSource.Stop();
        Destroy(audioSource);
        PlayingAudio.Remove(id);
    }

    IEnumerator Play(AudioClip audio, float volume, int priority, AudioSource audioSource, System.Guid id)
    {


        audioSource.clip = audio;
        audioSource.volume = volume;
        audioSource.priority = priority;
        PlayingAudio[id].source = audioSource;
        audioSource.Play();
        yield return new WaitForSeconds(audio.length);
        audioSource.Stop();
        Destroy(audioSource);
        PlayingAudio.Remove(id);
    }


}
