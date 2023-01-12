using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapNodeChunk
{
    #region Members

    [SerializeField]
    private List<MapNode> chunkNodes;

    private int chunkWidth;
    private int chunkHeight;

    private float offsetX;
    private float offsetY;

    private Vector3 chunkPosition;

    #endregion

    #region Methods
    public MapNodeChunk(int width, int height, float nodeOffsetX = 1, float nodeOffsetY = 1)
    {
        chunkWidth = width;
        chunkHeight = height;

        offsetX = nodeOffsetX;
        offsetY = nodeOffsetY;

        GenerateNodesInChunk();
    }

    #region Chunk LifeCycle Functions
    private void GenerateNodesInChunk()
    {
        chunkNodes = new List<MapNode>();

        for (int y = 0; y < chunkHeight; y++)
        {
            for(int x = 0; x < chunkWidth; x++)
            {
                MapNode newNode = new MapNode(chunkPosition + new Vector3(offsetX * x, offsetY * y, 0));
                chunkNodes.Add(newNode);
            }
        }
    }
    public void ClearChunk()
    {
        foreach(MapNode node in chunkNodes)
        {
            node.ClearNode();
        }

        chunkNodes.Clear();
    }

    #endregion

    #region Chunk Info Functions
    public int NumberOfNodesInChunk()
    {
        return chunkNodes.Count;
    }

    #endregion

    #endregion
}
