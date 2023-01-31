using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public static class PathingSystem
{
    #region Path Determination
    public static PathingPath GetPathFromNodeToNode(PathFindingNode start, PathFindingNode end)
    {
        PathingPath path = new PathingPath();

        List<PathingEvaluationNode> openNodes = new List<PathingEvaluationNode>();
        List<PathingEvaluationNode> closedNodes = new List<PathingEvaluationNode>();

        Debug.Log("Starting to check for path");

        PathingEvaluationNode startNode = new PathingEvaluationNode(start);
        startNode.SetGValue(0.0f, null);

        openNodes.Add(startNode);

        bool finished = false;

        int count = 0;

        while(finished == false)
        {
            Debug.Log("Starting itteration " + count);

            PathingEvaluationNode frontierNode = FindNextFrontierNode(openNodes);

            finished = EvaluateNode(frontierNode, end, openNodes, closedNodes);

            openNodes.Remove(frontierNode);

            closedNodes.Add(frontierNode);

            count++;
        }

        Debug.Log("We have found a path.");

        return path;
    }

    #region Overloads of Pathfinding Function
    public static PathingPath GetPathFromGridPosToGridPos(Vector2Int startPos, Vector2Int endPos)
    {
        MapObject map = MapGenerator.GetMap();

        if(map == null)
        {
            Debug.LogError("ERROR - Cannot find path, map is not initialized");
            return null;
        }

        PathFindingNode startNode = map.GetMapNode(startPos.x, startPos.y).GetPathNode();

        PathFindingNode endNode = map.GetMapNode(endPos.x, endPos.y).GetPathNode();

        return GetPathFromNodeToNode(startNode, endNode);
    }
    public static PathingPath GetPathFromPosToNode(Vector3 start, PathFindingNode end)
    {
        MapObject map = MapGenerator.GetMap();

        PathFindingNode startNode = map.GetNodeByPos(start).GetPathNode();

        return GetPathFromNodeToNode(startNode, end);
    }
    public static PathingPath GetPathFromNodeToPos(PathFindingNode start, Vector3 end)
    {
        MapObject map = MapGenerator.GetMap();

        PathFindingNode endNode = map.GetNodeByPos(end).GetPathNode();

        return GetPathFromNodeToNode(start, endNode);
    }
    public static PathingPath GetPathFromPosToPos(Vector3 start, Vector3 end)
    {
        MapObject map = MapGenerator.GetMap();

        PathFindingNode endNode = map.GetNodeByPos(end).GetPathNode();

        PathFindingNode startNode = map.GetNodeByPos(start).GetPathNode();

        return GetPathFromNodeToNode(startNode, endNode);
    }
    #endregion
    #endregion

    #region Pathfinding Helper Functions
    private static PathingEvaluationStates GetNodeCurrentEvalState(PathFindingNode node, out PathingEvaluationNode nodeEvalRef, List<PathingEvaluationNode> openNodes, List<PathingEvaluationNode> closedNodes)
    {
        nodeEvalRef = null;

        foreach(PathingEvaluationNode openNode in openNodes)
        {
            if(openNode.IsSameNode(node))
            {
                nodeEvalRef = openNode;
                return PathingEvaluationStates.OPEN;
            }
        }

        foreach(PathingEvaluationNode closedNode in closedNodes)
        {
            if(closedNode.IsSameNode(node))
            {
                nodeEvalRef = closedNode;
                return PathingEvaluationStates.CLOSED;
            }
        }

        return PathingEvaluationStates.UNEVALUATED;
    }
    private static PathingEvaluationNode FindNextFrontierNode(List<PathingEvaluationNode> openNodes)
    {
        PathingEvaluationNode nextNode = null;

        float currentLowestG = Mathf.Infinity;

        foreach(PathingEvaluationNode openNode in openNodes)
        {

            if(openNode.GetGValue() < currentLowestG)
            {
                nextNode = openNode;
                currentLowestG = openNode.GetGValue();
            }
        }

        return nextNode;
    }
    private static bool EvaluateNode(PathingEvaluationNode node, PathFindingNode endNode, List<PathingEvaluationNode> openNodes, List<PathingEvaluationNode> closedNodes)
    {

        if(node.IsSameNode(endNode))
        {
            return true;
        }

        List<PathFindingNode> connectedNodes = node.GetConnectedNodes();

        foreach(PathFindingNode pathNode in connectedNodes)
        {

            if(!pathNode.GetIfNodeWalkable())
            {
                continue;
            }

            PathingEvaluationStates nodeState = GetNodeCurrentEvalState(pathNode, out PathingEvaluationNode nodeRef, openNodes, closedNodes);

            if(nodeState == PathingEvaluationStates.CLOSED)
            {
                continue;
            }

            if(nodeState == PathingEvaluationStates.OPEN)
            {
                ReEvaluateNode(nodeRef, node, endNode);
                continue;
            }

            if(nodeState == PathingEvaluationStates.UNEVALUATED)
            {
                PathingEvaluationNode newOpenNode = new PathingEvaluationNode(pathNode);
                SetUpNewOpenNode(newOpenNode, node, endNode);
                continue;
            }
        }

        return false;
    }
    private static void SetUpNewOpenNode(PathingEvaluationNode newNode, PathingEvaluationNode baseNode, PathFindingNode endNode)
    {
        float newGValue = DetermineGValue(newNode, baseNode, endNode);
        newNode.SetGValue(newGValue, baseNode);

        return;
    }
    private static void ReEvaluateNode(PathingEvaluationNode evaluatingNode, PathingEvaluationNode baseNode, PathFindingNode endNode)
    {
        float newGValue = DetermineGValue(evaluatingNode, baseNode, endNode);

        if (newGValue < evaluatingNode.GetGValue())
        {
            evaluatingNode.SetGValue(newGValue, baseNode);
        }
    }
    private static float DetermineGValue(PathingEvaluationNode evaluatingNode, PathingEvaluationNode baseNode, PathFindingNode endNode)
    {
        float hueristic = evaluatingNode.GetEuclidianDistance(endNode.GetNodePosition());

        float GValue = baseNode.GetGValue() + hueristic;

        return GValue;
    }
    #endregion
}
