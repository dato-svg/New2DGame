using System.Collections.Generic;
using UnityEngine;

namespace add
{
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
                return;
            }

            Debug.Log($"[AudioManager] Зарегистрированные звуки: {string.Join(", ", audioClips.Keys)}");
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
                Debug.LogWarning($"[AudioManager] ���� {name} �� ������!");
                return;
            }
            if (audioSourcePrefab == null)
            {
                Debug.LogError("[AudioManager] audioSourcePrefab не задан!");
                return;
            }

            GameObject source = Instantiate(audioSourcePrefab).gameObject;
            source.GetComponent<AudioSource>().clip = audioClips[name];
            source.GetComponent<AudioSource>().volume = volume;
            source.GetComponent<AudioSource>().loop = loop;
            source.GetComponent<AudioSource>().Play();
            
            Debug.Log("Play Sound PPPPPPLEAAAASE    : " + name);
            activeAudioSources.Add(source.GetComponent<AudioSource>());

            if (!loop) Destroy(source.gameObject, source.GetComponent<AudioSource>().clip.length);
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
}
