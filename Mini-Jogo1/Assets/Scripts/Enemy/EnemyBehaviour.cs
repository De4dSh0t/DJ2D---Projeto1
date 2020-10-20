using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [Header("Movement Settings")] 
    [SerializeField] private int friendStepsToMove; 
    [SerializeField] private float secToMove;
    [SerializeField] private LevelSettings levelSettings;
    private Queue<Vector3> friendSteps = new Queue<Vector3>();
    private SpriteRenderer spriteRenderer;
    public bool canMove;
    private float timer;

    void Start()
    {
        GameManager.Instance.OnSceneUnload += UnsubscribeAll;
        
        FriendBehaviour.OnFriendMove += UpdateQueue;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
        timer = secToMove;

        //Settings
        if (!levelSettings.hasEnemy)
        {
            gameObject.SetActive(false);
        }
        secToMove = levelSettings.timeEnemyMovement;
    }
    
    void Update()
    {
        if (friendSteps.Count >= friendStepsToMove)
        {
            if (!canMove) FriendBehaviour.OnFriendResponse("I heard something! I think I'm not alone!");
            canMove = true;
            spriteRenderer.enabled = true;
        }
        
        if (canMove && friendSteps.Count != 0)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                Move();
                timer = secToMove;
            }
        }
    }

    /// <summary>
    /// Updates the queue according to each "friend" bot movement
    /// </summary>
    /// <param name="friendPos"></param>
    private void UpdateQueue(Vector3 friendPos)
    {
        if (friendSteps.Contains(friendPos)) return;
        friendSteps.Enqueue(friendPos);
    }

    private void Move()
    {
        transform.position = friendSteps.Dequeue();
    }

    private void UnsubscribeAll()
    {
        FriendBehaviour.OnFriendMove -= UpdateQueue;
        GameManager.Instance.OnSceneUnload -= UnsubscribeAll;
    }
}
