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
    [SerializeField]
    private AIAgent connectedAgent = null;

    [Header("Offset")]
    public static Vector3 AgentOffset = new Vector3(0.0f, 1.05f, 0.0f);
    #endregion

    #region Agent Connection
    public void ConnectAIVisuals(AIAgent agent) 
    {
        if(agent == null)
        {
            Debug.LogError("ERROR - Can not connect to an agent that is null");
            return;
        }

        connectedAgent = agent;

        name = connectedAgent.data.agentName + "'s visuals";

        ConnectEvents();
    }
    #endregion

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

        GameObject model = CharacterModelDataBase.GetModel(connectedAgent.data.modelToUse);

        AIObject = Instantiate(model);
        AIObject.name = connectedAgent.data.agentName + " visuals";
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
        connectedAgent.pathfinding.OnAgentMove += OnAgentMoved;
    }
    private void DisconnectEvents()
    {
        if(connectedAgent == null)
        {
            Debug.LogError("ERROR - Could not disconnect Events as connected agent was null");
            return;
        }

        connectedAgent.OnAgentDestroy -= OnConnectedAgentDestroyed;
        connectedAgent.pathfinding.OnAgentMove -= OnAgentMoved;
    }
    #endregion

    #region Event Recievers
    private void OnAgentMoved(Vector3 newPos)
    {
        //Debug.Log("Moved");
        transform.position = newPos;
    }
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

        transform.position = connectedAgent.pathfinding.GetWorldPos();
    }
    #endregion
}
