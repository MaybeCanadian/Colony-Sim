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

    /// <summary>
    /// Returns the need Value that is currently at the lowest in terms of percentage.
    /// </summary>
    /// <returns></returns>
    public NeedsTypes FindCurrentLowestNeed()
    {
        NeedsTypes lowest = NeedsTypes.Health;
        float lowestValue = statsPairDict[NeedsTypes.Health].GetCurrent();

        foreach(int needType in Enum.GetValues(typeof(NeedsTypes)))
        {
            if(!statsPairDict.ContainsKey((NeedsTypes)needType))
            {
                continue;
            }

            if (statsPairDict[(NeedsTypes)needType].GetCurrent() < lowestValue)
            {
                lowest = (NeedsTypes)needType;
                lowestValue = statsPairDict[(NeedsTypes)needType].GetCurrent();
                continue;
            }
        }

        return lowest;
    }

    /// <summary>
    /// Returns all needs currently below the given percentage. Value is percent out of 100.
    /// </summary>
    /// <param name="percentage"></param>
    /// <returns></returns>
    public List<NeedsTypes> GetNeedsBelowPercentValue(float percentage)
    {
        List<NeedsTypes> values = new List<NeedsTypes>();

        percentage = percentage / 100.0f;

        foreach(int needType in Enum.GetValues(typeof(NeedsTypes)))
        {
            if(!statsPairDict.ContainsKey((NeedsTypes)needType))
            {
                continue;
            }

            if (statsPairDict[(NeedsTypes)needType].GetPercentage() < percentage)
            {
                values.Add((NeedsTypes)needType);
                continue;
            }
        }

        return values;
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
    #region Member Variables
    private float Max;
    private float Current;
    #endregion

    #region Init Functions
    public StatPairing(float Max)
    {
        this.Max = Max;
        this.Current = Max;

        ValidateValues();

        return;
    }
    private void ValidateValues()
    {
        if(Max < 0)
        {
            Debug.LogError("ERROR - Needs Value initialized with a max below zero");
            Max = 0;
            Current = 0;
        }

        return;
    }
    #endregion

    #region Value Control Functions
    public void ChangeValueAmount(float amount)
    {
        Current += amount;

        if(Current > Max)
        {
            Current = Max;
            return;
        }

        if(Current < 0)
        {
            Current = 0;
            return;
        }
    }
    public void ChangeMaxTo(float value)
    {
        if(value < 0)
        {
            Debug.LogError("ERROR - Max value cannot be less than zero");
            return;
        }

        Max = value;
    } 
    #endregion

    #region Data Functions
    public float GetCurrent()
    {
        return Current;
    }
    public float GetMax()
    {
        return Max;
    }
    public float GetPercentage()
    {
        if(Max <= 0)
        {
            return 0;
        }

        return Current / Max;
    }
    #endregion
}
#endregion
