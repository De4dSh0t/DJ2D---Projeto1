using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sound Settings", menuName = "ScriptableObjects/SoundSettings", order = 2)]
public class SoundSettings : ScriptableObject
{
    public bool music;
    public bool sfx;
    public float volume;
}
