using UnityEngine;

namespace Audio
{
    public class SoundReferences : MonoBehaviour
    {
        public SoundSettings settings;
        public Sound[] sounds;
        
        void Start()
        {
            DontDestroyOnLoad(this);
        }
    }
}