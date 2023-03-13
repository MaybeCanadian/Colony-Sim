using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AIAgent
{
    #region Event Dispatchers
    //This event is for the visual agent to disconnect
    public delegate void AgentDestroyEvent();
    public AgentDestroyEvent OnAgentDestroy;
    #endregion

    [Header("Agent Information")]
    public int agentID;
    public AIAgentData agentData;

    [Header("Pathfinding")]
    public AIPathFindingAgent pathfindingAgent;

    public AIAgent(int id, Vector2Int startingGridPos, AIAgentData data = null)
    {
        pathfindingAgent = new AIPathFindingAgent(startingGridPos);

        agentData = data;

        if (data == null)
        {
            agentData = new AIAgentData();
        }

        ConnectEvents();
    }

    #region Event Functions
    private void ConnectEvents()
    {
        GameController.OnGameUpdate += Update;
        GameController.OnGameFixedUpdate += FixedUpdate;
        GameController.OnGameLateUpdate += LateUpdate;

        AIAgentManager.OnRemoveAllAgents += DestroyAgent;
    }
    private void DisconnectEvents()
    {
        GameController.OnGameUpdate -= Update;
        GameController.OnGameFixedUpdate -= FixedUpdate;
        GameController.OnGameLateUpdate -= LateUpdate;

        AIAgentManager.OnRemoveAllAgents -= DestroyAgent;
    }
    #endregion

    #region Update Functions
    public void Update(float deltaTime)
    {
        pathfindingAgent.PathfindingUpdate(deltaTime);
    }
    public void FixedUpdate(float fixedDeltaTime)
    {
        pathfindingAgent.PathfindingFixedUpdate(fixedDeltaTime);
    }
    public void LateUpdate(float deltaTime) 
    {
        pathfindingAgent.PathfindingLateUpdate(deltaTime);
    }
    #endregion

    #region Destructor Functions
    public void DestroyAgent()
    {
        DisconnectEvents();

        pathfindingAgent.DestoryPathfindingAgent();

        pathfindingAgent = null;

        OnAgentDestroy?.Invoke();
    }
    #endregion

    #region Debug
    public void PathToRandomLocation()
    {
        pathfindingAgent.PathToRandomLocation();
    }
    #endregion
}
