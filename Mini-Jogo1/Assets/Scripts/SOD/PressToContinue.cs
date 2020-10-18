using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PressToContinue : MonoBehaviour
{
    [SerializeField] private float timeToActivate;
    [SerializeField] private float fadeRate;
    private bool isVisible;
    private TMP_Text text;
    
    void Start()
    {
        text = GetComponent<TMP_Text>();
    }
    
    void Update()
    {
        if (Time.time >= timeToActivate)
        {
            if (isVisible)
            {
                text.alpha -= fadeRate * Time.deltaTime;

                if (text.alpha <= 0)
                {
                    isVisible = false;
                }
            }
            else
            {
                text.alpha += fadeRate * Time.deltaTime;

                if (text.alpha >= 1)
                {
                    isVisible = true;
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}
