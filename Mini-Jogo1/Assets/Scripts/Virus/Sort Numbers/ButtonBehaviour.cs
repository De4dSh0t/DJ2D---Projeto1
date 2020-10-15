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

    private void Awake()
    {
        txt = GetComponentInChildren<TMP_Text>();
        button = GetComponent<Button>();
        defaultColor = button.image.color;
    }

    void Start()
    {
        SortBehaviour.OnPlayerMiss += UnlockButton;
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
        if (button == null) return;
        button.interactable = false;
        button.image.color = new Color(0.08f, 0.27f, 0.48f);
        if(OnButtonPressed != null) OnButtonPressed(txt.text);
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
}
