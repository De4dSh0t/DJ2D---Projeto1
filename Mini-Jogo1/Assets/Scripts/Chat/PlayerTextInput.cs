using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerTextInput : MonoBehaviour
{
    [SerializeField] private TMP_InputField textInput;
    public List<string> messages;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && textInput.text != "")
        {
            SaveText();
            ClearText();
        }
    }

    private void SaveText()
    {
        messages.Add(textInput.text);
    }

    private void ClearText()
    {
        textInput.text = "";
    }
}
