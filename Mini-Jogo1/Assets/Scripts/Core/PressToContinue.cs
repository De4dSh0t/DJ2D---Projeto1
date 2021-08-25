using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class PressToContinue : MonoBehaviour
    {
        [Header("Animation Settings")]
        [SerializeField] private float timeToActivate;
        [SerializeField] private float fadeRate;
        private bool isVisible;
        private TMP_Text text;
        
        void Start()
        {
            text = GetComponent<TMP_Text>();
        }
        
        void Update()
        {
            HandleAnimation();
        }
        
        private void HandleAnimation()
        {
            if (!(Time.time >= timeToActivate)) return;
            
            if (isVisible)
            {
                text.alpha -= fadeRate * Time.deltaTime;
                    
                if (text.alpha <= 0)
                {
                    isVisible = false;
                }
            }
            else
            {
                text.alpha += fadeRate * Time.deltaTime;
                    
                if (text.alpha >= 1)
                {
                    isVisible = true;
                }
            }
                
            HandleInput();
        }
        
        private void HandleInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}