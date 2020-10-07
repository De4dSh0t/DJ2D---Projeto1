using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [Header("Movement Settings")] 
    [SerializeField] private int friendStepsToMove;
    [SerializeField] private float secToMove;
    private Queue<Vector3> friendSteps = new Queue<Vector3>();
    private bool canMove;
    private float timer;

    void Start()
    {
        FriendBehaviour.OnFriendMove += UpdateQueue;
        timer = secToMove;
    }
    
    void Update()
    {
        if (friendSteps.Count >= friendStepsToMove)
        {
            canMove = true;
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
        friendSteps.Enqueue(friendPos);
    }

    private void Move()
    {
        transform.position = friendSteps.Dequeue();
    }
}
