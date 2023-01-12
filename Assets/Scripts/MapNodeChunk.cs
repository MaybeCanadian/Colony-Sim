using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapNodeChunk
{
    private List<MapNode> chunkNodes;



    private int chunkWidth;
    private int chunkHeight;

    private Vector3 chunkPosition;

    public MapNodeChunk(int width, int height)
    {
        chunkWidth = width;
        chunkHeight = height;

        chunkNodes = new List<MapNode>();
    }

    public void ClearChunk()
    {
        foreach(MapNode node in chunkNodes)
        {
            node.ClearNode();
        }

        chunkNodes.Clear();
    }
}
