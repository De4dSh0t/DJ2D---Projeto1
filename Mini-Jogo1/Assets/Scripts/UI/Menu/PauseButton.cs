using Audio;
using Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Menu
{
    public class PauseButton : MonoBehaviour
    {
        [Header("UI Settings")]
        [SerializeField] private Canvas pauseCanvas;
        private AudioSource audioSource;
        private Button pauseButton;
        
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
            // Pause Audio
            if (audioSource != null) audioSource.Pause();
            
            // Enable PauseMenu canvas
            pauseCanvas.gameObject.SetActive(true);
            GameManager.Instance.inGame = false;
            Time.timeScale = 0;
        }
    }
}