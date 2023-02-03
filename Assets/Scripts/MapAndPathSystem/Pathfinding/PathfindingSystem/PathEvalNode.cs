using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PathEvalNode
{
    PathFindingNode connectedNode;
    PathingEvalStates currentNodeState;
    int nodeHCost;
    int nodeGCost;
    int nodeFCost;

    PathEvalNode baseNode;

    public PathEvalNode(PathFindingNode node)
    {
        connectedNode = node;
        currentNodeState = PathingEvalStates.UNEVALUATED;
        nodeGCost = int.MaxValue;
        nodeHCost = int.MaxValue;

        DetermineNewFCost();
        baseNode = null;
    }

    #region Path Node Values
    public void SetNewConnectedNode(int newGCost, PathEvalNode originatingNode)
    {
        nodeGCost = newGCost;

        DetermineNewFCost();
        
        baseNode = originatingNode;
    }
    public void SetHCost(int input)
    {
        nodeHCost = input;

        DetermineNewFCost();
    }
    public int GetGCost()
    {
        return nodeGCost;
    }
    public int GetHCost()
    {
        return nodeHCost;
    }
    public int GetFCost()
    {
        return nodeFCost;
    }
    private void DetermineNewFCost()
    {
        nodeFCost = nodeGCost + nodeHCost;
    }
    public PathEvalNode GetBaseNode()
    {
        return baseNode;
    }
    public Vector3 GetNodeWorldPosition()
    {
        if(connectedNode == null)
        {
            Debug.LogError("ERROR - Cannot get world Position as connected Node is null.");
            return Vector3.zero;
        }

        return connectedNode.GetNodePosition();
    }
    public PathingEvalStates GetNodeState()
    {
        return currentNodeState;
    }
    public void SetNodeState(PathingEvalStates newState)
    {
        currentNodeState = newState;
    }
    public PathFindingNode GetConnectedPathfindingNode()
    {
        return connectedNode;
    }
    #endregion

    #region Connected PathFinding Node Functions

    public List<PathFindingNode> GetNodeNeighbours()
    {
        if(connectedNode == null)
        {
            Debug.LogError("ERROR - Connected Pathfinding Node was null.");
            return null;
        }

        List<PathFindingNode> neighbours = connectedNode.GetAllConnectedNodes();

        //connectedNode.PrintNeightbourPos();

        return neighbours;
    }

    #endregion

    #region Eval Node Lifecycle
    public void DeleteNode()
    {
        connectedNode = null;
        baseNode = null;
        return;
    }

    #endregion
}
