using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MapTester : MonoBehaviour
{
    [SerializeField]
    private MapObject map = null;

    [SerializeField]
    private int mapWidth = 10;
    [SerializeField]
    private int mapHeight = 10;

    [SerializeField]
    private float XTileOffset = 1.0f;
    [SerializeField]
    private float YTileOffset = 1.0f;

    [SerializeField]
    private GridType tileGridType = GridType.HEX;
    [SerializeField]
    private GridType chunkGridType = GridType.SQUARE;
    private void Start()
    {
        MapGenerator.GenerateMap(tileGridType, chunkGridType, mapWidth, mapHeight, XTileOffset, YTileOffset);
        map = MapGenerator.GetMap();

        Debug.Log("Node count is - " + map.GetNodeCount());

        MapVisualsController.instance.CreateMapVisuals();

        map.GetMapNode(1, 1).GetPathNode().PrintNodeType();
        map.GetMapNode(1, 1).GetPathNode().PrintNeightbourPos();

        //PathingSystem.GetPathFromGridPosToGridPos(new Vector2Int(1, 1), new Vector2Int(4, 4));
    }

    public void OnMapRegenEvent(InputAction.CallbackContext context)
    {

        if (context.started)
        {
            map.RandomizeMap();
            Debug.Log("regen map");
        }
    }

    public void OnDestroyMap(InputAction.CallbackContext context) 
    {
        if (context.started)
        {
            MapVisualsController.instance.DestroyMapVisuals();
        }
    }

    public void OnRecreateMap(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            MapVisualsController.instance.CreateMapVisuals();
        }
    }

}
