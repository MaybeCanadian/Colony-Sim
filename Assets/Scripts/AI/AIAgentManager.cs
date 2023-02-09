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

    public static void Init()
    {
        activeAgents = new Dictionary<int, AIAgent>();
        replacementID = new Queue<int>();

        nextAgentID = 0;
    }
    #region Agent Add
    public static void SpawnNewAgentAtNodePos(Vector2 nodePos)
    {
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
    }
    #endregion

    #region Agent Search
    public static AIAgent GetAgentByID(int ID)
    {
        return activeAgents[ID];
    }
    #endregion

    #region Agent Remove
    public static void RemoveAgentAtID(int ID)
    {
        activeAgents[ID].DestroyAgent();
        activeAgents[ID] = null;

        activeAgents.Remove(ID);

        replacementID.Enqueue(ID);

        OnAgentRemove?.Invoke(ID);
    }
    public static void RemoveAgents()
    {
        OnRemoveAllAgents?.Invoke();

        activeAgents.Clear();
        replacementID.Clear();
        nextAgentID = 0;

        OnAgentRemovedAll?.Invoke();
    }

    #endregion
}
