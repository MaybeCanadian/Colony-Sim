using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PathOperation
{
    public int operationID;

    public Dictionary<PathFindingNode, PathEvalNode> activeNodeDict;

    public List<PathEvalNode> openNodes;

    public PathEvalNode startNodeRef;
    public PathEvalNode endNodeRef;

    public PathOperation(int ID)
    {
        operationID = ID;

        activeNodeDict = new Dictionary<PathFindingNode, PathEvalNode>();

        openNodes = new List<PathEvalNode>();
    }
    private void SetStartNode(PathFindingNode start)
    {
        PathEvalNode startNode = new PathEvalNode(start);

        activeNodeDict.Add(start, startNode);

        startNode.SetNodeGValue(0.0f, null);

        startNodeRef = startNode;

        openNodes.Add(startNode);

        startNode.SetNodeState(PathingEvalStates.OPEN);
    }
    private void SetEndNode(PathFindingNode end)
    {
        PathEvalNode endNode = new PathEvalNode(end);

        activeNodeDict.Add(end, endNode);

        endNode.SetNodeGValue(Mathf.Infinity, null);

        endNodeRef = endNode;
    }
    private void PurgeClosedNodesFromOpenList()
    {
        List<int> purgeItts = new List<int>();

        for(int i = 0; i < openNodes.Count; i++)
        {
            if (openNodes[i].GetNodeState() == PathingEvalStates.CLOSED)
            {
                purgeItts.Add(i);
            }
        }

        for(int i = purgeItts.Count - 1; i >= 0; i--)
        {
            openNodes.RemoveAt(i);
        }

        return;
    }
    private PathEvalNode DetermineNextFrontierNode()
    {
        if(openNodes.Count == 0)
        {
            //maybe change later to show no path can be found
            Debug.LogError("ERROR - Could not get a new frontier node as no nodes are open.");
            return null;
        }

        float lowestGValue = Mathf.Infinity;
        PathEvalNode currentFrontierNode = null;

        foreach(PathEvalNode node in openNodes)
        {
            if(node.GetGValue() < lowestGValue) 
            {
                lowestGValue = node.GetGValue();
                currentFrontierNode = node;
            }
        }


        if(currentFrontierNode == null)
        {
            Debug.LogError("ERROR - Frontier node was unable to be found.");
            return null;
        }

        currentFrontierNode.SetNodeState(PathingEvalStates.FRONTIER);
        return currentFrontierNode;
    }
    private void EvaluateFrontierNode(PathEvalNode frontierNode)
    {
        List<PathFindingNode> frontierNeighbours = frontierNode.GetNodeNeighbours();

        foreach(PathFindingNode node in frontierNeighbours)
        {
            if(!node.GetIfNodeWalkable())
            {
                //node cannot be travesed so ignore
                continue;
            }

            PathEvalNode neighbour = null;

            if(!activeNodeDict.ContainsKey(node))
            {
                //we have looked at it before
                neighbour = activeNodeDict[node];
            }
            else
            {
                //we have not seen this node before
                neighbour = new PathEvalNode(node);

                activeNodeDict.Add(node, neighbour);

                openNodes.Add(neighbour);

                neighbour.SetNodeState(PathingEvalStates.OPEN);
            }

            
            continue;
        }
    }
}
