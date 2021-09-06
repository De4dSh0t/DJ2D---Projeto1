using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Virus.Teleporting_Buttons
{
    public class TeleportBehaviour : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private float rate;
        [SerializeField] private Vector3 min;
        [SerializeField] private Vector3 max;
        private readonly List<Button> buttonList = new List<Button>();
        private int destroyedButtons;
        
        // Event
        public static event Action OnPlayerSuccess;
        
        void Start()
        {
            buttonList.AddRange(GetComponentsInChildren<Button>());
            InvokeRepeating(nameof(Teleport), 0, rate);
        }
        
        void Update()
        {
            Check();
        }
        
        private void Teleport()
        {
            if (buttonList.Count == 0) return;
            
            foreach (var button in buttonList)
            {
                button.transform.position = new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
            }
        }
        
        private void Check()
        {
            if (destroyedButtons == buttonList.Count)
            {
                OnPlayerSuccess?.Invoke();
            }
        }
        
        public void UpdateCount()
        {
            destroyedButtons++;
        }
    }
}