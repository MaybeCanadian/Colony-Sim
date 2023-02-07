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

        MapVisualsController.instance.CreateMapVisuals();
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

    public void OnGenerateTestPath(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            PathSystem.FindPathBetweenTwoNodes(map.GetMapNode(0, 0).GetPathNode(), map.GetMapNode(6, 4).GetPathNode(), out PathRoute route);
        }
    }

}
