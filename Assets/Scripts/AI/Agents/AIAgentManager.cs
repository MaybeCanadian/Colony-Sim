using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AIAgentManager
{
    #region Event Dispatchers
    //This is an event to tell all agents to remove themselves
    public delegate void RemoveAllAgents();
    public static RemoveAllAgents OnRemoveAllAgents;

    public delegate void AgentAddEvent(AIAgent agent);
    public static AgentAddEvent OnAgentAdd;

    public delegate void AgentRemoveEvent(int agentID);
    public static AgentRemoveEvent OnAgentRemove;

    //This is an event that says all agents have been removed
    public delegate void AgentRemovedAllEvent();
    public static AgentRemovedAllEvent OnAgentRemovedAll;
    #endregion

    public static Dictionary<int, AIAgent> activeAgents;
    public static Queue<int> replacementID;
    public static int nextAgentID;

    public static int numAgents;

    #region Init Functions
    public static void Init()
    {
        activeAgents = new Dictionary<int, AIAgent>();
        replacementID = new Queue<int>();

        nextAgentID = 0;

        numAgents = 0;
    }
    private static void CheckInit()
    {
        if(activeAgents == null)
        {
            Init();
        }
    }
    #endregion

    #region Agent Add
    public static void SpawnNewAgentAtNodePos(Vector2 nodePos)
    {
        CheckInit();

        int idToGive = -1;

        if(replacementID.Count != 0)
        {
            idToGive = replacementID.Dequeue();
        }
        else
        {
            idToGive = nextAgentID;
            nextAgentID++;
        }

        AIAgent agent = new AIAgent(idToGive, nodePos);

        activeAgents.Add(idToGive, agent);

        OnAgentAdd?.Invoke(agent);

        numAgents++;
    }
    #endregion

    #region Agent Search
    public static AIAgent GetAgentByID(int ID)
    {
        CheckInit();

        return activeAgents[ID];
    }
    #endregion

    #region Agent Remove
    public static bool RemoveAgentAtID(int ID)
    {
        CheckInit();

        if(!activeAgents.ContainsKey(ID))
        {
            Debug.LogError("ERROR - Could not find an agent with given ID to remove.");
            return false;
        }

        if (activeAgents[ID] == null)
        {
            Debug.LogError("ERROR - Agent at given ID was null when remove call was given.");
            activeAgents.Remove(ID);
            ValidateAgentCount();
            return false;
        }

        activeAgents[ID].DestroyAgent();
        activeAgents[ID] = null;

        activeAgents.Remove(ID);

        replacementID.Enqueue(ID);

        OnAgentRemove?.Invoke(ID);

        numAgents--;

        return true;
    }
    public static void RemoveAgents()
    {
        CheckInit();

        OnRemoveAllAgents?.Invoke();

        activeAgents.Clear();
        replacementID.Clear();
        nextAgentID = 0;

        OnAgentRemovedAll?.Invoke();

        numAgents = 0;
    }

    #endregion

    #region Manager Values
    public static int GetAgentCount()
    {
        return numAgents;
    }
    public static void ValidateAgentCount()
    {
        numAgents = activeAgents.Count;
    }
    #endregion
}
