using UnityEngine;
using System.Collections.Generic;

public class GenerateTerrain : MonoBehaviour
{
    public GameObject[] terrainTiles; // Массив префабов для ландшафта
    public Transform player;          // Ссылка на камеру или персонажа
    public float tileSize = 10f;      // Размер плитки (можно настроить)
    public float groundHeight = 0f;   // Высота, на которой будут генерироваться плитки
    public int gridSize = 5;          // Размер сетки (количество плиток по осям)

    private HashSet<Vector3> occupiedPositions = new HashSet<Vector3>(); // Множество занятых позиций плиток
    private Vector3 lastPlayerPosition; // Для отслеживания движения игрока

    void Start()
    {
        lastPlayerPosition = player.position;
        GenerateInitialTiles();
    }

    void Update()
    {
        if (Vector3.Distance(player.position, lastPlayerPosition) > tileSize)
        {
            lastPlayerPosition = player.position;
            GenerateSurroundingTiles();
        }
    }

    void GenerateInitialTiles()
    {
        // Генерация плиток в сетке вокруг начальной позиции игрока
        for (int x = -gridSize; x <= gridSize; x++)
        {
            for (int z = -gridSize; z <= gridSize; z++)
            {
                Vector3 spawnPos = new Vector3(player.position.x + x * tileSize, groundHeight, player.position.z + z * tileSize);
                SpawnTile(spawnPos);
            }
        }
    }

    void GenerateSurroundingTiles()
    {
        // Проверка и генерация плиток по сетке вокруг игрока
        for (int x = -gridSize; x <= gridSize; x++)
        {
            for (int z = -gridSize; z <= gridSize; z++)
            {
                Vector3 spawnPos = new Vector3(Mathf.Round((player.position.x + x * tileSize) / tileSize) * tileSize,
                                                groundHeight,
                                                Mathf.Round((player.position.z + z * tileSize) / tileSize) * tileSize);

                // Проверка, существует ли уже плитка на этой позиции
                if (!occupiedPositions.Contains(spawnPos))
                {
                    SpawnTile(spawnPos);
                }
            }
        }
    }

    void SpawnTile(Vector3 position)
    {
        GameObject tile = Instantiate(terrainTiles[Random.Range(0, terrainTiles.Length)], position, Quaternion.identity);
        occupiedPositions.Add(position); // Добавляем позицию в занятые
    }
}
