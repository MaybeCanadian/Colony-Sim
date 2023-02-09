using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AIPathFindingAgent
{
    AIAgent agent;
    public AIPathFindingAgent(AIAgent parentAgent)
    {
        agent = parentAgent;
    }
    public void DesttoryPathfindingAgent()
    {
        agent = null;
    }
}
