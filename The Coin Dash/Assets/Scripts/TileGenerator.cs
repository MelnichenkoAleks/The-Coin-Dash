using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    private List<GameObject> activeTiles = new List<GameObject>();
    private float spawnPos = 0;
    private float tileLength = 200;

    [SerializeField] private Transform player;
    private int startTiles = 6;

    private int lastTileIndex = -1; 

    void Start()
    {
        for (int i = 0; i < startTiles; i++)
        {
            if (i == 0)
                SpawnTile(5);
            else
                SpawnRandomTile();
        }
    }

    void Update()
    {
        if (player.position.z - 175 > spawnPos - (startTiles * tileLength))
        {
            SpawnRandomTile();
            DeleteTile();
        }
    }

    private void SpawnRandomTile()
    {
        int randomTileIndex = GetRandomTileIndex();
        SpawnTile(randomTileIndex);
    }

    private int GetRandomTileIndex()
    {
        int randomIndex = Random.Range(0, tilePrefabs.Length);

        while (randomIndex == lastTileIndex)
        {
            randomIndex = Random.Range(0, tilePrefabs.Length);
        }

        lastTileIndex = randomIndex;
        return randomIndex;
    }

    private void SpawnTile(int tileIndex)
    {
        GameObject nextTile = Instantiate(tilePrefabs[tileIndex], transform.forward * spawnPos, transform.rotation);
        activeTiles.Add(nextTile);
        spawnPos += tileLength;
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
