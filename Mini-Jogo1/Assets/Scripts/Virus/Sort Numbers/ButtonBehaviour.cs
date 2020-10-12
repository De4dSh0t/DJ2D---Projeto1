using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBehaviour : MonoBehaviour
{
    private TMP_Text txt;
    private Button button;
    private Color defaultColor;

    public static Action<string> OnButtonPressed;
    
    void Start()
    {
        txt = GetComponentInChildren<TMP_Text>();
        button = GetComponent<Button>();
        defaultColor = button.image.color;

        SortBehaviour.OnPlayerMiss += UnlockButton;
        SortBehaviour.OnPlayerSuccess += Completed;
    }

    /// <summary>
    /// Sets the number of the button
    /// </summary>
    public void SetNumber(string num)
    {
        txt.text = num;
    }

    /// <summary>
    /// Locks the button when pressed
    /// </summary>
    public void LockButton()
    {
        button.enabled = false;
        button.image.color = new Color(0.70f, 0.70f, 0.70f);
        OnButtonPressed(txt.text);
    }

    /// <summary>
    /// Unlocks the button when player fails to sort the numbers
    /// </summary>
    public void UnlockButton()
    {
        button.enabled = true;
        button.image.color = defaultColor;
    }

    public void Completed()
    {
        button.image.color = new Color(0.43f, 0.92f, 0.31f);
    }
}
