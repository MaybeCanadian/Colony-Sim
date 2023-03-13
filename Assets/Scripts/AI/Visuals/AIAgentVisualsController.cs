using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAgentVisualsController : MonoBehaviour
{
    public static AIAgentVisualsController instance;

    [Header("Parents")]
    public GameObject AIParent = null;

    [Header("AI Visual Agents")]
    private List<AIAgentVisual> AIAgentVisualList = null;
    private bool visualsActive = false;

    #region Init Functions
    private void Awake()
    {
        if(instance != this && instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;

            CheckInit();
        }
    }
    private void OnEnable()
    {
        ConnectEvents();
    }
    private void OnDisable()
    {
        DisconnectEvents();
    }
    private void Init()
    {
        SetUpAIParentOBJ();

        SetUpVisualsList();   
    }
    private void CheckInit()
    {
        if (AIParent == null)
        {
            Init();
        }
    }
    private void SetUpAIParentOBJ()
    {
        if(AIParent != null)
        {
            return;
        }

        AIParent = new GameObject();
        AIParent.name = "[AI]";
    }
    private void SetUpVisualsList()
    {
        AIAgentVisualList = new List<AIAgentVisual>();
    }
    #endregion

    #region Event Functions
    private void ConnectEvents()
    {
        AIAgentManager.OnAgentAdd += OnAIAgentAdded;
    }
    private void DisconnectEvents()
    {
        AIAgentManager.OnAgentAdd -= OnAIAgentAdded;
    }
    #endregion

    #region AI Visuals
    public void CreateAIVisuals()
    {
        visualsActive = true;

        foreach(AIAgentVisual agent in AIAgentVisualList)
        {
            agent.CreateAIAgentVisuals();
        }
    }
    public void DestoryAIVisuals()
    {
        visualsActive = false;

        foreach(AIAgentVisual agent in AIAgentVisualList)
        {
            agent.DestoryAIAgentVisuals();
        }
    }
    #endregion

    #region Agent Control
    private AIAgentVisual CreateAIVisualAgent(AIAgent connectedAgent)
    {
        if(connectedAgent == null)
        {
            Debug.LogError("ERROR - Could not create visuals as the connected agent is null");
            return null;
        }

        GameObject AIVisualOBJ = new GameObject();
        AIAgentVisual AIVisuals = AIVisualOBJ.AddComponent<AIAgentVisual>();

        if(AIVisuals == null)
        {
            Debug.LogError("ERROR - Issue creating the aivisual obj");
            return null;
        }

        AIVisuals.transform.SetParent(AIParent.transform);

        AIVisuals.ConnectAIVisuals(connectedAgent);

        AIVisuals.OnAgentRemoved += OnAIAgentVisualRemoved;

        AIAgentVisualList.Add(AIVisuals);

        if (visualsActive)
        {
            AIVisuals.CreateAIAgentVisuals();
        }

        return AIVisuals;
    }
    #endregion

    #region Event Recievers
    private void OnAIAgentAdded(AIAgent agent)
    {
        CheckInit();

        CreateAIVisualAgent(agent);

        return;
    }
    private void OnAIAgentVisualRemoved(AIAgentVisual agentVisual)
    {
        CheckInit();

        agentVisual.OnAgentRemoved -= OnAIAgentVisualRemoved;

        AIAgentVisualList.Remove(agentVisual);
    }
    #endregion
}
