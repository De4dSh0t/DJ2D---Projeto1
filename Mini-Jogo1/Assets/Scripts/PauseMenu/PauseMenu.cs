using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject pause;
    [SerializeField] private GameObject options;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button back;
    [SerializeField] private Button quit;
    
    void Start()
    {
        resumeButton.onClick.AddListener(ClosePause);
        optionsButton.onClick.AddListener(OpenOptions);
        back.onClick.AddListener(OpenPause);
        quit.onClick.AddListener(LoadMainMenu);
    }
    
    private void ClosePause()
    {
        canvas.gameObject.SetActive(false);
        GameManager.Instance.inGame = true;
        Time.timeScale = 1;
    }

    private void OpenPause()
    {
        pause.SetActive(true);
        options.SetActive(false);
    }

    private void OpenOptions()
    {
        pause.SetActive(false);
        options.SetActive(true);
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
