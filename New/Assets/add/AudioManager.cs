using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource audioSourcePrefab;

    private Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();
    private List<AudioSource> activeAudioSources = new List<AudioSource>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

   
    public void RegisterSound(string name, AudioClip clip)
    {
        if (!audioClips.ContainsKey(name))
        {
            audioClips[name] = clip;
        }
    }

    
    public void PlaySound(string name, float volume = 1f, bool loop = false)
    {
        if (!audioClips.ContainsKey(name))
        {
            Debug.LogWarning($"[AudioManager] «вук {name} не найден!");
            return;
        }

        AudioSource source = Instantiate(audioSourcePrefab, transform);
        source.clip = audioClips[name];
        source.volume = volume;
        source.loop = loop;
        source.Play();

        activeAudioSources.Add(source);

        if (!loop) Destroy(source.gameObject, source.clip.length);
    }

    
    public void StopAllSounds()
    {
        foreach (var source in activeAudioSources)
        {
            if (source != null)
                Destroy(source.gameObject);
        }
        activeAudioSources.Clear();
    }

    
    public void StopSound(string name)
    {
        foreach (var source in activeAudioSources)
        {
            if (source != null && source.clip.name == name)
            {
                Destroy(source.gameObject);
                activeAudioSources.Remove(source);
                break;
            }
        }
    }
}
