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
    public int id;
    public AIAgentData data = null;

    [Header("Connected Systems")]
    public AIPathFindingAgent pathfinding = null;

    public AIBrain brain = null;

    public AIAgent(int id, Vector2Int startingGridPos, AIAgentData data = null)
    {
        this.id = id;

        SetUpBrain();
        SetUpData(data);
        SetUpPathfinging(startingGridPos);

        ConnectEvents();
    }
    private void SetUpBrain()
    {
        brain = new AIBrain(this);
    }
    private void SetUpPathfinging(Vector2Int startingGridPos)
    {
        pathfinding = new AIPathFindingAgent(startingGridPos);
    }
    private void SetUpData(AIAgentData data = null)
    {
        if (data == null)
        {
            this.data = new AIAgentData();
            return;
        }
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

        brain.BrainUpdate(deltaTime);

        pathfinding.PathfindingUpdate(deltaTime);
    }
    public void FixedUpdate(float fixedDeltaTime)
    {
        brain.BrainFixedUpdate(fixedDeltaTime);

        pathfinding.PathfindingFixedUpdate(fixedDeltaTime);
    }
    public void LateUpdate(float deltaTime) 
    {
        brain.BrainLateUpdate(deltaTime);

        pathfinding.PathfindingLateUpdate(deltaTime);
    }
    #endregion

    #region Destructor Functions
    public void DestroyAgent()
    {
        DisconnectEvents();

        pathfinding.DestoryPathfindingAgent();

        pathfinding = null;

        OnAgentDestroy?.Invoke();
    }
    #endregion

    #region Debug
    public void PathToRandomLocation()
    {
        pathfinding.PathToRandomLocation();
    }
    #endregion
}
