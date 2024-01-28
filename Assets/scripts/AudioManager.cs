using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    
    public Sound[] musicSounds, sfxSounds, ambSounds;
    public AudioSource musicSource, sfxSource, ambSource;

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

    private void Start()
    {
        PlayMusic("Music_Gameplay");
        PlayAmb("Crowd");
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound not Found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.volume = s.volume;  
            musicSource.pitch = s.pitch;    
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Sound not Found");
        }
        else
        {
            sfxSource.volume = s.volume;
            sfxSource.pitch = s.pitch;
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void PlayAmb(string name)
    {
        Sound s = Array.Find(ambSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Sound not Found");
        }
        else
        {
            ambSource.clip = s.clip;
            ambSource.volume = s.volume;
            ambSource.pitch = s.pitch;
            ambSource.Play();
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}
