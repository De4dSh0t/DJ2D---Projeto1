using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatBubble : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private TMP_Text message;
    private TMP_Text dummyText;

    void Start()
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
        Vector2 offset = new Vector2(2, 2);
        //background.rectTransform.sizeDelta = messageSize + offset; //Resizes the text depending on message size
    }
}
