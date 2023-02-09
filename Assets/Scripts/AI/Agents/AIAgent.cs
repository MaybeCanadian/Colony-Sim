using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AIAgent
{
    public AIPathFindingAgent pathfinding;
    public Vector2 currentGridPos;
    public int agentID;

    public AIAgent(int id, Vector2 startingGridPos)
    {
        agentID = id;
        currentGridPos = startingGridPos;
        pathfinding = new AIPathFindingAgent(this);

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

    }
    public void FixedUpdate(float fixedDeltaTime)
    {

    }
    public void LateUpdate(float deltaTime) 
    {

    }
    #endregion

    #region Destructor Functions
    public void DestroyAgent()
    {
        DisconnectEvents();

        pathfinding.DesttoryPathfindingAgent();

        pathfinding = null;
    }
    #endregion
}
