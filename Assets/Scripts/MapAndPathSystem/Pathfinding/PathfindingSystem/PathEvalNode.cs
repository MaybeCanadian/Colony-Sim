using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PathEvalNode
{
    PathFindingNode connectedNode;
    PathingEvalStates currentNodeState;
    float gValue;

    PathEvalNode baseNode;

    public PathEvalNode(PathFindingNode node)
    {
        connectedNode = node;
        currentNodeState = PathingEvalStates.UNEVALUATED;
        gValue = Mathf.Infinity;
        baseNode = null;
    }

    #region Path Node Values
    public void SetNodeGValue(float newValue, PathEvalNode originatingNode)
    {
        gValue = newValue;
        baseNode = originatingNode;
    }
    public float GetGValue() 
    {
        return gValue;
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
