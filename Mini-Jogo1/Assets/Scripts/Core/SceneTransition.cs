using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class SceneTransition : MonoBehaviour
    {
        [SerializeField] private float time;

        void Update()
        {
            HandleTimer();
        }
        
        private void HandleTimer()
        {
            time -= Time.deltaTime;
            
            if (time <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}