using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundReferences : MonoBehaviour
{
    public SoundSettings settings;
    public Sound[] sounds;

    public static Action OnUpdateSettings;

    void Start()
    {
        DontDestroyOnLoad(this);
    }
}
