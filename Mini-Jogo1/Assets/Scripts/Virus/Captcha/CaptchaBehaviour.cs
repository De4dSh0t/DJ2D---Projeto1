﻿using System;
using Core;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Virus.Captcha
{
    public class CaptchaBehaviour : MonoBehaviour
    {
        [Header("Captcha Settings")]
        [SerializeField] private int captchaLength;
        private readonly char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        private readonly int[] numbers = {1, 2, 3, 4, 5, 6, 7, 8, 9};
        private TMP_Text captchaText;
        private string captcha;
        
        // Event
        public static event Action OnPlayerSuccess;
        
        void Start()
        {
            captchaText = GetComponentInChildren<TMP_Text>();
            captcha = GenerateCaptcha();
            captchaText.text = captcha;
            
            PlayerTextInput.OnTextInput += Check;
        }
        
        /// <summary>
        /// Generates a captcha using letters and numbers
        /// </summary>
        /// <returns></returns>
        private string GenerateCaptcha()
        {
            string gCaptcha = "";
            
            for (int i = 0; i < captchaLength; i++)
            {
                int selectedArray = Random.Range(0, 2);
                
                switch (selectedArray)
                {
                    case 0:
                    {
                        int letterCase = Random.Range(0, 2);
                        int rIndex = Random.Range(0, alphabet.Length - 1);
                        
                        if (letterCase == 1) gCaptcha += alphabet[rIndex].ToString().ToLower(); // Lowercase
                        else gCaptcha += alphabet[rIndex]; // Uppercase
                        
                        break;
                    }
                    case 1:
                    {
                        int rIndex = Random.Range(0, numbers.Length - 1);
                        gCaptcha += numbers[rIndex];
                        break;
                    }
                }
            }
            
            return gCaptcha;
        }
        
        /// <summary>
        /// Check if the attempt corresponds to the captcha
        /// </summary>
        /// <param name="attempt"></param>
        private void Check(string attempt)
        {
            // Remove spaces
            attempt = attempt.Replace(" ", "");
            
            if (attempt == captcha)
            {
                OnPlayerSuccess?.Invoke();
                PlayerTextInput.OnTextInput -= Check;
            }
            else
            {
                captcha = GenerateCaptcha();
                captchaText.text = captcha;
            }
        }
    }
}