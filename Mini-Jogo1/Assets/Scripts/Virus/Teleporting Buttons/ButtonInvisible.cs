using UnityEngine;

namespace Virus.Teleporting_Buttons
{
    public class ButtonInvisible : MonoBehaviour
    {
        public void Invisible()
        {
            gameObject.SetActive(false);
        }
    }
}