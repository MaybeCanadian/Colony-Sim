using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using static MapGenerator;

[System.Serializable]
public class MapObject
{
    [Header("Nodes")]
    [SerializeField]
    private MapNode[] mapNodes;

    [Header("Map Information")]
    [SerializeField]
    private GridType tileGridType;
    [SerializeField]
    private GridType mapGridType;
    [SerializeField]
    private int mapWidth;
    [SerializeField]
    private int mapHeight;
    [SerializeField]
    private float XTileOffset;
    [SerializeField]
    private float YTileOffset;

    #region Map LifeCycle
    public MapObject(GridType tileGridType = GridType.HEX, GridType mapGridType = GridType.SQUARE, int mapWidth = 10, int mapHeight = 10, float XTileOffset = 1.0f, float YTileOffset = 1.0f)
    {
        mapNodes = null;

        this.tileGridType = tileGridType;
        this.mapGridType = mapGridType;

        this.mapWidth = mapWidth;
        this.mapHeight = mapHeight;

        this.XTileOffset = XTileOffset;
        this.YTileOffset = YTileOffset;
    }

    #region Map Node Generation Functions
    public void GenerateMapNodes()
    {
        if(mapNodes != null)
        {
            DestroyMap();
        }

        switch(mapGridType)
        {
            case GridType.SQUARE:
                GenerateSquareMap();
                break;
            case GridType.HEX:
                GenerateHexMap();
                break;
        }

        OnMapGenerationCompleteEvent?.Invoke();

        OnMapCompletedEvent?.Invoke();
        return;
    }
    private void GenerateSquareMap()
    {
        mapNodes = new MapNode[mapWidth * mapHeight];

        Vector3 OffSetStart = new Vector3(XTileOffset * mapWidth / 2.0f, 0.0f, YTileOffset * mapHeight / 2.0f);

        float xPos = 0.0f;
        float yPos = 0.0f;

        int itt = 0;

        for(int y = 0; y < mapHeight; y++)
        {
            for(int x = 0; x < mapWidth; x++)
            {

                MapNode node = new MapNode(tileGridType, new Vector3(xPos, 0.0f, yPos) - OffSetStart);

                mapNodes[itt] = node;

                xPos += XTileOffset;
                itt++;
            }

            xPos = 0.0f;
            if(y % 2 == 0)
            {
                xPos += XTileOffset / 2.0f;
            }
            yPos += YTileOffset;
        }
    }
    private void GenerateHexMap()
    {
        mapNodes = new MapNode[0];
    }

    #endregion

    #region Map Node Destruction Functions
    public void DestroyMapNodes()
    {
        for(int itt = 0; itt < mapNodes.Length; itt++)
        {
            mapNodes[itt].DestroyNode();
            mapNodes[itt] = null;
        }

        mapNodes = null;

        OnMapDestructionEvent?.Invoke();
    }

    #endregion

    #region Map Generation Functions
    public void RandomizeMap()
    {
        foreach(MapNode node in mapNodes)
        {
            int random = UnityEngine.Random.Range(0, Enum.GetValues(typeof(TileType)).Length);

            node.SetTileType((TileType)random);
        }
    }

    #endregion

    #endregion

    #region Map Data Functions
    public GridType GetMapTileGridType()
    {
        return tileGridType;
    }
    public GridType GetMapGridType()
    {
        return mapGridType;
    }
    public int GetMapWidth()
    {
        return mapWidth;
    }
    public int GetMapHeight()
    {
        return mapHeight;
    }
    public int GetNodeCount()
    {
        int count = 0;
        
        foreach(MapNode node in mapNodes)
        {
            if (node == null)
                continue;

            count++;
        }

        return count;
    }
    public MapNode GetMapNode(int index)
    {
        return mapNodes[index];
    }
    public int GetNumberOfNodes()
    {
        return mapNodes.Length;
    }
    #endregion
}
