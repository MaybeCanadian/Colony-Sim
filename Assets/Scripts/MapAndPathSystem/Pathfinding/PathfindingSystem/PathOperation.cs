using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEditor.PlayerSettings;

[System.Serializable]
public class PathOperation
{
    public int operationID;

    public Dictionary<PathFindingNode, PathEvalNode> activeNodeDict;

    public List<PathEvalNode> openNodes;
    public List<PathEvalNode> closedNodes;

    public PathEvalNode startNodeRef;
    public PathEvalNode endNodeRef;

    public PathOperation(int ID, PathFindingNode start, PathFindingNode end)
    {
        operationID = ID;

        activeNodeDict = new Dictionary<PathFindingNode, PathEvalNode>();

        openNodes = new List<PathEvalNode>();
        closedNodes = new List<PathEvalNode>();

        SetEndNode(end);
        SetStartNode(start);
    }

    #region Setup Functions
    private void SetStartNode(PathFindingNode start)
    {
        PathEvalNode startNode = SetUpNewOpenNode(start);

        startNode.SetNewConnectedNode(0, null);

        startNodeRef = startNode;
    }
    private void SetEndNode(PathFindingNode end)
    {
        PathEvalNode endNode = new PathEvalNode(end);

        endNode.SetHCost(0);

        activeNodeDict.Add(end, endNode);

        endNode.SetNodeState(PathingEvalStates.UNEVALUATED);

        endNodeRef = endNode;
    }
    #endregion

    #region Open Node List Control
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
    #endregion

    #region Pathfinding Functions
    public bool StartPathingOperation(out PathRoute route)
    {
        route = null;

        bool done = false;

        while (done == false)
        {
            PathEvalNode frontierNode = DetermineNextFrontierNode();

            if(frontierNode == null)
            {
                Debug.Log("Count not locate a path");
                return false;
            }

            done = EvaluateFrontierNode(frontierNode);
        }

        Debug.Log("Path Found");

        route = DeterminePath();

        CleanUpOperation();

        return true;
    }
    private PathRoute DeterminePath()
    {
        PathRoute route = new PathRoute();

        PathEvalNode currentNode = endNodeRef;

        if(endNodeRef.GetBaseNode() == null)
        {
            Debug.LogError("ERROR - End node has not base to reference");
            return null;
        }
        int count = 0;

        do
        {
            route.AddPathPoint(currentNode.GetConnectedPathfindingNode());

            currentNode = currentNode.GetBaseNode();

            count++;
        } while (currentNode.GetBaseNode() != null);


        route.AddPathPoint(currentNode.GetConnectedPathfindingNode());
        count++;

        Debug.Log("Done Generating Path, path is " + count + " long");

        return route;
    }
    private PathEvalNode DetermineNextFrontierNode()
    {
        //Debug.Log("Current open node count is " + openNodes.Count);

        if(openNodes.Count == 0)
        {
            //maybe change later to show no path can be found
            Debug.LogError("ERROR - Could not get a new frontier node as no nodes are open.");
            return null;
        }

        float lowestFCost = Mathf.Infinity;
        float currentFrontierHCost = Mathf.Infinity;
        PathEvalNode currentFrontierNode = null;

        foreach(PathEvalNode node in openNodes)
        {
            if(node.GetFCost() < lowestFCost) 
            {
                lowestFCost = node.GetFCost();
                currentFrontierHCost = node.GetHCost();
                currentFrontierNode = node;
                continue;
            }

            if(node.GetFCost() == lowestFCost)
            {
                if(node.GetHCost() < currentFrontierHCost)
                {
                    lowestFCost = node.GetGCost();
                    currentFrontierHCost = node.GetHCost();
                    currentFrontierNode = node;

                    continue;
                }
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
    private bool EvaluateFrontierNode(PathEvalNode frontierNode)
    {
        Debug.Log("Current Frontier node is at grid pos " + frontierNode.GetConnectedPathfindingNode().getNodeGridPos().x + " " + frontierNode.GetConnectedPathfindingNode().getNodeGridPos().y);

        List<PathFindingNode> frontierNeighbours = frontierNode.GetNodeNeighbours();

        int frontierGCost = frontierNode.GetGCost();

        int count = 0;

        int nodeNum = 0;

        foreach (PathFindingNode node in frontierNeighbours)
        {
            if (node == null)
            {
                Debug.Log("Node at grid pos " + frontierNode.GetConnectedPathfindingNode().getNodeGridPos().x + " " + frontierNode.GetConnectedPathfindingNode().getNodeGridPos().y + " skipped node num " + nodeNum);
                //This is not actually a thing we can check, we on the mpa edge
                nodeNum++;
                continue;
            }

            nodeNum++;

            PathEvalNode neighbour = null;

            if(activeNodeDict.ContainsKey(node))
            {
                //we have looked at it before
                neighbour = activeNodeDict[node];

                if(neighbour.GetNodeState() == PathingEvalStates.CLOSED)
                {
                    //skip the already done nodes
                    continue;
                }

                //Debug.Log("ReEvaluating node at grid pos" + node.getNodeGridPos().x + " " + node.getNodeGridPos().y + " using frontier node of  " + +frontierNode.GetConnectedPathfindingNode().getNodeGridPos().x + " " + frontierNode.GetConnectedPathfindingNode().getNodeGridPos().y);
            }
            else
            {
                //we have not seen this node before
                neighbour = SetUpNewOpenNode(node);
            }

            Debug.Log("Current Neighbour Node at grid Pos " + neighbour.GetConnectedPathfindingNode().getNodeGridPos().x + " " + neighbour.GetConnectedPathfindingNode().getNodeGridPos().y);

            int distanceBetweenFrontierAndNeighbour = GetDistanceFromNodeToNode(frontierNode, neighbour);

            int newGCost = frontierGCost + distanceBetweenFrontierAndNeighbour;

            if (newGCost < neighbour.GetGCost())
            {
                neighbour.SetNewConnectedNode(newGCost, frontierNode);
            }

            if (neighbour == endNodeRef)
            {
                Debug.Log("we have reached the endNode");
                return true;
            }

            count++;
            continue;
        }

        Debug.Log("Frontier Node at " + frontierNode.GetConnectedPathfindingNode().getNodeGridPos().x + " " + frontierNode.GetConnectedPathfindingNode().getNodeGridPos().y + " had " + count + " not closed neighbours");

        frontierNode.SetNodeState(PathingEvalStates.CLOSED);
        closedNodes.Add(frontierNode);
        openNodes.Remove(frontierNode);

        Debug.Log("Frontier Node at grid Pos " + frontierNode.GetConnectedPathfindingNode().getNodeGridPos().x + " " + frontierNode.GetConnectedPathfindingNode().getNodeGridPos().y + " is closed");
        return false;
    }
    private int GenerateHueristicValue(PathEvalNode node)
    {
        int EuclidianDistanceToEnd = (int)((node.GetNodeWorldPosition() - endNodeRef.GetNodeWorldPosition()).magnitude * 100.0f);

        return EuclidianDistanceToEnd;
    }
    private int GetDistanceFromNodeToNode(PathEvalNode node1, PathEvalNode node2)
    {
        int distance = (int)((node1.GetNodeWorldPosition() - node2.GetNodeWorldPosition()).magnitude * 100.0f);

        return distance;
    }
    private PathEvalNode SetUpNewOpenNode(PathFindingNode node)
    {
        PathEvalNode neighbour = new PathEvalNode(node);

        int neighbourHCost = GenerateHueristicValue(neighbour);

        //Debug.Log("Setting node at grid pos H value to " + node.getNodeGridPos().x + " " + node.getNodeGridPos().y + " to " + neighbourHCost);

        neighbour.SetHCost(neighbourHCost);

        activeNodeDict.Add(node, neighbour);

        openNodes.Add(neighbour);

        neighbour.SetNodeState(PathingEvalStates.OPEN);

        //Debug.Log("Opening node at grid pos " + node.getNodeGridPos().x + " " + node.getNodeGridPos().y);

        return neighbour;
    }
    private void CleanUpOperation()
    {
        foreach(PathEvalNode node in openNodes)
        {
            node.DeleteNode();
        }

        foreach(PathEvalNode node in closedNodes)
        {
            node.DeleteNode();
        }

        activeNodeDict.Clear();
        activeNodeDict = null;

        openNodes.Clear();
        openNodes = null;

        closedNodes.Clear();
        closedNodes = null;

        startNodeRef= null;
        endNodeRef= null;
    }
    #endregion
}
