using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private List<Sound> sounds;

    private Dictionary<string, Sound> _soundsDictionary;

    
    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("Multiple AudioManager instances");
        }
        
        _soundsDictionary = new Dictionary<string, Sound>();
        
        foreach (var sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
            var soundName = sound.name;
            if (soundName== "")
            {
                soundName = sound.clip.name;
            }
            _soundsDictionary[sound.name] = sound;
            sound.source.outputAudioMixerGroup = sound.audioMixerGroup;
        }
    }

    public void Play(string soundName)
    {
        Play(soundName, 1f);
    }
    
    /// <param name="volume">0 1 range</param>
    public void Play(string soundName, float volume)
    {
        if (_soundsDictionary.ContainsKey(soundName))
        {
            if (!_soundsDictionary[soundName].source.isPlaying)
            {
                _soundsDictionary[soundName].source.volume = volume * _soundsDictionary[soundName].volume;
                _soundsDictionary[soundName].source.Play();
            }
        }
        else
        {
            Debug.Log($"No sound with name {soundName}");
        }
    }
    
    public void PlayWithOverlay(string soundName)
    {
        PlayWithOverlay(soundName, 1f);
    }
    
    public void PlayWithOverlay(string soundName, float volume)
    {
        if (_soundsDictionary.ContainsKey(soundName))
        {
            _soundsDictionary[soundName].source.volume = volume * _soundsDictionary[soundName].volume;
            _soundsDictionary[soundName].source.Play();
        }
        else
        {
            Debug.Log($"No sound with name {soundName}");
        }
    }
    
    public void Pause(string soundName)
    {
        if (_soundsDictionary.ContainsKey(soundName))
        {
            _soundsDictionary[soundName].source.Pause();
        }
        else
        {
            Debug.Log($"No sound with name {soundName}");
        }
    }
    
    public void UnPause(string soundName)
    {
        if (_soundsDictionary.ContainsKey(soundName))
        {
            _soundsDictionary[soundName].source.UnPause();
        }
        else
        {
            Debug.Log($"No sound with name {soundName}");
        }
    }

    public void Stop(string soundName)
    {
        if (_soundsDictionary.ContainsKey(soundName))
        {
            _soundsDictionary[soundName].source.Stop();
        }
        else
        {
            Debug.Log($"No sound with name {soundName}");
        }
    }
}
