using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    private SoundReferences soundRef;
    private Sound[] sounds;
    
    public enum SoundName
    {
        ChatNotification,
        Glitch,
        MainMenu,
        Game
    }
    
    void Awake()
    {
        soundRef = FindObjectOfType<SoundReferences>();
        sounds = soundRef.sounds;

        MainMenu.OnVolumeUpdate += UpdateVolume;
        PauseMenu.OnVolumeUpdate += UpdateVolume;
        MainMenu.OnMusicToggle += UpdateMusicState;
        PauseMenu.OnMusicToggle += UpdateMusicState;
    }

    public void PlaySound(SoundName sound)
    {
        if (!soundRef.settings.sfx) return;
        GameObject soundObj = new GameObject();
        soundObj.AddComponent<AudioSource>().PlayOneShot(GetSound(sound)); //Adds audio source and plays sound
    }

    public AudioSource PlayMusic(SoundName sound)
    {
        if (!soundRef.settings.music) return null;
        GameObject musicObj = new GameObject();
        AudioSource source = musicObj.AddComponent<AudioSource>();
        source.PlayOneShot(GetSound(sound));
        source.loop = true;
        return musicObj.GetComponent<AudioSource>();
    }

    private AudioClip GetSound(SoundName soundName)
    {
        foreach (var sound in sounds)
        {
            if (sound.soundName == soundName)
            {
                 return sound.clip;
            }
        }

        return null;
    }

    private void UpdateMusicState(AudioSource source)
    {
        if (source.isPlaying) source.Pause();
        else source.UnPause();
    }

    private void UpdateVolume()
    {
        AudioListener.volume = soundRef.settings.volume;
    }
}
