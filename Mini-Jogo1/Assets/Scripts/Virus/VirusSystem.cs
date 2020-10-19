using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VirusSystem : MonoBehaviour
{
    [SerializeField] private Canvas virusCanvas;
    [SerializeField] private List<GameObject> virusList = new List<GameObject>();
    [SerializeField] private int minPlayerSteps; //Steps to spawn the virus
    [SerializeField] private int maxPlayerSteps; //Steps to spawn the virus
    [SerializeField] private LevelSettings levelSettings;
    private List<Vector3> playerPos = new List<Vector3>();
    private GameObject currentVirus;
    private bool canSpawn;
    private int stepsToSpawn;
    private List<int> virusIndex; //Prevent repetition of viruses
    private int previousVirus;
    
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
        if (playerPos.Count >= stepsToSpawn && playerPos.Count != 0)
        {
            canSpawn = true;
            stepsToSpawn += Random.Range(minPlayerSteps, maxPlayerSteps);
        }

        if (canSpawn)
        {
            SpawnVirus();
            canSpawn = false;
        }
    }

    private void UpdateList(Vector3 currentPos)
    {
        playerPos.Add(currentPos);
    }

    private void SpawnVirus()
    {
        int rIndex = Random.Range(0, virusIndex.Count);
        currentVirus = Instantiate(virusList[virusIndex[rIndex]], virusCanvas.transform);
        virusIndex.Remove(rIndex); //Removes current virus
        virusIndex.Add(previousVirus); //Adds previous virus
        previousVirus = rIndex;
        
        virusCanvas.gameObject.SetActive(true);
        GameManager.Instance.inGame = false;
    }

    private void Complete()
    {
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
}
