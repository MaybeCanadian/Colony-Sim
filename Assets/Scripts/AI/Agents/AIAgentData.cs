using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AIAgentData
{
    [Header("Personel Information")]
    public string agentName = "Name";
    public int agentAge = 0;

    [Header("Visual Information")]
    public BaseModelList modelToUse = BaseModelList.TestAI;

}
