using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Virus.Alphabetic_Order;

namespace Virus.Sort_Numbers
{
    public class ButtonBehaviour : MonoBehaviour
    {
        // UI Settings
        private TMP_Text txt;
        private Button button;
        private Color defaultColor;
        
        // Event
        public static Action<string> OnButtonPressed;
        
        private void Awake()
        {
            txt = GetComponentInChildren<TMP_Text>();
            button = GetComponent<Button>();
            defaultColor = button.image.color;
        }
        
        void Start()
        {
            SortBehaviour.OnPlayerMiss += UnlockButton;
            OrderBehaviour.OnPlayerMiss += UnlockButton;
            SortBehaviour.OnPlayerSuccess += Complete;
            OrderBehaviour.OnPlayerSuccess += Complete;
        }
        
        /// <summary>
        /// Sets the text of the button
        /// </summary>
        public void SetText(string s)
        {
            txt.text = s;
        }
        
        /// <summary>
        /// Locks the button when pressed
        /// </summary>
        public void LockButton()
        {
            if (button == null) return;
            button.interactable = false;
            button.image.color = new Color(0.08f, 0.27f, 0.48f);
            OnButtonPressed?.Invoke(txt.text);
        }
        
        /// <summary>
        /// Unlocks the button when player fails to sort the numbers
        /// </summary>
        private void UnlockButton()
        {
            if (button == null) return;
            button.interactable = true;
            button.image.color = defaultColor;
        }
        
        /// <summary>
        /// Removes all subscriptions (Actions)
        /// </summary>
        private void Complete()
        {
            SortBehaviour.OnPlayerMiss -= UnlockButton;
            OrderBehaviour.OnPlayerMiss -= UnlockButton;
            SortBehaviour.OnPlayerSuccess -= Complete;
            OrderBehaviour.OnPlayerSuccess -= Complete;
        }
    }
}