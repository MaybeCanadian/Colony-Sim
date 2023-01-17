using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.iOS.Xcode;
using UnityEngine;

[System.Serializable]
static public class MapGenerator
{
    [SerializeField]
    static MapObject map = null;

    #region Event Dispatchers

    public delegate void MapGenerationCompleteEvent();
    public static MapGenerationCompleteEvent OnMapGenerationCompleteEvent;

    public delegate void MapLoadCompleteEvent();
    public static MapLoadCompleteEvent OnMapLoadCompleteEvent;

    public delegate void MapCompletedEvent();
    public static MapCompletedEvent OnMapCompletedEvent;

    public delegate void MapDestructionEvent();
    public static MapDestructionEvent OnMapDestructionEvent;

    public delegate void ChunkGeneratedEvent();
    public static ChunkGeneratedEvent OnChunkGeneratedEvent;

    #endregion

    #region Map Lifecycle

    #region Map Generation
    public static void GenerateMap(GridType tileGridType, GridType chunkGridType, int mapWdith, int mapHeight, int chunkWidth, int chunkHeight)
    {
        if (map != null)
        {
            DestroyMap();
        }

        map = new MapObject(tileGridType, chunkGridType, mapWdith, mapHeight, chunkWidth, chunkHeight);

        map.GenerateMapNodes();

        OnMapGenerationCompleteEvent?.Invoke();

        OnMapCompletedEvent?.Invoke();

        Debug.Log("Finished Generating Map");
    }
    #endregion

    #region Map Loading
    public static void LoadMap(string saveName)
    {
        if(map != null)
        {
            DestroyMap();
        }

        map = MapSerializer.LoadMapFromFile(saveName);

        OnMapLoadCompleteEvent?.Invoke();

        OnMapCompletedEvent?.Invoke();
    }

    #endregion

    #region Map Destruction
    public static void DestroyMap()
    {
        //map.DestroyMap();

        map = null;

        OnMapDestructionEvent?.Invoke();
    }

    #endregion

    #endregion

    #region Map Data
    public static MapObject GetMap()
    {
        return map;
    }

    #endregion
}
