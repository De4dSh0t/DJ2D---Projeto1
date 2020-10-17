using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusSystem : MonoBehaviour
{
    [SerializeField] private Canvas virusCanvas;
    [SerializeField] private int minPlayerSteps; //Steps to spawn the virus
    [SerializeField] private int maxPlayerSteps; //Steps to spawn the virus
    [SerializeField] private List<GameObject> virusList = new List<GameObject>();
    private List<Vector3> playerPos = new List<Vector3>();
    private GameObject currentVirus;
    private bool canSpawn;
    private int stepsToSpawn;
    
    void Start()
    {
        FriendBehaviour.OnFriendMove += UpdateList;
        CaptchaBehaviour.OnPlayerSuccess += Complete;
        SortBehaviour.OnPlayerSuccess += Complete;
        TeleportBehaviour.OnPlayerSuccess += Complete;
        OrderBehaviour.OnPlayerSuccess += Complete;
        
        stepsToSpawn = Random.Range(minPlayerSteps, maxPlayerSteps);
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
        //currentVirus = Instantiate(virusList[Random.Range(0, virusList.Count)], virusCanvas.transform);
        currentVirus = Instantiate(virusList[3], virusCanvas.transform);
        virusCanvas.gameObject.SetActive(true);
        GameManager.Instance.inGame = false;
    }

    private void Complete()
    {
        Destroy(currentVirus);
        virusCanvas.gameObject.SetActive(false);
        GameManager.Instance.inGame = true;
    }
}
