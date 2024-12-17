using System;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int gridWidth = 5, gridHeight = 5;
    [SerializeField] public GameObject whiteTilePrefab;
    [SerializeField] public GameObject blackTilePrefab;
    [SerializeField] public GameObject redTilePrefab;
    [SerializeField] public GameObject glassTilePrefab;
    [SerializeField] public GameObject changeTilePrefab;
    [SerializeField] public GameObject playerPrefab;
    [SerializeField] public Transform gridParent;
    [SerializeField] public Vector2Int playerStartPosition = new Vector2Int(2, 2);
    [SerializeField] public Vector2Int[] wallPositions;
    [SerializeField] public Vector2Int[] redTilePositions;
    [SerializeField] public Vector2Int[] glassTilePositions;
    [SerializeField] public Vector2Int[] changeTilePositions;

    public GameObject player;
    public Vector2Int playerPosition;
    private bool[,] wallGrid;

    void Start()
    {
        if (gridParent == null)
        {
            Debug.LogError("Grid Parent is not assigned!");
            return;
        }

        GenerateGrid();
    }

    void GenerateGrid()
    {
        wallGrid = new bool[gridWidth, gridHeight];

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Vector3 position = new Vector3(x, y, 0);

                if (Array.Exists(wallPositions, pos => pos.x == x && pos.y == y))
                {
                    Instantiate(blackTilePrefab, position, Quaternion.identity, gridParent);
                    wallGrid[x, y] = true;
                }
                else if (Array.Exists(redTilePositions, pos => pos.x == x && pos.y == y))
                {
                    position.z = 50; 
                    Instantiate(redTilePrefab, position, Quaternion.identity, gridParent);
                }
                else if (Array.Exists(glassTilePositions, pos => pos.x == x && pos.y == y))
                {
                    position.z = 50; 
                    Instantiate(glassTilePrefab, position, Quaternion.identity, gridParent);

                }
                else if (Array.Exists(changeTilePositions, pos => pos.x == x && pos.y == y))
                {
                    position.z = 50; 
                    Instantiate(changeTilePrefab, position, Quaternion.identity, gridParent);
                }
                else
                {
                    position.z = 50; 
                    Instantiate(whiteTilePrefab, position, Quaternion.identity, gridParent);
                }
            }
        }

        playerPosition = playerStartPosition;
        Vector3 playerWorldPosition = new Vector3(playerPosition.x, playerPosition.y, 0);
        player = Instantiate(playerPrefab, playerWorldPosition, Quaternion.identity, gridParent);

        Camera.main.transform.position = new Vector3(gridWidth / 2f - 0.5f, gridHeight / 2f - 0.5f, -10);
        Camera.main.orthographicSize = gridHeight / 2f + 1;
    }

    public bool IsValidMove(Vector2Int position)
    {
        if (position.x < 0 || position.x >= gridWidth || position.y < 0 || position.y >= gridHeight)
            return false;

        if (wallGrid[position.x, position.y])
            return false;

        return true;
    }
}
