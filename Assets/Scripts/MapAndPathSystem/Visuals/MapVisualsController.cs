using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapVisualsController : MonoBehaviour
{
    public static MapVisualsController instance;

    [Header("Parents")]
    [SerializeField]
    private GameObject mapTileParent = null;
    [SerializeField]
    private GameObject mapObjectParent = null;
    [Header("Nodes")]
    [SerializeField]
    private TileType deafultTileType = TileType.GRASS_EMPTY;
    
    private void Awake()
    {
        if(instance != this && instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    #region Map Visuals Creation
    public void CreateMapVisuals()
    {
        CreateTileVisuals();
        CreateObjectVisuals();
    }
    private void CreateTileVisuals()
    {
        if (mapTileParent != null)
        {
            DestroyMapTileVisuals();
        }

        mapTileParent = new GameObject();
        mapTileParent.transform.SetParent(transform);
        mapTileParent.name = "Map Tile Parent";

        MapObject map = MapGenerator.GetMap();

        int nodesX = map.GetMapWidth();
        int nodesY = map.GetMapHeight();

        for(int x = 0; x < nodesX; x++)
        {
            for(int y = 0; y < nodesY; y++)
            {
                MapNode mapNode = map.GetMapNode(x, y);

                if (mapNode == null)
                {
                    Debug.LogError("ERROR - Map Node at given location is null.");
                    continue;
                }

                TileType tileType = mapNode.GetTileType();

                GameObject visualNodeObject = new GameObject();

                visualNodeObject.transform.SetParent(mapTileParent.transform);

                VisualNode visualNode = visualNodeObject.AddComponent<VisualNode>();
                visualNode.ConnectMapNode(mapNode);
                visualNode.ShowNodeVisuals();

                visualNodeObject.name = "TILE number " + x + ", " + y;
            }
        }

    }
    private void CreateObjectVisuals()
    {
        if(mapObjectParent != null)
        {
            DestroyMapObjectVisuals();
        }
    }

    #endregion

    #region Map Visuals Destruction
    public void DestroyMapVisuals()
    {
        DestroyMapTileVisuals();
        DestroyMapObjectVisuals();
    }
    private void DestroyMapTileVisuals()
    {
        if(mapTileParent == null)
        {
            return;
        }

        Destroy(mapTileParent);

        mapTileParent = null;
    }
    private void DestroyMapObjectVisuals()
    {
        if(mapObjectParent == null)
        {
            return;
        }

        Destroy(mapObjectParent);

        mapObjectParent = null;
    }

    #endregion

    #region Map Visuals Data Function
    public TileType GetDefaultTileType()
    {
        return deafultTileType;
    }

    #endregion
}
