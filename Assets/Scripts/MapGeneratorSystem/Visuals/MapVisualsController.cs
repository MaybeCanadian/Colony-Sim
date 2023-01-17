using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapVisualsController : MonoBehaviour
{
    public static MapVisualsController instance;

    private GameObject TileParent = null;

    [Header("Default Tiles")]
    [SerializeField]
    private GameObject defaultHexTile;
    [SerializeField]
    private GameObject defaultSquareTile;
    Dictionary<GridTypes, GameObject> defaultTiles;

    private void Awake()
    {
        if (instance != this && instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;

            CreateDefaultDictionry();
        }
    }
    private void CreateDefaultDictionry()
    {
        defaultTiles= new Dictionary<GridTypes, GameObject>();
        defaultTiles.Add(GridTypes.HEX, defaultHexTile);
        defaultTiles.Add(GridTypes.SQUARE, defaultSquareTile);
    }
    public void GenerateMapVisuals()
    {
        if(TileParent != null)
        {
            DestroyMapVisuals();
        }

        TileParent = new GameObject();
        TileParent.transform.SetParent(transform);
        TileParent.name = "Tile Parent";

        for(int i = 0; i < 10; i++)
        {
            CreateNewTileObject(GridTypes.HEX);
        }

    } 
    public void DestroyMapVisuals()
    {
        Destroy(TileParent);
    }
    public GameObject CreateNewTileObject(GridTypes type)
    {
        GameObject Tile = Instantiate(defaultTiles[type]);
        Tile.transform.SetParent(TileParent.transform);
        Tile.name = "Tile";

        return Tile;
    }
    private void Update()
    {
        CheckDebugInputs();
    }
    private void CheckDebugInputs()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            GenerateMapVisuals();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            DestroyMapVisuals();
        }
    }
}
