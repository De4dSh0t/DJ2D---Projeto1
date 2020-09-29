using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatBubble : MonoBehaviour
{
    private SpriteRenderer background;
    private TMP_Text message;
    
    void Start()
    {
        background = transform.Find("Background").GetComponent<SpriteRenderer>();
        message = transform.Find("Message").GetComponent<TMP_Text>();
    }

    /// <summary>
    /// Chat bubble setup
    /// </summary>
    /// <param name="text"></param>
    public void Setup(string text)
    {
        //Message
        message.text = text;
        
        //Background
        message.ForceMeshUpdate(); //Assures that the "GetRenderedValues" allways gets the latest values
        Vector2 messageSize = message.GetRenderedValues(false);
        Vector2 offset = new Vector2(2, 2);
        background.size = messageSize + offset; //Resizes the text depending on message size
    }
}
