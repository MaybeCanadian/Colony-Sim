using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AIAgentData
{
    [Header("Personel Information")]
    public string agentName = "Name";
    public int agentAge = 0;

    [Header("Visual Information")]
    public BaseModelList modelToUse = BaseModelList.TestAI;

    [Header("Movement Values")]
    public float speedMultiplier = 0.0f;

    public AIAgentData()
    {
        speedMultiplier = Random.Range(0.5f, 1.5f);
    }

}
