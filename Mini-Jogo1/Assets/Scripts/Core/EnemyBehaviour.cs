using System.Collections.Generic;
using UI.Menu;
using UnityEngine;

namespace Core
{
    public class EnemyBehaviour : MonoBehaviour
    {
        [Header("Movement Settings")] 
        [SerializeField] private int friendStepsToMove; 
        [SerializeField] private float secToMove;
        [SerializeField] private LevelSettings levelSettings;
        private readonly Queue<Vector3> friendSteps = new Queue<Vector3>();
        private SpriteRenderer spriteRenderer;
        public bool canMove;
        private float timer;
        
        void Start()
        {
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
            HandleStart();
            HandleMovement();
        }
        
        private void HandleStart()
        {
            if (friendSteps.Count >= friendStepsToMove && !canMove)
            {
                FriendBehaviour.OnFriendResponse?.Invoke("I heard something! I think I'm not alone!");
                canMove = true;
                spriteRenderer.enabled = true;
            }
        }
        
        private void HandleMovement()
        {
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
        
        private void OnDestroy()
        {
            FriendBehaviour.OnFriendMove -= UpdateQueue;
        }
    }
}