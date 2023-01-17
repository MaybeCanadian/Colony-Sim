using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapChunk
{
    [Header("Chunk Data")]
    [SerializeField]
    private Vector3 chunkPosition;
    [SerializeField]
    private int chunkWidth;
    [SerializeField]
    private int chunkHeight;

    MapObject map;

    [Header("Nodes in Chunk")]
    [SerializeField]
    private MapNode[,] chunkNodes;

    #region Node LifeCycle
    public MapChunk(MapObject map, int chunkWidth, int chunkHeight)
    {
        chunkNodes = new MapNode[chunkWidth, chunkHeight];

        this.map = map;
        this.chunkWidth = chunkWidth;
        this.chunkHeight = chunkHeight;
    }
    public void GenerateSquareChunk()
    {
        for(int y = 0; y < chunkHeight; y++)
        {
            for(int x = 0; x < chunkWidth; x++)
            {
                MapNode node = new MapNode(this, chunkPosition);

                chunkNodes[y, x] = node;
            }
        }
    }
    public void AddNode()
    {

    }
    public void DestroyChunk()
    {
        for(int y = 0; y < chunkHeight; y++)
        {
            for(int x = 0; x < chunkWidth; x++)
            {
                chunkNodes[x, y].DestroyNode();

                chunkNodes[x, y] = null;
            }
        }

        chunkNodes = null;
    }
    #endregion

    #region Chunk Data Functions
    public GridType GetTileGridType()
    {
        return map.GetMapTileGridType();
    }
    public GridType GetChunkGridType()
    {
        return map.GetMapChunkGridType();
    }
    public int GetNodeCount()
    {
        int count = 0;

        foreach(MapNode node in chunkNodes)
        {
            if (node == null)
                continue;

            count++;
        }

        return count;
    }
    #endregion
}
