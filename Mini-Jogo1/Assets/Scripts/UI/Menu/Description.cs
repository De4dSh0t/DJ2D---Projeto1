using TMPro;
using UnityEngine;

namespace UI.Menu
{
    public class Description : MonoBehaviour
    {
        private TMP_Text description;
        
        void Start()
        {
            description = GetComponent<TMP_Text>();
            DifficultyButton.OnDescriptionUpdate += UpdateDescription;
        }
        
        private void UpdateDescription(string text)
        {
            description.text = text;
        }
    }
}