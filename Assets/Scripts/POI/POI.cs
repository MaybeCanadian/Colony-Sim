using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POI
{
    #region Event Dispatchers
    public delegate void POIRemoveEvent();
    public POIRemoveEvent OnPOIRemove;
    #endregion

    public Vector2Int connectedGridPos = Vector2Int.zero;
    public Vector3 POIworldPos = Vector3Int.zero;

    public POICategories category = POICategories.Default;

    #region Init Functions
    public POI(Vector2Int gridPOs)
    {
        connectedGridPos = gridPOs;

        //find connected worldpos
    }
    public POI(Vector3 worldPos)
    {
        POIworldPos = worldPos;

        //find connected gridpos
    }
    public POI(Vector2Int gridPos, Vector3 worldPos)
    {
        connectedGridPos = gridPos;
        POIworldPos = worldPos;
    }
    #endregion

    #region Lifecycle
    public void RemvoePOI()
    {


        OnPOIRemove?.Invoke();
    }
    #endregion

    #region Distance
    public float FindDistanceToWorldPos(Vector3 worldPos)
    {
        float dist;

        dist = (worldPos - POIworldPos).magnitude;

        return dist;
    }
    #endregion
}
