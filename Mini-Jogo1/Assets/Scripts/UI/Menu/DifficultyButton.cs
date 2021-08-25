using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.Menu
{
    public class DifficultyButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("UI Settings")]
        [SerializeField] private string description;
        [SerializeField] private bool hasEnemy;
        [SerializeField] private float timeEnemyMovement;
        [SerializeField] private int minVirusSpawn;
        [SerializeField] private int maxVirusSpawn;
        [SerializeField] private LevelSettings levelSettings;
        [SerializeField] private GameObject loadingScreen;
        private Button button;
        
        // Event
        public static Action<string> OnDescriptionUpdate;
        
        void Start()
        {
            button = GetComponent<Button>();
        }
        
        void Update()
        {
            button.onClick.AddListener(SetSettings);
        }
        
        private void SetSettings()
        {
            levelSettings.hasEnemy = hasEnemy;
            levelSettings.timeEnemyMovement = timeEnemyMovement;
            levelSettings.minVirusSpawn = minVirusSpawn;
            levelSettings.maxVirusSpawn = maxVirusSpawn;
        
            loadingScreen.SetActive(true);
            SceneManager.LoadScene("Game1");
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            OnDescriptionUpdate(description);
        }
        
        public void OnPointerExit(PointerEventData eventData)
        {
            OnDescriptionUpdate("");
        }
    }
}