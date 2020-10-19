using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Description : MonoBehaviour
{
    private TMP_Text description;

    void Start()
    {
        description = GetComponent<TMP_Text>();
        DifficultyButton.OnDescriptionUpdate += UpdateDescription;
    }

    private void UpdateDescription(string text)
    {
        description.text = text;
    }
}
