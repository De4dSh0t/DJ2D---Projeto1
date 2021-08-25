using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class Clock : MonoBehaviour
    {
        private TMP_Text clockText;
        
        void Start()
        {
            clockText = GetComponent<TMP_Text>();
        }
        
        void Update()
        {
            UpdateClock();
        }
        
        private void UpdateClock()
        {
            // Updates text to match desktop time 
            clockText.text = DateTime.Now.ToShortTimeString();
        }
    }
}