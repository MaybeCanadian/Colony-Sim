using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMemory
{
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
    public void SavePOIInMemory(POI poiToSave, float importance = 0.0f, bool replaceAny = false)
    {
        SavedOPOIInformation info = new SavedOPOIInformation(poiToSave, importance); 

        if(CheckForMemorySpace())
        {
            rememberedPOIs.Add(info);
            return;
        }

        TryReplaceMemory(info, replaceAny);
    }
    public POI RememberPOIWithInteractable()
    {
        return null;
    }
    private bool CheckForMemorySpace()
    {
        if(rememberedPOIs.Count > maxNumberofSavedItems)
        {
            return false;
        }

        return true;
    }
    private void TryReplaceMemory(SavedOPOIInformation toReplace, bool replaceAny)
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
            return;
        }

        rememberedPOIs[index] = toReplace;

        return;
    }
    #endregion
}

[System.Serializable]
public class SavedOPOIInformation
{
    public POI poi;
    public float importanceValue = 0.0f;

    public SavedOPOIInformation(POI toSave, float importtance = 0.0f)
    {
        poi = toSave;
        importanceValue = importtance;
    }
}
