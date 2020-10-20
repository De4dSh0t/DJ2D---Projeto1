using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    [SerializeField] private Canvas pauseCanvas;
    private Button pauseButton;
    private AudioSource audioSource;

    void Start()
    {
        pauseButton = GetComponent<Button>();
        pauseButton.onClick.AddListener(OpenPauseMenu);

        //Play game theme music (https://www.audiolibrary.com.co/nihilore/bush-week/)
        audioSource = AudioManager.Instance.PlayMusic(AudioManager.SoundName.Game);
        pauseCanvas.GetComponent<PauseMenu>().audioSource = audioSource;
    }

    private void OpenPauseMenu()
    {
        audioSource.Pause();
        pauseCanvas.gameObject.SetActive(true);
        GameManager.Instance.inGame = false;
        Time.timeScale = 0;
    }
}
