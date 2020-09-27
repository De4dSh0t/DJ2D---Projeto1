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
    private List<Vector2Int> visitedCellsPosition = new List<Vector2Int>();
    private int visitedCellsCount;
    private int[,] maze;

    // Start is called before the first frame update
    void Start()
    {
        maze = new int[mazeWidth, mazeHeight];
        
        MazeAlgorithm(Vector2Int.zero);
        foreach (var direction in maze)
        {
            Debug.Log(direction);
        }
        
        //DrawMaze();
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
            case 0:
            {
                for (int posX = x; posX < cellSize; posX++)
                {
                    pathTilemap.SetTile(new Vector3Int(posX, y + cellSize + 1, 0), pathTile);
                }
                break;
            }
            case 1:
            {
                for (int posX = x; posX < cellSize; posX++)
                {
                    pathTilemap.SetTile(new Vector3Int(posX, y - 1, 0), pathTile);
                }
                break;
            }
            case 2:
            {
                for (int posY = y; posY < cellSize; posY++)
                {
                    pathTilemap.SetTile(new Vector3Int(x + cellSize + 1, posY, 0), pathTile);
                }
                break;
            }
            case 3:
            {
                for (int posY = y; posY < cellSize; posY++)
                {
                    pathTilemap.SetTile(new Vector3Int(x - 1, posY, 0), pathTile);
                }
                break;
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

            //Checks if there is any available direction
            if (availableDirections.Count == 0)
            {
                positionStack.Pop(); //Backtracks
                currentPos = positionStack.Peek();
                break;
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
    /// Abstractly generates the grid used by the maze
    /// </summary>
    private void DrawMaze()
    {
        for (int x = 0; x < mazeWidth; x++)
        {
            for (int y = 0; y < mazeHeight; y++)
            {
                DrawCell(x, y);
                DrawConnection(x, y);
            }
        }
        
        //GenerateWalls();
    }
}
