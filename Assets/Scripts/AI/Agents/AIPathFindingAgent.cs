using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AIPathFindingAgent
{
    //AIAgent agent;
    public Vector2Int currentGridPos;

    #region Lifecycle
    public AIPathFindingAgent(Vector2Int startingGridPos)
    {
        currentGridPos = startingGridPos;
    }
    public void DestoryPathfindingAgent()
    {
        //agent = null;
    }
    #endregion

    #region Position Data
    public Vector3 GetWorldPos()
    {
        Vector3 worldPos = Vector3.zero;

        MapNode currentNode = MapGenerator.GetMap().GetMapNode(currentGridPos.x, currentGridPos.y);

        worldPos = currentNode.GetNodePosition();

        worldPos += new Vector3(0.0f, 1.05f, 0.0f);

        return worldPos;
    }
    #endregion
}
