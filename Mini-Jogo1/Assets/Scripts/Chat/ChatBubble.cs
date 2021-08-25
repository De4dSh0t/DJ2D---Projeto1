using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Chat
{
    public class ChatBubble : MonoBehaviour
    {
        [Header("UI Settings")]
        [SerializeField] private Image background;
        [SerializeField] private TMP_Text message;
        private TMP_Text dummyText;
        
        void Awake()
        {
            dummyText = gameObject.GetComponent<TMP_Text>();
        }
        
        /// <summary>
        /// Chat bubble setup
        /// </summary>
        /// <param name="text"></param>
        public void Setup(string text)
        {
            //Dummy Text (Used to space all bubbles depending on text height)
            dummyText.text = text;
            
            //Message
            message.text = text;
            
            //Background
            message.ForceMeshUpdate(); //Assures that the "GetRenderedValues" allways gets the latest values
            Vector2 messageSize = message.GetRenderedValues(false);
            Vector2 padding = new Vector2(10, 10);
            background.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,messageSize.x + padding.x); //Resizes the text depending on message size (X)
            background.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,messageSize.y + padding.y); //Resizes the text depending on message size (Y)
        }
    }
}