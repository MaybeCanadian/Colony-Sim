using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PathingEvaluationNode
{
    //This may cause memory things, maybe look into removing this refernece if pathing node is removed
    PathFindingNode pathing = null;
    PathingEvaluationStates state = PathingEvaluationStates.UNEVALUATED;

    PathingEvaluationNode connectedEvalNode = null;
    float gValue = 0.0f;

    public PathingEvaluationNode(PathFindingNode connectedPathfinding)
    {
        pathing = connectedPathfinding;
        state = PathingEvaluationStates.UNEVALUATED;

        gValue = 0.0f;
        connectedEvalNode = null;
    }
    public bool IsSameNode(PathFindingNode node)
    {
        if(node == null)
        {
            Debug.Log("What");
        }

        if(node == pathing)
        {
            return true;
        }

        return false;
    }
    public float GetEuclidianDistance(Vector3 TargetPos)
    {
        return (TargetPos - pathing.GetNodePosition()).magnitude;
    }
    public float GetGValue()
    {
        return gValue;
    }
    public void SetGValue(float value, PathingEvaluationNode baseNode)
    {
        gValue = value;
        connectedEvalNode = baseNode;
    }
    public List<PathFindingNode> GetConnectedNodes()
    {
        return pathing.GetAllConnectedNodes();
    }
}
