using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    #region Members
    //private GameObject tilePrefab;
    public static MapGenerator instance;

    [SerializeField]
    private List<MapNodeChunk> mapChunks;

    #endregion

    #region Methods
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

    #region Chunk LifeCycle Functions
    public void CreateMapChunks(int numberOfChunksX, int numberOfChunksY, int chunkSizeX = 10, int chunkSizeY = 10)
    {
        mapChunks = new List<MapNodeChunk>();

        for(int y = 0; y < numberOfChunksY; y++)
        {
            for(int x = 0; x < numberOfChunksX; x++)
            {
                MapNodeChunk newChunk = new MapNodeChunk(chunkSizeX, chunkSizeY);

                mapChunks.Add(newChunk);
            }
        }

        DisplayTotalTileNumber();
    }
    private void DestroyMapChunks() //This functions Physically removes the map chunks from existance, will need to remake them after
    {
        foreach (MapNodeChunk chunk in mapChunks)
        {
            chunk.ClearChunk();
        }

        mapChunks.Clear();
    }

    #endregion

    #region Map LifeCycle Functions

    public void GenerateMap(int chunksX, int chunksY)
    {
        
    }

    #endregion

    #region Map Info Functions

    public void DisplayTotalTileNumber()
    {
        int total = 0;

        foreach(MapNodeChunk chunk in mapChunks)
        {
            total += chunk.NumberOfNodesInChunk();
        }

        Debug.Log("The number of total nodes is " + total);

    }

    #endregion

    #endregion
}
