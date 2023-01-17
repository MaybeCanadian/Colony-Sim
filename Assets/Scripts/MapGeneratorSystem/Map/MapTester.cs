using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTester : MonoBehaviour
{
    [SerializeField]
    private MapObject map = null;

    [SerializeField]
    private int mapWidth = 1;
    [SerializeField]
    private int mapHeight = 1;

    [SerializeField]
    private int chunkWidth = 10;
    [SerializeField]
    private int chunkHeight = 10;

    [SerializeField]
    private GridType tileGridType = GridType.HEX;
    [SerializeField]
    private GridType chunkGridType = GridType.SQUARE;

    private void Start()
    {
        MapGenerator.GenerateMap(tileGridType, chunkGridType, mapWidth, mapHeight, chunkWidth, chunkHeight);
        map = MapGenerator.GetMap();

        Debug.Log("Node count is - " + map.GetNodeCount());
        Debug.Log("Chunk count is - " + map.GetCountCount());
    }
}
