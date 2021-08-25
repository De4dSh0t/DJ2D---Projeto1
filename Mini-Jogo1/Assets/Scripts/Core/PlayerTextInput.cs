using System;
using TMPro;
using UnityEngine;

namespace Core
{
    public class PlayerTextInput : MonoBehaviour
    {
        [Header("UI Settings")]
        [SerializeField] private TMP_InputField textInput;
        
        // Event
        public static Action<string> OnTextInput;
        
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
            OnTextInput?.Invoke(textInput.text);
        }
        
        public void ClearText()
        {
            textInput.text = "";
            textInput.ActivateInputField();
        }
    }
}