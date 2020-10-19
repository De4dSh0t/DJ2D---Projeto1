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
    
    void Start()
    {
        startButton.onClick.AddListener(OpenStart);
        startBack.onClick.AddListener(OpenMain);
        optionsButton.onClick.AddListener(OpenOptions);
        optionsBack.onClick.AddListener(OpenMain);
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
}
