using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Clock : MonoBehaviour
{
    private TMP_Text clockText;

    void Start()
    {
        clockText = GetComponent<TMP_Text>();
    }
    
    void Update()
    {
        clockText.text = DateTime.Now.ToShortTimeString();
    }
}
