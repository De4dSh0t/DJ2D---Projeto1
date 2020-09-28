using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FriendBehaviour : MonoBehaviour
{
    [Header("Commands Settings")]
    [SerializeField] private List<string> commands = new List<string>();

    [Header("Movement Settings")] 
    [SerializeField] private Tilemap wallTilemap; //To prevent the player going over walls
    [SerializeField] private int moveDistance;
    private Transform friendPos;
    
    void Start()
    {
        friendPos = GetComponent<Transform>();
        PlayerTextInput.OnTextInput += TryMove;
    }

    /// <summary>
    /// Tries moving the "friend" bot by checking if the input corresponds to a command
    /// </summary>
    /// <param name="textInput"></param>
    /// <returns></returns>
    private void TryMove(string textInput)
    {
        int direction = -2;
        Vector3 currentPos = friendPos.position;
        
        for (int i = 0; i < commands.Count; i++)
        {
            if (textInput.ToUpper() == commands[i])
            {
                direction = i;
                break;
            }
        }

        switch (direction)
        {
            case -1: //WALL (CAN'T MOVE)
            {
                Debug.Log("There is a wall in front of me!");
                break;
            }
            case 0: //LEFT
            {
                if (wallTilemap.HasTile(new Vector3Int((int) currentPos.x - 1, (int) currentPos.y, 0)))
                {
                    goto case -1;
                }
                
                friendPos.Translate(Vector2.left * moveDistance);
                break;
            }
            case 1: //RIGHT
            {
                if (wallTilemap.HasTile(new Vector3Int((int) currentPos.x + 1, (int) currentPos.y, 0)))
                {
                    goto case -1;
                }
                
                friendPos.Translate(Vector2.right * moveDistance);
                break;
            }
            case 2: //UP
            {
                if (wallTilemap.HasTile(new Vector3Int((int) currentPos.x, (int) currentPos.y + 1, 0)))
                {
                    goto case -1;
                }
                
                friendPos.Translate(Vector2.up * moveDistance);
                break;
            }
            case 3: //DOWN
            {
                if (wallTilemap.HasTile(new Vector3Int((int) currentPos.x, (int) currentPos.y - 1, 0)))
                {
                    goto case -1;
                }
                
                friendPos.Translate(Vector2.down * moveDistance);
                break;
            }
            default: //WRONG COMMAND
            {
                Debug.Log("Not a command!");
                break;
            }
        }
    }
}