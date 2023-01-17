using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapVisualsController : MonoBehaviour
{
    public static MapVisualsController instance;

    public GameObject mapTileParent = null;
    public GameObject mapObjectParent = null;

    public List<VisualNode> visualNodes;
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

        visualNodes = new List<VisualNode>();
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

        foreach(VisualNode node in visualNodes)
        {
            node.DestroyNode();
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
}
