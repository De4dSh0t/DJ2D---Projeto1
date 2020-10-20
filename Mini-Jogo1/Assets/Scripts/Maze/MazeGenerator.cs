using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MazeGenerator : MonoBehaviour
{
    [Header("Tile Settings")]
    [SerializeField] private Tilemap pathTilemap;
    [SerializeField] private Tilemap wallTilemap;
    [SerializeField] private Tilemap exitTilemap;
    [SerializeField] private Tile pathTile;
    [SerializeField] private Tile wallTile;
    [SerializeField] private Tile exitTile;

    [Header("Maze Settings")]
    [SerializeField] private Vector2Int startPos = Vector2Int.zero;
    [SerializeField] private int mazeWidth;
    [SerializeField] private int mazeHeight;
    [SerializeField] private int cellSize;
    [SerializeField] private int wallSize;
    private Stack<Vector2Int> positionStack = new Stack<Vector2Int>();
    private List<Vector2Int> visitedCellsPosition = new List<Vector2Int>();
    private List<Vector2Int[]> stackList = new List<Vector2Int[]>();
    private int visitedCellsCount;
    private int[,] maze;
    public static Vector3Int exitPosition;

    // Start is called before the first frame update
    void Start()
    {
        maze = new int[mazeWidth, mazeHeight];
        
        MazeAlgorithm(startPos);
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
    /// Draws a connection between two cells
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    private void DrawConnection(int x, int y)
    {
        int direction = maze[x, y];

        switch (direction)
        {
            case 0: //North
            {
                for (int posX = x * (cellSize + wallSize); posX < x * (cellSize + wallSize) + cellSize; posX++)
                {
                    pathTilemap.SetTile(new Vector3Int(posX, y * (cellSize + wallSize) + cellSize, 0), pathTile);
                }
                break;
            }
            case 1: //South
            {
                for (int posX = x * (cellSize + wallSize); posX < x * (cellSize + wallSize) + cellSize; posX++)
                {
                    pathTilemap.SetTile(new Vector3Int(posX, y * (cellSize + wallSize) - 1, 0), pathTile);
                }
                break;
            }
            case 2: //East
            {
                for (int posY = y * (cellSize + wallSize); posY < y * (cellSize + wallSize) + cellSize; posY++)
                {
                    pathTilemap.SetTile(new Vector3Int(x * (cellSize + wallSize) + cellSize, posY, 0), pathTile);
                }
                break;
            }
            case 3: //West
            {
                for (int posY = y * (cellSize + wallSize); posY < y * (cellSize + wallSize) + cellSize; posY++)
                {
                    pathTilemap.SetTile(new Vector3Int(x * (cellSize + wallSize) - 1, posY, 0), pathTile);
                }
                break;
            }
        }
    }
    
    /// <summary>
    /// Generates walls, taking into account the "pathTilemap" boundaries
    /// </summary>
    private void DrawWalls()
    {
        BoundsInt bounds = pathTilemap.cellBounds; //Saves the boundaries of a the "groundMap" tilemap
        
        for (int xMap = bounds.xMin - 1; xMap < bounds.xMax + 1; xMap++)
        {
            for (int yMap = bounds.yMin - 1; yMap < bounds.yMax + 1; yMap++)
            {
                Vector3Int currentPos = new Vector3Int(xMap, yMap, 0); //Vector3Int of the current position

                if (!pathTilemap.HasTile(currentPos) && !exitTilemap.HasTile(currentPos)) //Checks if the cell ("tile") at the "currentPos" is empty (null)
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
        Vector2Int currentPos = startPos;
        positionStack.Push(currentPos);
        visitedCellsPosition.Add(currentPos);
        visitedCellsCount++;
        List<int> availableDirections = new List<int>(); //0: North | 1: South | 2: East | 3: West
        
        while (visitedCellsCount < mazeWidth * mazeHeight) //Goes through all available cells
        {
            availableDirections.Clear();

            //Searches for neighbour cells that haven't been visited
            if (currentPos.y + 1 < mazeHeight)
            {
                if (!visitedCellsPosition.Contains(new Vector2Int(currentPos.x, currentPos.y + 1)))
                {
                    availableDirections.Add(0);
                }
            }
            if (currentPos.y - 1 >= 0)
            {
                if (!visitedCellsPosition.Contains(new Vector2Int(currentPos.x, currentPos.y - 1)))
                {
                    availableDirections.Add(1);
                }
            }
            if (currentPos.x + 1 < mazeWidth)
            {
                if (!visitedCellsPosition.Contains(new Vector2Int(currentPos.x + 1, currentPos.y)))
                {
                    availableDirections.Add(2);
                }
            }
            if (currentPos.x - 1 >= 0)
            {
                if (!visitedCellsPosition.Contains(new Vector2Int(currentPos.x - 1, currentPos.y)))
                {
                    availableDirections.Add(3);
                }
            }

            //Checks if there is no available direction
            if (availableDirections.Count == 0)
            {
                stackList.Add(positionStack.ToArray()); //Saves current stack to determine the longest path
                positionStack.Pop(); //Backtracks
                currentPos = positionStack.Peek();
                continue;
            }
            
            //Chooses a random direction
            int rIndex = Random.Range(0, availableDirections.Count);

            switch (availableDirections[rIndex])
            {
                case 0: //North
                {
                    currentPos.y++;
                    maze[currentPos.x, currentPos.y] = 1; //Saves the connection between current cell and next cell direction (South)
                    break;
                }
                case 1: //South
                {
                    currentPos.y--;
                    maze[currentPos.x, currentPos.y] = 0; //Saves the connection between current cell and next cell direction (North)
                    break;
                }
                case 2: //East
                {
                    currentPos.x++;
                    maze[currentPos.x, currentPos.y] = 3; //Saves the connection between current cell and next cell direction (West)
                    break;
                }
                case 3: //West
                {
                    currentPos.x--;
                    maze[currentPos.x, currentPos.y] = 2; //Saves the connection between current cell and next cell direction (East)
                    break;
                }
            }

            positionStack.Push(currentPos);
            visitedCellsPosition.Add(currentPos);
            visitedCellsCount++;
        }
    }

    /// <summary>
    /// Determines the longest exit and draws
    /// </summary>
    private void DrawExit()
    {
        int iMax = 0;

        for (int i = 1; i < stackList.Count; i++)
        {
            if (stackList[i].Length > stackList[iMax].Length)
            {
                iMax = i;
            }
        }

        Vector2Int exitPos = stackList[iMax][0]; //Gets the first (last from stack) Vector2Int from the array with the most variables
        exitPosition = new Vector3Int(exitPos.x * (cellSize + wallSize) + 1, exitPos.y * (cellSize + wallSize) + 1, -1);
        
        //Draws Tiles
        for (int cellX = 0; cellX < cellSize; cellX++)
        {
            for (int cellY = 0; cellY < cellSize; cellY++)
            {
                exitTilemap.SetTile(new Vector3Int(exitPos.x * (cellSize + wallSize) + cellX, exitPos.y * (cellSize + wallSize) + cellY, 0), exitTile);
                pathTilemap.SetTile(new Vector3Int(exitPos.x * (cellSize + wallSize) + cellX, exitPos.y * (cellSize + wallSize) + cellY, 0), null);
            }
        }
    }

    /// <summary>
    /// Draws the maze, using the maze 2D array
    /// </summary>
    private void DrawMaze()
    {
        for (int x = startPos.x; x < mazeWidth; x++)
        {
            for (int y = startPos.y; y < mazeHeight; y++)
            {
                DrawCell(x, y);
                DrawConnection(x, y);
            }
        }
        
        DrawExit();
        DrawWalls();
    }
}