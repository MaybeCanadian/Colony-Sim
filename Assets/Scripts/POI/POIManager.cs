using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class POIManager
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
    public static void AddPOIAtLocation(Vector3 worldLocation)
    {
        //need to find grid pos of that to assign the conencted gridpos

        return;
    }
    public static void AddPOIAtGridPos(Vector2Int gridPos)
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
    #endregion
}
