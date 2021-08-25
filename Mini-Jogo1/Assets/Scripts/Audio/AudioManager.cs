using Core;
using UI.Menu;
using UnityEngine;

namespace Audio
{
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
        
            // Create soundObj
            GameObject soundObj = new GameObject {name = "SoundObj"};
        
            // Add audioSource
            soundObj.AddComponent<AudioSource>().PlayOneShot(GetSound(sound)); //Adds audio source and plays sound
        }
    
        public AudioSource PlayMusic(SoundName sound)
        {
            // Create musicObj
            GameObject musicObj = new GameObject {name = "MusicObj"};
        
            // Add audioSource
            AudioSource source = musicObj.AddComponent<AudioSource>();
            source.loop = true;
            source.PlayOneShot(GetSound(sound));
        
            // Check if music settings has music toggled off (Pause)
            if (!soundRef.settings.music) source.Pause();
        
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
            if (source == null) return;
            if (source.isPlaying) source.Pause();
            else source.UnPause();
        }
    
        private void UpdateVolume()
        {
            AudioListener.volume = soundRef.settings.volume;
        }
    }
}