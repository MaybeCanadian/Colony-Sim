using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapResourceVisualsController : MonoBehaviour
{
    #region Event Dispatchers

    #endregion

    static MapResourceVisualsController instance;

    List<MapResourcesVisuals> visualsList = null;
    GameObject visualsOBJ = null;

    #region Init Functions
    private void Awake()
    {
        if(instance != this && instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            Init();
        }
    }
    private void Init()
    {
        SetUpVisualsList();
        SetUpVisualsParent();
    }
    private void SetUpVisualsList()
    {
        if(visualsList != null)
        {
            Debug.LogError("ERROR - Could not set up resource visuals list as it already exists.");
            return;
        }

        visualsList= new List<MapResourcesVisuals>();
    }
    private void SetUpVisualsParent()
    {
        if(visualsOBJ != null)
        {
            Debug.LogError("ERROR - Could not set up resource visual parent as it already exists.");
            return;
        }

        visualsOBJ = new GameObject();
        visualsOBJ.name = "[RESOURCES]";
    }
    private void ConnectEvents() 
    {
        MapResourceController.OnResourceCreated += OnMapResourceCreated;
        MapResourceController.OnResourceDestroyed += OnMapResourceDestroyed;
        MapResourceController.OnAllResourcesDestroy += OnAllMapResourcesDestroyed;
    }
    private void DisconnectEvents()
    {
        MapResourceController.OnResourceCreated -= OnMapResourceCreated;
        MapResourceController.OnResourceDestroyed -= OnMapResourceDestroyed;
        MapResourceController.OnAllResourcesDestroy -= OnAllMapResourcesDestroyed;
    }
    #endregion

    #region Event Recievers
    private void OnMapResourceCreated(MapResource resource)
    {

    }
    private void OnMapResourceDestroyed(MapResource resource)
    {

    }
    private void OnAllMapResourcesDestroyed()
    {

    }
    #endregion
}
