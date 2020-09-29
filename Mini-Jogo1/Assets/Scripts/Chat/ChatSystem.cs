using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatSystem : MonoBehaviour
{
    [SerializeField] private GameObject chatPanel;
    [SerializeField] private GameObject bubblePrefab;
    [SerializeField] private List<GameObject> messageList = new List<GameObject>();
    [SerializeField] private int maxMessages = 20;
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerTextInput.OnTextInput += DisplayText;
    }

    // Update is called once per frame
    void Update()
    {
        //Removes the first message when it reaches the maximum
        if (messageList.Count > maxMessages)
        {
            Destroy(messageList[0]);
            messageList.Remove(messageList[0]);
        }
    }

    private void DisplayText(string text)
    {
        //Implement text instantiation (with bubble background)
        GameObject message = Instantiate(bubblePrefab, chatPanel.transform);
        message.GetComponent<TMP_Text>().text = text;
        messageList.Add(message);
    }
}
