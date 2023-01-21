using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTester : MonoBehaviour
{
    [SerializeField]
    private MapObject map = null;

    [SerializeField]
    private int mapWidth = 10;
    [SerializeField]
    private int mapHeight = 10;

    [SerializeField]
    private float XTileOffset = 1.0f;
    [SerializeField]
    private float YTileOffset = 1.0f;

    [SerializeField]
    private GridType tileGridType = GridType.HEX;
    [SerializeField]
    private GridType chunkGridType = GridType.SQUARE;

    private void Start()
    {
        MapGenerator.GenerateMap(tileGridType, chunkGridType, mapWidth, mapHeight, XTileOffset, YTileOffset);
        map = MapGenerator.GetMap();

        Debug.Log("Node count is - " + map.GetNodeCount());

        MapVisualsController.instance.CreateMapVisuals();
    }
}
