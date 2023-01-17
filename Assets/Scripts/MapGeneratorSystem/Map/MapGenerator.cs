using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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

    #endregion

    #region Map Lifecycle

    #region Map Generation
    public static void GenerateMap(GridType gridType, int mapWdith, int mapHeight)
    {
        if (map != null)
        {
            DestroyMap();
        }

        map = new MapObject(gridType, mapWdith, mapHeight);

        switch (gridType) 
        {
            case GridType.HEX:
                GenerateHexMap();
                break;
            case GridType.SQUARE:
                GenerateSqureMap();
                break;

        }

        OnMapGenerationCompleteEvent?.Invoke();

        OnMapCompletedEvent?.Invoke();
    }
    private static void GenerateHexMap()
    {

    }
    private static void GenerateSqureMap()
    {

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
}
