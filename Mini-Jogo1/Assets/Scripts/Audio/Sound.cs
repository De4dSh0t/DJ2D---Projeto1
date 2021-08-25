using System;
using UnityEngine;

namespace Audio
{
    [Serializable]
    public class Sound
    {
        public AudioManager.SoundName soundName;
        public AudioClip clip;
    }
}