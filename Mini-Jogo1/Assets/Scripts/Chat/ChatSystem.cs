using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatSystem : MonoBehaviour
{
    [SerializeField] private GameObject chatPanel;
    [SerializeField] private GameObject playerBubble;
    [SerializeField] private GameObject friendBubble;
    [SerializeField] private List<GameObject> messageList = new List<GameObject>();
    [SerializeField] private int maxMessages = 20;

    void Start()
    {
        GameManager.Instance.OnSceneUnload += UnsubscribeAll;
        
        PlayerTextInput.OnTextInput += DisplayPlayerText;
        FriendBehaviour.OnFriendResponse += DisplayFriendText;
    }
    
    void Update()
    {
        //Removes the first message when it reaches the maximum
        if (messageList.Count > maxMessages)
        {
            Destroy(messageList[0]);
            messageList.Remove(messageList[0]);
        }
    }

    private void DisplayPlayerText(string text)
    {
        if (!GameManager.Instance.inGame) return;
        
        GameObject message = Instantiate(playerBubble, chatPanel.transform);
        message.GetComponent<ChatBubble>().Setup(text);
        messageList.Add(message);
    }

    private void DisplayFriendText(string text)
    {
        if (!GameManager.Instance.inGame) return;
        
        GameObject message = Instantiate(friendBubble, chatPanel.transform);
        message.GetComponent<ChatBubble>().Setup(text);
        messageList.Add(message);
    }

    private void UnsubscribeAll()
    {
        PlayerTextInput.OnTextInput -= DisplayPlayerText;
        FriendBehaviour.OnFriendResponse -= DisplayFriendText;
        GameManager.Instance.OnSceneUnload -= UnsubscribeAll;
    }
}
