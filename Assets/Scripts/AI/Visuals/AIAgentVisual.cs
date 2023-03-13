using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAgentVisual : MonoBehaviour
{
    #region Event Dispatchers
    public delegate void AgentRemovedEvent(AIAgentVisual agent);
    public AgentRemovedEvent OnAgentRemoved;
    #endregion

    #region Member Variables
    [Header("Game Object Values")]
    private GameObject AIObject = null;

    [Header("Connected Agent")]
    private AIAgent connectedAgent = null;

    [Header("Offset")]
    public static Vector3 AgentOffset = new Vector3(0.0f, 1.05f, 0.0f);
    #endregion

    public void ConnectAIVisuals(AIAgent agent) 
    {
        if(agent == null)
        {
            Debug.LogError("ERROR - Can not connect to an agent that is null");
            return;
        }

        connectedAgent = agent;

        name = connectedAgent.agentData.agentName + "'s visuals";

        ConnectEvents();
    }

    #region AI Visuals
    public void CreateAIAgentVisuals()
    {
        if(connectedAgent == null)
        {
            Debug.LogError("ERROR - Cannot set up visuals without a connected agent.");
            return;
        }

        if(AIObject != null)
        {
            DestoryAIAgentVisuals();
        }

        GameObject model = CharacterModelDataBase.GetModel(connectedAgent.agentData.modelToUse);

        AIObject = Instantiate(model);
        AIObject.name = connectedAgent.agentData.agentName + " visuals";
        AIObject.transform.SetParent(transform);

        MoveToPos();
    }
    public void DestoryAIAgentVisuals()
    {
        if(AIObject == null)
        {
            return;
        }

        Destroy(AIObject);

        AIObject = null;
    }
    #endregion

    #region Event Functions
    private void ConnectEvents()
    {
        if (connectedAgent == null)
        {
            Debug.LogError("ERROR - Could not connect events as the connected agent was null");
            return;
        }

        connectedAgent.OnAgentDestroy += OnConnectedAgentDestroyed;
    }
    private void DisconnectEvents()
    {
        if(connectedAgent == null)
        {
            Debug.LogError("ERROR - Could not disconnect Events as connected agent was null");
            return;
        }

        connectedAgent.OnAgentDestroy -= OnConnectedAgentDestroyed;
    }
    #endregion

    #region Event Recievers
    private void OnConnectedAgentDestroyed()
    {
        DisconnectEvents();

        OnAgentRemoved?.Invoke(this);

        connectedAgent = null;

        Destroy(gameObject);
        //maybe remove this thing?
    }
    #endregion

    #region Movement Functions
    private void MoveToPos()
    {
        if(connectedAgent == null)
        {
            Debug.LogError("ERROR - Could not move to ai location as connected agent is null");
            return;
        }

        transform.position = connectedAgent.pathfindingAgent.GetWorldPos();
    }
    #endregion
}
