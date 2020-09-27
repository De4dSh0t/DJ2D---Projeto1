using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField] private Tilemap pathTilemap;
    [SerializeField] private Tilemap wallTilemap;
    [SerializeField] private Tile pathTile;
    [SerializeField] private Tile wallTile;
    [SerializeField] private int mazeWidth;
    [SerializeField] private int mazeHeight;
    [SerializeField] private int cellSize;
    [SerializeField] private int wallSize;
    private Stack<Vector2Int> positionStack = new Stack<Vector2Int>();
    private int visitedCellsCount;
    private int[,] maze;

    // Start is called before the first frame update
    void Start()
    {
        maze = new int[mazeWidth, mazeHeight];
        
        DrawMaze();
    }

    /// <summary>
    /// Draws a cell with a space of "wallSize" between each other
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    private void DrawCell(int x, int y)
    {
        for (int cellX = 0; cellX < cellSize; cellX++)
        {
            for (int cellY = 0; cellY < cellSize; cellY++)
            {
                pathTilemap.SetTile(new Vector3Int(x * (cellSize + wallSize) + cellX, y * (cellSize + wallSize) + cellY, 0), pathTile);
            }
        }
    }
    
    /// <summary>
    /// Generates walls, taking into account the "pathTilemap" boundaries
    /// </summary>
    private void GenerateWalls()
    {
        BoundsInt bounds = pathTilemap.cellBounds; //Saves the boundaries of a the "groundMap" tilemap
        
        for (int xMap = bounds.xMin - 1; xMap < bounds.xMax + 1; xMap++)
        {
            for (int yMap = bounds.yMin - 1; yMap < bounds.yMax + 1; yMap++)
            {
                Vector3Int currentPos = new Vector3Int(xMap, yMap, 0); //Vector3Int of the current position

                if (!pathTilemap.HasTile(currentPos)) //Checks if the cell ("tile") at the "currentPos" is empty (null)
                {
                    wallTilemap.SetTile(currentPos, wallTile);
                }
            }
        }
    }

    /// <summary>
    /// Abstractly generates a maze using the Recursive Backtracking algorithm
    /// </summary>
    private void MazeAlgorithm(Vector2Int startPos)
    {
        Vector2Int pos = startPos;
        positionStack.Push(pos);
        visitedCellsCount++;
        List<int> availableDirections = new List<int>(); //0: North | 1: South | 2: East | 3: West
        
        while (visitedCellsCount < mazeWidth * mazeHeight) //Goes through all available cells
        {
            availableDirections.Clear();
            
            //Searches for neighbour cells that hasn't been visited
            for (int posX = pos.x - 1; posX < pos.x + 2; posX++)
            {
                for (int posY = pos.y - 1; posY < pos.y + 2; posY++)
                {
                    if (posX < 0 || posX > mazeWidth || posY < 0 || posY > mazeHeight) //Prevents index out of bounds
                    {
                        break;
                    }

                    if (maze[posX, posY] == 0) //Checks if it hasn't been visited
                    {
                        if (posX > pos.x) availableDirections.Add(0);
                        if (posX < pos.x) availableDirections.Add(1);
                        if (posY > pos.y) availableDirections.Add(2);
                        if (posY < pos.y) availableDirections.Add(3);
                    }
                }
            }

            //Checks if there is any available direction
            if (availableDirections.Count == 0)
            {
                positionStack.Pop(); //Backtracks
                pos = positionStack.Peek();
            }
            
            //Chooses a random direction
            int rIndex = Random.Range(0, availableDirections.Count);

            switch (availableDirections[rIndex])
            {
                case 0: //North
                {
                    pos.y++;
                    maze[pos.x, pos.y] = 1; //Saves the connection between current cell and next cell direction (South)
                    break;
                }
                case 1: //South
                {
                    pos.y--;
                    maze[pos.x, pos.y] = 0; //Saves the connection between current cell and next cell direction (North)
                    break;
                }
                case 2: //East
                {
                    pos.x++;
                    maze[pos.x, pos.y] = 3; //Saves the connection between current cell and next cell direction (West)
                    break;
                }
                case 3: //West
                {
                    pos.x--;
                    maze[pos.x, pos.y] = 2; //Saves the connection between current cell and next cell direction (East)
                    break;
                }
            }

            positionStack.Push(pos);
            visitedCellsCount++;
        }
    }

    /// <summary>
    /// Abstractly generates the grid used by the maze
    /// </summary>
    private void DrawMaze()
    {
        for (int x = 0; x < mazeWidth; x++)
        {
            for (int y = 0; y < mazeHeight; y++)
            {
                DrawCell(x, y);
            }
        }
        
        GenerateWalls();
    }
}
