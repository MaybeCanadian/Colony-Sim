using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    #region Members
    public static GameController instance;

    public int MapSizeX = 10;
    public int MapSizeY = 10;

    public int ChunkSizeX = 10;
    public int ChunkSizeY = 10;

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
    }
    void Start()
    {
        MapGenerator.instance.CreateMapChunks(MapSizeX, MapSizeY, ChunkSizeX, ChunkSizeY);
    }

    #endregion
}
