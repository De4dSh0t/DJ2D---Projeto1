using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class TeleportBehaviour : MonoBehaviour
{
    [SerializeField] private float rate;
    [SerializeField] private Vector3 min;
    [SerializeField] private Vector3 max;
    private List<Button> buttonList = new List<Button>();
    private int destroyedButtons;

    public static Action OnPlayerSuccess;

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
            OnPlayerSuccess();
        }
    }

    public void UpdateCount()
    {
        destroyedButtons++;
    }
}
