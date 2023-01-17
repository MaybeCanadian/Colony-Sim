using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MapGenerator;

[System.Serializable]
public class MapObject
{
    [SerializeField]
    private MapChunk[,] mapChunks;
    [SerializeField]
    private GridType tileGridType;
    [SerializeField]
    private GridType chunkGridType;

    [SerializeField]
    private int mapWidth;
    [SerializeField]
    private int mapHeight;
    [SerializeField]
    private int chunkWidth;
    [SerializeField]
    private int chunkHeight;

    #region Map LifeCycle
    public MapObject(GridType tileGridType, GridType chunkGridType, int mapWidth, int mapHeight, int chunkWdith, int chunkHeight)
    {
        mapChunks = new MapChunk[mapWidth, mapHeight];

        this.tileGridType = tileGridType;
        this.chunkGridType = chunkGridType;

        this.mapWidth = mapWidth;
        this.mapHeight = mapHeight;
        this.chunkWidth = chunkWdith;
        this.chunkHeight = chunkHeight;
    }
    public void GenerateMapNodes()
    {
        switch(chunkGridType)
        {
            case GridType.HEX:
                GenerateHexChunks();
                break;
            case GridType.SQUARE:
                GenerateSquareChunks();
                break;
        }
    }
    public void GenerateSquareChunks()
    {
        for(int y = 0; y < mapHeight; y++)
        {
            for(int x = 0; x < mapWidth; x++)
            {
                MapChunk chunk = new MapChunk(this, chunkWidth, chunkHeight);

                mapChunks[x,y] = chunk;

                chunk.GenerateSquareChunk();
            }
        }
    }
    public void GenerateHexChunks()
    {
        //TO DO - add hex chunk code
    }
    public void AddChunk(MapChunk chunk, int x, int y)
    {
        if(x < 0 || x > mapWidth || y < 0 || y > mapHeight)
        {
            Debug.LogError("ERROR - Chunk add Index out of range.");
            return;
        }

        mapChunks[x, y] = chunk;

        return;
    }
    public void DestroyMap()
    {
        for(int y = 0; y < mapHeight; y++)
        {
            for(int x = 0; x < mapWidth; x++)
            {
                mapChunks[x, y].DestroyChunk();

                mapChunks[x, y] = null;
            }
        }

        mapChunks = null;
    }
    #endregion

    #region Map Data Functions
    public GridType GetMapTileGridType()
    {
        return tileGridType;
    }
    public GridType GetMapChunkGridType()
    {
        return chunkGridType;
    }
    public int GetMapWidth()
    {
        return mapWidth;
    }
    public int GetMapHeight()
    {
        return mapHeight;
    }
    public int GetChunkWidth()
    {
        return chunkWidth;
    }
    public int GetChunkHeight() 
    {
        return chunkHeight;
    }
    public int GetNodeCount()
    {
        int count = 0;
        
        foreach(MapChunk chunk in mapChunks)
        {
            if (chunk == null)
                continue;

            count += chunk.GetNodeCount();
        }

        return count;
    }
    public int GetCountCount()
    {
        int count = 0;

        foreach(MapChunk chunk in mapChunks)
        {
            if (chunk == null)
                continue;

            count++;
        }

        return count;
    }
    #endregion
}
