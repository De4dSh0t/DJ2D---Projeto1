using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FriendBehaviour : MonoBehaviour
{
    [Header("Commands Settings")]
    [SerializeField] private string[] commands;

    [Header("Movement Settings")] 
    [SerializeField] private Tilemap wallTilemap; //To prevent the player going over walls
    [SerializeField] private int moveDistance;
    private Transform friendPos;

    public static Action<string> OnFriendResponse;
    
    void Start()
    {
        friendPos = GetComponent<Transform>();
        PlayerTextInput.OnTextInput += TryMove; //Subscribes to the action "OnTextInput"
    }

    /// <summary>
    /// Tries moving the "friend" bot by checking if the input corresponds to a command
    /// </summary>
    /// <param name="textInput"></param>
    /// <returns></returns>
    private void TryMove(string textInput)
    {
        string[] words = textInput.Split(' ');
        int direction = -1;
        Vector3 currentPos = friendPos.position;
        int nMoves = 1;
        bool canMove = true;

        if (words.Length == 2)
        {
            if (Int32.TryParse(words[0], out int result))
            {
                nMoves = result;
            }
        
            //Checks if the input correspond to any command
            for (int i = 0; i < commands.Length; i++)
            {
                if (words[1].ToUpper() == commands[i])
                {
                    direction = i;
                    break;
                }
            }
        }

        //Moves the "friend" bot "nMoves" times, allways checking if it's possible to do so
        for (int i = 0; i < nMoves; i++)
        {
            switch (direction)
            {
                case -2: //WALL (CAN'T MOVE)
                {
                    //Debug.Log("There is a wall in front of me!");
                    OnFriendResponse("There is a wall in front of me!");
                    canMove = false;
                    break;
                }
                case -1: //WRONG COMMAND
                {
                    //Debug.Log("Not a command!");
                    OnFriendResponse("Not a command!");
                    canMove = false;
                    break;
                }
                case 0: //LEFT
                {
                    if (wallTilemap.HasTile(new Vector3Int((int) currentPos.x - 2, (int) currentPos.y, 0)))
                    {
                        goto case -2;
                    }
                            
                    friendPos.Translate(Vector2.left * moveDistance);
                    break;
                }
                case 1: //RIGHT
                {
                    if (wallTilemap.HasTile(new Vector3Int((int) currentPos.x + 1, (int) currentPos.y, 0)))
                    {
                        goto case -2;
                    }
                            
                    friendPos.Translate(Vector2.right * moveDistance);
                    break;
                }
                case 2: //UP
                {
                    if (wallTilemap.HasTile(new Vector3Int((int) currentPos.x, (int) currentPos.y + 1, 0)))
                    {
                        goto case -2;
                    }
                            
                    friendPos.Translate(Vector2.up * moveDistance);
                    break;
                }
                case 3: //DOWN
                {
                    if (wallTilemap.HasTile(new Vector3Int((int) currentPos.x, (int) currentPos.y - 2, 0)))
                    {
                        goto case -2;
                    }
                            
                    friendPos.Translate(Vector2.down * moveDistance);
                    break;
                }
            }
            
            if(!canMove) break;

            currentPos = friendPos.position;
        }
    }
}