using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Virus.Sort_Numbers
{
    public class SortBehaviour : MonoBehaviour
    {
        // Button Settings
        private readonly List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        private readonly List<ButtonBehaviour> buttonsList = new List<ButtonBehaviour>();
        private readonly int[] buttonNums = new int[10];
        private int[] numArray; //Used in "CheckOrder"
        private int index;
        
        // Event
        public static event Action OnPlayerMiss;
        public static event Action OnPlayerSuccess;
        
        void Start()
        {
            numArray = numbers.ToArray();
            buttonsList.AddRange(GetComponentsInChildren<ButtonBehaviour>());
            ButtonBehaviour.OnButtonPressed += CheckOrder;
            
            PopulateArray(buttonNums, numbers);
            SetButtonNum(buttonsList);
        }
        
        /// <summary>
        /// Populates the array used to give each button a number
        /// </summary>
        /// <param name="bNums"></param>
        /// <param name="tempList"></param>
        private void PopulateArray(int[] bNums, List<int> tempList)
        {
            for (int i = 0; i < bNums.Length; i++)
            {
                int rIndex = Random.Range(0, tempList.Count - 1);
                buttonNums[i] = tempList[rIndex];
                tempList.RemoveAt(rIndex);
            }
        }
        
        /// <summary>
        /// Sets the number of each button from the "buttonNums" array
        /// </summary>
        /// <param name="bList"></param>
        private void SetButtonNum(List<ButtonBehaviour> bList)
        {
            for (int i = 0; i < buttonNums.Length; i++)
            {
                bList[i].SetText(buttonNums[i].ToString());
            }
        }
        
        /// <summary>
        /// Checks if the player is sorting the number correctly
        /// </summary>
        /// <param name="num"></param>
        private void CheckOrder(string num)
        {
            if (int.Parse(num) == numArray[index])
            {
                index++;
                
                // Successfully finishes the mini challenge
                if (index >= 10)
                {
                    OnPlayerSuccess?.Invoke();
                    ButtonBehaviour.OnButtonPressed -= CheckOrder;
                    index = 0;
                }
            }
            else
            {
                index = 0;
                OnPlayerMiss?.Invoke();
            }
        }
    }
}