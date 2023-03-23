using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AINeedsValues
{
    #region Values
    public Dictionary<NeedsTypes, StatPairing> statsPairDict = null;

    public float defaultStartingValues = 100.0f;
    #endregion

    #region Functions
    public AINeedsValues()
    {
        SetUpStatsDict();

        return;
    }
    private void SetUpStatsDict()
    {
        statsPairDict = new Dictionary<NeedsTypes, StatPairing>();

        foreach (int needType in Enum.GetValues(typeof(NeedsTypes)))
        {
            StatPairing stats = new StatPairing(defaultStartingValues);

            statsPairDict.Add((NeedsTypes)needType, stats);
        }

        return;
    }
    public NeedsTypes FindCurrentLowestNeed()
    {
        NeedsTypes lowest = NeedsTypes.Health;
        float lowestValue = statsPairDict[NeedsTypes.Health].Current;

        foreach(int needType in Enum.GetValues(typeof(NeedsTypes)))
        {
            if(!statsPairDict.ContainsKey((NeedsTypes)needType))
            {
                continue;
            }

            if (statsPairDict[(NeedsTypes)needType].Current < lowestValue)
            {
                lowest = (NeedsTypes)needType;
                lowestValue = statsPairDict[(NeedsTypes)needType].Current;
                continue;
            }
        }

        return lowest;
    }
    #endregion
}

#region Needs Extras
[System.Serializable]
public enum NeedsTypes
{
    Health,
    Water,
    Hunger
}

[System.Serializable]
public class StatPairing
{
    public float Max;
    public float Current;

    public StatPairing(float Max)
    {
        this.Max = Max;
        this.Current = Max;

        return;
    }
}
#endregion
