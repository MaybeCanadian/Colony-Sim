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
    private MapNode[,] mapNodes;

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

    #region Map Generation Functions
    public void GenerateMapNodes()
    {
        if(mapNodes != null)
        {
            DestroyMap();
        }

        mapNodes = new MapNode[mapWidth, mapHeight];

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
        float z = 0.0f;

        float xPos = 0.0f;
        float yPos = 0.0f;

        for(int y = 0; y < mapHeight; y++)
        {
            for(int x = 0; x < mapWidth; x++)
            {

                MapNode node = new MapNode(tileGridType, new Vector3(xPos, 0.0f, yPos));

                mapNodes[x, y] = node;

                xPos += XTileOffset;
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

    }

    #endregion

    #region Map Destruction Functions
    public void DestroyMap()
    {
        for(int y = 0; y < mapHeight; y++)
        {
            for(int x = 0; x < mapWidth; x++)
            {
                mapNodes[x, y].DestroyNode();

                mapNodes[x, y] = null;
            }
        }

        mapNodes = null;

        OnMapDestructionEvent?.Invoke();
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
    public MapNode GetMapNode(int x, int y)
    {
        return mapNodes[x, y];
    }
    #endregion
}
