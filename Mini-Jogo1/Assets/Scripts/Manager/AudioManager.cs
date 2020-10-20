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
        Glitch
    }
    
    void Awake()
    {
        soundRef = FindObjectOfType<SoundReferences>();
        sounds = soundRef.sounds;

        MainMenu.OnVolumeUpdate += UpdateVolume;
        PauseMenu.OnVolumeUpdate += UpdateVolume;
    }

    public void PlaySound(SoundName sound)
    {
        if (!soundRef.settings.sfx) return;
        GameObject soundObj = new GameObject();
        soundObj.AddComponent<AudioSource>().PlayOneShot(GetSound(sound)); //Adds audio source and plays sound
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

    private void UpdateVolume()
    {
        AudioListener.volume = soundRef.settings.volume;
    }
}
