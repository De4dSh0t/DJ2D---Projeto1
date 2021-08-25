using UnityEngine;

namespace Core
{
    public class CameraSize : MonoBehaviour
    {
        [SerializeField] private Vector2 targetSize;
        private float screenRatio;
        private float targetRatio;
        
        void Update()
        {
            HandleSize();
        }
        
        private void HandleSize()
        {
            //Calculate Ratios
            screenRatio = (float)Screen.width / Screen.height;
            targetRatio = targetSize.x / targetSize.y;
            
            if (screenRatio >= targetRatio)
            {
                Camera.main.orthographicSize = targetSize.y / 2;
            }
            else
            {
                float sizeDiff = targetRatio / screenRatio;
                Camera.main.orthographicSize = targetSize.y / 2 * sizeDiff;
            }
        }
    }
}