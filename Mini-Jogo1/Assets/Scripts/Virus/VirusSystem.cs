using System.Collections.Generic;
using Audio;
using Core;
using UI.Menu;
using UnityEngine;
using Virus.Alphabetic_Order;
using Virus.Captcha;
using Virus.Sort_Numbers;
using Virus.Teleporting_Buttons;

namespace Virus
{
    public class VirusSystem : MonoBehaviour
    {
        [Header("System Settings")]
        [SerializeField] private Canvas virusCanvas;
        [SerializeField] private List<GameObject> virusList = new List<GameObject>();
        [SerializeField] private int minPlayerSteps; //Steps to spawn the virus
        [SerializeField] private int maxPlayerSteps; //Steps to spawn the virus
        [SerializeField] private LevelSettings levelSettings;
        [SerializeField] private PlayerTextInput player;
        private readonly List<Vector3> playerPos = new List<Vector3>();
        private GameObject currentVirus;
        private List<int> virusIndex; //Prevent repetition of viruses
        private int previousVirus;
        private int stepsToSpawn;
        
        void Start()
        {
            FriendBehaviour.OnFriendMove += UpdateList;
            CaptchaBehaviour.OnPlayerSuccess += Complete;
            SortBehaviour.OnPlayerSuccess += Complete;
            TeleportBehaviour.OnPlayerSuccess += Complete;
            OrderBehaviour.OnPlayerSuccess += Complete;
            
            //Settings
            minPlayerSteps = levelSettings.minVirusSpawn;
            maxPlayerSteps = levelSettings.maxVirusSpawn;
            
            stepsToSpawn = Random.Range(minPlayerSteps, maxPlayerSteps);
            virusIndex = new List<int>();
            PopulateList(virusIndex);
        }
        
        void Update()
        {
            HandleTrigger();
        }
        
        private void HandleTrigger()
        {
            if (playerPos.Count >= stepsToSpawn && playerPos.Count != 0)
            {
                stepsToSpawn += Random.Range(minPlayerSteps, maxPlayerSteps);
                SpawnVirus();
            }
        }
        
        private void UpdateList(Vector3 currentPos)
        {
            playerPos.Add(currentPos);
        }
        
        private void SpawnVirus()
        {
            //Play glitch sound
            AudioManager.Instance.PlaySound(AudioManager.SoundName.Glitch);
            
            // Choose random virus
            int rIndex = Random.Range(0, virusIndex.Count);
            currentVirus = Instantiate(virusList[virusIndex[rIndex]], virusCanvas.transform);
            virusIndex.Remove(rIndex); //Removes current virus
            virusIndex.Add(previousVirus); //Adds previous virus
            previousVirus = rIndex;
            
            // Activate virus canvas
            virusCanvas.gameObject.SetActive(true);
            GameManager.Instance.inGame = false;
        }
        
        private void Complete()
        {
            // Reset inputfield
            player.ClearText();
            
            // Remove virus from canvas
            Destroy(currentVirus);
            virusCanvas.gameObject.SetActive(false);
            GameManager.Instance.inGame = true;
        }
        
        private void PopulateList(List<int> targetList)
        {
            int index = 0;
            
            for (int i = 0; i < virusList.Count; i++)
            {
                targetList.Add(index++);
            }
        }
        
        private void OnDestroy()
        {
            FriendBehaviour.OnFriendMove -= UpdateList;
            CaptchaBehaviour.OnPlayerSuccess -= Complete;
            SortBehaviour.OnPlayerSuccess -= Complete;
            TeleportBehaviour.OnPlayerSuccess -= Complete;
            OrderBehaviour.OnPlayerSuccess -= Complete;
        }
    }
}