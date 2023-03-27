using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InteractableManager
{
    #region Event Dispatcher
    public delegate void POIAddedEvent(POI addedPOI);
    public static POIAddedEvent OnPOIAdded;

    public delegate void POIRemovedEvent(POI removedPOI);
    public static POIRemovedEvent OnPOIRemoved;

    public delegate void RemoveAllPOIsEvent();
    public static RemoveAllPOIsEvent OnRemoveAllPOIs;
    #endregion

    #region Member Variables
    private static List<POI> poiList = null;
    #endregion

    #region
    public static void OutSideInit()
    {
        CheckInit();

        return;
    }
    private static void CheckInit()
    {
        if(poiList == null)
        {
            Init();
        }

        return;
    }
    private static void Init()
    {
        poiList = new List<POI>();

        Debug.Log("POI manager init");

        return;
    }
    #endregion

    #region POI Control
    private static void AddPOIAtLocation(Vector3 worldLocation)
    {
        //need to find grid pos of that to assign the conencted gridpos

        return;
    }
    private static void AddPOIAtGridPos(Vector2Int gridPos)
    {
        POI newPOI = new POI(gridPos);

        poiList.Add(newPOI);

        OnPOIAdded?.Invoke(newPOI);

        return;
    }
    public static void RemoveAllPOI()
    {
        foreach(POI poi in poiList)
        {
            poi.RemvoePOI();
        }

        poiList.Clear();
    }
    #endregion

    #region InteractableControl
    public static void AddInteractableAtLocation(Vector3 worldPos)
    {

        return;
    }
    public static void AddInteractableAtGridPos(Vector2Int gridPos)
    {
        return;
    }
    public static void RemoveAllInteracablesInPOIAtLocation(Vector3 worldPos)
    {
        return;
    }
    public static void RemoveAllInteractablesInPOIAtGridPos(Vector2Int gridPos)
    {
        return;
    }
    #endregion

    #region POI Locate
    public static List<POI> FindPOIsInRangeofWorldPos (Vector3 worldPos, float range, POICategories POIFilter = POICategories.Default)
    {
        List<POI> nearList = new List<POI>();

        //need to add category check

        foreach(POI poi in poiList)
        {
            if(poi.FindDistanceToWorldPos(worldPos) < range)
            {
                nearList.Add(poi);
            }
        }

        return nearList;
    }
    public static POI FindPOIContainingWorldPos(Vector3 worldPos)
    {
        MapObject map = MapGenerator.GetMap();

        float tileRange = map.GetTileMaxRange();

        POI closestPOI = null;
        float closestDistance = Mathf.Infinity;

        foreach(POI poi in poiList)
        {
            float distance = (poi.POIworldPos - worldPos).magnitude;

            if(distance > tileRange)
            {
                continue;
            }
            
            if(distance < closestDistance)
            {
                closestPOI = poi;
                closestDistance = distance;
            }

            continue;
        }

        return closestPOI;
    }
    public static POI FindPOIAtGridPos(Vector2Int gridPos)
    {
        POI mapPOI = null;

        foreach(POI poi in poiList)
        {
            if(poi.connectedGridPos == gridPos)
            {
                mapPOI = poi;
                break;
            }
        }

        return mapPOI;
    }
    #endregion
}
