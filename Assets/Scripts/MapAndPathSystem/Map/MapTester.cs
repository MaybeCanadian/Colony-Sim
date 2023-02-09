using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MapTester : MonoBehaviour
{
    MapObject map = null;

    #region Event Connections
    private void OnEnable()
    {
        MapGenerator.OnMapCompletedEvent += OnMapCompleted;
    }
    private void OnDisable()
    {
        MapGenerator.OnMapCompletedEvent -= OnMapCompleted;
    }
    #endregion

    #region Event Receivers
    private void OnMapCompleted()
    {
        map = MapGenerator.GetMap();
    }
    #endregion

    #region Debug Inputs
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
    #endregion
}
