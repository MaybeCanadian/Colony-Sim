using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MapResourceController
{
    #region Event Dispatchers
    public delegate void ResourceCreatedEvent(MapResource resource);
    public static ResourceCreatedEvent OnResourceCreated;

    public delegate void ResourceDestroyedEvent(MapResource resource);
    public static ResourceDestroyedEvent OnResourceDestroyed;

    public delegate void AllResourcesDestroyEvent();
    public static AllResourcesDestroyEvent OnAllResourcesDestroy;
    #endregion

    public static List<MapResource> mapResources = null;

    #region Init Fucntions
    public static void OutSideInit()
    {
        CheckInit();
    }
    private static void CheckInit()
    {
        if(mapResources == null)
        {
            Init();
        }
    }
    private static void Init()
    {
        SetUpMapResourceList();
    }
    private static void SetUpMapResourceList()
    {
        if(mapResources != null)
        {
            Debug.LogError("ERROR - Could not set up map resource list as list already exists.");
            return;
        }

        mapResources = new List<MapResource>();
    }
    #endregion

    #region Resource Control
    public static void CreateResourceAtNode(MapNode node)
    {
        CheckInit();

        MapResource resource = new MapResource(node);

        mapResources.Add(resource);

        OnResourceCreated?.Invoke(resource);
    }
    public static void CreateResourceAtGridPos(Vector2Int gridPos)
    {

    }
    public static void RemoveAllResources()
    {
        foreach(MapResource resource in mapResources)
        {
            resource.DestroyResource();
        }

        OnAllResourcesDestroy?.Invoke();
    }
    #endregion
}
