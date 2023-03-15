using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class POIDataBase
{
    public static Dictionary<POIModelList, GameObject> poiModelDict = null;

    #region Init Functions
    public static void OutSideInit()
    {
        CheckInit();

        return;
    }
    private static void CheckInit()
    {
        if(poiModelDict == null)
        {
            Init();
        }

        return;
    }
    private static void Init()
    {
        SetUpModelDict();

        Debug.Log("POI Model Data Base init");

        return;
    }
    private static void SetUpModelDict()
    {
        foreach(int modelID in Enum.GetValues(typeof(POIModelList)))
        {
            if((POIModelList)modelID == POIModelList.NULL)
            {
                continue;
            }

            GameObject model = Resources.Load<GameObject>("Prefabs/Models/POI/" + ((POIModelList)modelID).ToString());

            if(model == null)
            {
                Debug.Log("ERROR - Couldn't load model for " + (POIModelList)modelID);
                continue;
            }

            poiModelDict.Add((POIModelList)modelID, model);
        }

        return;
    }
    #endregion

    #region Model
    public static GameObject GetModel(POIModelList modelID)
    {
        if(modelID == POIModelList.NULL)
        {
            Debug.LogError("ERROR - Can not get a model for NULL POI");
            return null;
        }

        if(!poiModelDict.ContainsKey(modelID))
        {
            Debug.LogError("ERROR - Data Base doesn't have a model for given key");
            return null;
        }

        return poiModelDict[modelID];
    }
    #endregion
}
