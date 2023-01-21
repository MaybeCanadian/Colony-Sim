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

    public delegate void MapNodeGenerationCompleteEvent();
    public static MapNodeGenerationCompleteEvent OnMapNodeGenerationCompleteEvent;

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
    public static void GenerateMap(GridType tileGridType, GridType mapGridType, int mapWdith = 10, int mapHeight = 10, float XTileOffset = 1.0f, float YTileOffset = 1.0f)
    {
        if (map != null)
        {
            DestroyMap();
        }

        map = new MapObject(tileGridType, mapGridType, mapWdith, mapHeight, XTileOffset, YTileOffset);

        map.GenerateMapNodes();

        OnMapNodeGenerationCompleteEvent?.Invoke();

        map.RandomizeMap();

        OnMapGenerationCompleteEvent?.Invoke();

        OnMapCompletedEvent?.Invoke();
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
