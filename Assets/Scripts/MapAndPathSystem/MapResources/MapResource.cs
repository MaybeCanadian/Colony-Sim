using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapResource
{
    #region Event Dispatchers
    public delegate void ResourceDestroyEvent();
    public ResourceDestroyEvent OnResourceDestroy;
    #endregion

    public MapNode connectedMapNode = null;

    public MapResource(MapNode connected)
    {
        connectedMapNode = connected;
    }

    public void DestroyResource()
    {
        connectedMapNode = null;

        OnResourceDestroy?.Invoke();
    }
}
