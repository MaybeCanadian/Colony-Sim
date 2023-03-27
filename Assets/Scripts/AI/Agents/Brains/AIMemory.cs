using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMemory
{
    #region Event Dispatchers
    public delegate void POILocationRememberedEvent(SavedOPOIInformation info);
    public POILocationRememberedEvent OnPOILocationRemebered;

    public delegate void POILocationSavedToMemoryEvent(SavedOPOIInformation info);
    public POILocationSavedToMemoryEvent OnPOILocationSavedToMemory;

    public delegate void POILocationForgotEvent(SavedOPOIInformation info);
    public POILocationForgotEvent OnPOILocationForgot;
    #endregion

    #region Variables
    public List<SavedOPOIInformation> rememberedPOIs = null;
    public int maxNumberofSavedItems = 1;
    #endregion

    #region Init
    public AIMemory(int maxMemory = 1)
    {
        rememberedPOIs = new List<SavedOPOIInformation>();
        maxNumberofSavedItems = maxMemory;
    }
    #endregion

    #region Remembering
    //public bool SavePOIInMemory(POI poiToSave, float importance = 0.0f, bool replaceAny = false)
    //{
    //    SavedOPOIInformation info = new SavedOPOIInformation(poiToSave, importance); 

    //    if(CheckForMemorySpace())
    //    {
    //        rememberedPOIs.Add(info);
    //        OnPOILocationRemebered?.Invoke(info);
    //        return true;
    //    }

    //    return TryReplaceMemory(info, replaceAny);
    //}
    //public List<POI> RememberPOIsWithInteractable(POICategories categoryFilter)
    //{
    //    List<POI> rememberedList = new List<POI>();

    //    foreach(SavedOPOIInformation info in rememberedPOIs)
    //    {
            
    //    }

    //    return rememberedList;
    //}
    private bool CheckForMemorySpace()
    {
        if(rememberedPOIs.Count > maxNumberofSavedItems)
        {
            return false;
        }

        return true;
    }
    private bool TryReplaceMemory(SavedOPOIInformation toReplace, bool replaceAny)
    {
        int index = -1;
        float lowestImportance = toReplace.importanceValue;

        for (int i = 0; i < rememberedPOIs.Count; i++)
        {
            if (rememberedPOIs[i].importanceValue < lowestImportance)
            {
                index = i;
                lowestImportance = rememberedPOIs[i].importanceValue;

                if(replaceAny == true)
                {
                    break;
                }

                continue;
            }
        }

        if(index == -1)
        {
            return false;
        }

        OnPOILocationForgot?.Invoke(rememberedPOIs[index]);
        rememberedPOIs[index] = toReplace;
        OnPOILocationSavedToMemory?.Invoke(toReplace);

        return true;
    }
    #endregion
}

[System.Serializable]
public class SavedOPOIInformation
{
    //public POI poi;
    public float importanceValue = 0.0f;

    //public SavedOPOIInformation(POI toSave, float importtance = 0.0f)
    //{
    //    poi = toSave;
    //    importanceValue = importtance;
    //}
}
