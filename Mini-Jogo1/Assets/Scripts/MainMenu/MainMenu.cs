using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject main;
    [SerializeField] private GameObject start;
    [SerializeField] private GameObject options;
    [SerializeField] private Button startButton;
    [SerializeField] private Button startBack;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button optionsBack;

    [Header("Options Menu Settings")]
    [SerializeField] private Toggle musicToggle;
    [SerializeField] private Toggle sfxToggle;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private SoundSettings settings;

    public static Action OnVolumeUpdate;
    
    void Start()
    {
        startButton.onClick.AddListener(OpenStart);
        startBack.onClick.AddListener(OpenMain);
        optionsButton.onClick.AddListener(OpenOptions);
        optionsBack.onClick.AddListener(OpenMain);
        
        //Options Menu (Sound Settings)
        musicToggle.onValueChanged.AddListener(UpdateMusic);
        sfxToggle.onValueChanged.AddListener(UpdateSFX);
        volumeSlider.onValueChanged.AddListener(UpdateVolume);

        //Set current sound settings values
        musicToggle.isOn = settings.music;
        sfxToggle.isOn = settings.sfx;
        volumeSlider.value = settings.volume;
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
        settings.music = state;
    }
    
    private void UpdateSFX(bool state)
    {
        settings.sfx = state;
    }

    private void UpdateVolume(float volume)
    {
        settings.volume = volume;
        if (OnVolumeUpdate != null) OnVolumeUpdate();
    }
}
