using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CharacterModelDataBase
{
    private static Dictionary<BaseModelList, GameObject> modelDict = null;

    #region Init Functions
    public static void OutSideInit()
    {
        CheckInit();
    }
    private static void CheckInit()
    {
        if(modelDict == null)
        {
            Init();
        }
    }
    private static void Init()
    {
        SetUpModelDictionary();

        Debug.Log("Model Data Base Init");
    }
    private static void SetUpModelDictionary()
    {
        modelDict = new Dictionary<BaseModelList, GameObject>();

        foreach(int modelID in Enum.GetValues(typeof(BaseModelList)))
        {
            if((BaseModelList)modelID == BaseModelList.NULL)
            {
                continue;
            }

            GameObject model = Resources.Load<GameObject>("Prefabs/Models/" + ((BaseModelList)modelID).ToString());

            if(model == null)
            {
                Debug.LogError("ERROR - Could not load the model for " + (BaseModelList)modelID);
                continue;
            }

            modelDict.Add((BaseModelList)modelID, model);
        }

        return;
    }
    #endregion

    #region Model Getting
    public static GameObject GetModel(BaseModelList modelID)
    {
        CheckInit();

        if(modelID == BaseModelList.NULL)
        {
            Debug.LogError("ERROR - Can not get a model for NULL");
            return null;
        }

        if(modelDict.ContainsKey(modelID) == false)
        {
            Debug.LogError("ERROR - Model dictionary does not have a model for given key");
            return null;
        }

        return modelDict[modelID];
    }
    #endregion
}
