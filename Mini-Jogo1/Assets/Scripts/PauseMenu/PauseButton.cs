using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    [SerializeField] private Canvas pauseCanvas;
    private Button pauseButton;

    void Start()
    {
        pauseButton = GetComponent<Button>();
        pauseButton.onClick.AddListener(OpenPauseMenu);
    }

    private void OpenPauseMenu()
    {
        pauseCanvas.gameObject.SetActive(true);
        GameManager.Instance.inGame = false;
        Time.timeScale = 0;
    }
}
