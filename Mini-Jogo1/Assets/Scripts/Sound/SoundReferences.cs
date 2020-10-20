using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundReferences : MonoBehaviour
{
    public Sound[] sounds;

    void Start()
    {
        DontDestroyOnLoad(this);
    }
}
