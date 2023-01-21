using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileAssets
{
    private static TileAssets instance;

    private List<GameObject> tiles;
    private const string tilePath = "Prefabs/Tiles/";
    public static TileAssets GetInstance()
    {
        if (instance == null)
        {
            instance = new TileAssets();
        }

        return instance;
    }
    private TileAssets()
    {
        LoadTileAssetss();
    }
    private void LoadTileAssetss()
    {
        int count = 0;
        tiles = new List<GameObject>();

        foreach(string tileName in Enum.GetNames(typeof(TileType)))
        {
            GameObject temp = Resources.Load<GameObject>(tilePath + tileName);

            if(temp == null)
            {
                Debug.LogError("ERROR - Could not load tile Asset " + tileName);
                tiles.Add(null);
                continue;
            }

            count++;
            tiles.Add(temp);
        }

        Debug.Log("Loaded a total of " + count + " assets out of " + tiles.Count);
    }
    public GameObject GetTileAsset(TileType type)
    {
        if(tiles.Count < (int)type)
        {
            Debug.LogError("ERROR - Tile Asset List is smaller than given index");
            return null;
        }

        return tiles[(int)type];
    }

}
