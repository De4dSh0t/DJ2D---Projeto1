using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class OrderBehaviour : MonoBehaviour
{
    private char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
    private readonly char[] buttonChars = new char[6];
    private List<ButtonBehaviour> buttonList = new List<ButtonBehaviour>();
    private char[] alphabeticOrder = new char[6]; //Array used to compare
    private int index;

    public static Action OnPlayerMiss;
    public static Action OnPlayerSuccess;
    
    void Start()
    {
        buttonList.AddRange(GetComponentsInChildren<ButtonBehaviour>());
        ButtonBehaviour.OnButtonPressed += CheckOrder;
        
        PopulateArray(buttonChars, alphabet.ToList());
        SetButtonChar(buttonList);
    }

    /// <summary>
    /// Populates the array used to give each button a char
    /// </summary>
    /// <param name="bChars"></param>
    /// <param name="tempList"></param>
    private void PopulateArray(char[] bChars, List<char> tempList)
    {
        for (int i = 0; i < bChars.Length; i++)
        {
            int rIndex = Random.Range(0, tempList.Count - 1);
            bChars[i] = tempList[rIndex];
            alphabeticOrder[i] = tempList[rIndex];
            tempList.RemoveAt(rIndex);
        }
        
        Array.Sort(alphabeticOrder); //Sorts the helper array in alphabetic order
    }
    
    /// <summary>
    /// Sets the number of each button from the "buttonNums" array
    /// </summary>
    /// <param name="bList"></param>
    private void SetButtonChar(List<ButtonBehaviour> bList)
    {
        for (int i = 0; i < buttonChars.Length; i++)
        {
            bList[i].SetText(buttonChars[i].ToString());
        }
    }
    
    /// <summary>
    /// Checks if the player is sorting the number correctly
    /// </summary>
    /// <param name="s"></param>
    private void CheckOrder(string s)
    {
        if (char.Parse(s) == alphabeticOrder[index])
        {
            index++;

            // Successfully finishes the mini challenge
            if (index >= 6)
            {
                if (OnPlayerSuccess != null) OnPlayerSuccess();
                ButtonBehaviour.OnButtonPressed -= CheckOrder;
                index = 0;
            }
        }
        else
        {
            index = 0;
            if (OnPlayerMiss != null) OnPlayerMiss();
        }
    }
}
