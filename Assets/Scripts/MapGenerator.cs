using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    //private GameObject tilePrefab;
    public static MapGenerator instance;

    public List<MapNodeChunk> mapChunks;
    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        //tilePrefab = Resources.Load<GameObject>("Prefabs/Tile");
    }

    #region Map Generation Functions
    public void GenerateMap()
    {
        mapChunks = new List<MapNodeChunk>();

        ClearMap();
    }

    #endregion

    #region Map Destruction Functions
    private void ClearMap()
    {
        foreach(MapNodeChunk chunk in mapChunks)
        {
            chunk.ClearChunk();
        }

        mapChunks.Clear();
    }

    #endregion
}
