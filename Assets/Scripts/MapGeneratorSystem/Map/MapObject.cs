using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MapGenerator;

[System.Serializable]
public class MapObject
{
    [SerializeField]
    private List<MapChunk> mapChunks;
    [SerializeField]
    private GridType gridType;
    public MapObject(GridType gridType, int mapWeight, int mapHeight)
    {
        mapChunks = new List<MapChunk>();
    }
    public void AddChunk(Vector3 chunkPosition)
    {

    }
    public void DestroyMap()
    {

    }
}
