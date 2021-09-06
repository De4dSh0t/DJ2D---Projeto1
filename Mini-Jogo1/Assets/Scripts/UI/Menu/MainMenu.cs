using System;
using Audio;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Menu
{
    public class MainMenu : MonoBehaviour
    {
        [Header("UI Settings")]
        [SerializeField] private GameObject main;
        [SerializeField] private GameObject start;
        [SerializeField] private GameObject options;
        [SerializeField] private Button startButton;
        [SerializeField] private Button startBack;
        [SerializeField] private Button optionsButton;
        [SerializeField] private Button optionsBack;
        [SerializeField] private Button quitButton;
        
        [Header("Options Menu Settings")]
        [SerializeField] private Toggle musicToggle;
        [SerializeField] private Toggle sfxToggle;
        [SerializeField] private Slider volumeSlider;
        [SerializeField] private SoundSettings settings;
        
        // Audio Settings
        private AudioSource audioSource;
        
        // Events
        public static event Action<AudioSource> OnMusicToggle;
        public static event Action OnVolumeUpdate;
        
        void Start()
        {
            startButton.onClick.AddListener(OpenStart);
            startBack.onClick.AddListener(OpenMain);
            optionsButton.onClick.AddListener(OpenOptions);
            optionsBack.onClick.AddListener(OpenMain);
            quitButton.onClick.AddListener(CloseGame);
            
            //Options Menu (Sound Settings)
            musicToggle.onValueChanged.AddListener(UpdateMusic);
            sfxToggle.onValueChanged.AddListener(UpdateSFX);
            volumeSlider.onValueChanged.AddListener(UpdateVolume);
            
            //Set current sound settings values
            musicToggle.isOn = settings.music;
            sfxToggle.isOn = settings.sfx;
            volumeSlider.value = settings.volume;
            
            //Play MainMenu Theme music (https://www.youtube.com/watch?v=k_oMAGlWKTs)
            audioSource = AudioManager.Instance.PlayMusic(AudioManager.SoundName.MainMenu);
        }
        
        private void OpenMain()
        {
            main.SetActive(true);
            start.SetActive(false);
            options.SetActive(false);
        }
        
        private void OpenStart()
        {
            main.SetActive(false);
            start.SetActive(true);
            options.SetActive(false);
        }
        
        private void OpenOptions()
        {
            main.SetActive(false);
            start.SetActive(false);
            options.SetActive(true);
        }
        
        private void UpdateMusic(bool state)
        {
            OnMusicToggle?.Invoke(audioSource);
            settings.music = state;
        }
        
        private void UpdateSFX(bool state)
        {
            settings.sfx = state;
        }
        
        private void UpdateVolume(float volume)
        {
            settings.volume = volume;
            OnVolumeUpdate?.Invoke();
        }
        
        private void CloseGame()
        {
            Application.Quit();
        }
    }
}