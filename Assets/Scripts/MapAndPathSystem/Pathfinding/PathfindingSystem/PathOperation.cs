using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        SetStartNode(start);
        SetEndNode(end);
    }

    #region Setup Functions
    private void SetStartNode(PathFindingNode start)
    {
        PathEvalNode startNode = new PathEvalNode(start);

        activeNodeDict.Add(start, startNode);

        startNode.SetNodeGValue(0.0f, null);

        startNodeRef = startNode;

        openNodes.Add(startNode);

        startNode.SetNodeState(PathingEvalStates.OPEN);

        //Debug.Log("Added start node to open nodes");
        //Debug.Log("Current Size of Open nodes is " + openNodes.Count);
    }
    private void SetEndNode(PathFindingNode end)
    {
        PathEvalNode endNode = new PathEvalNode(end);

        activeNodeDict.Add(end, endNode);

        endNode.SetNodeGValue(Mathf.Infinity, null);

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
        Debug.Log("Current open node count is " + openNodes.Count);

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

        Debug.Log("Found a new frontier node");
        currentFrontierNode.SetNodeState(PathingEvalStates.FRONTIER);
        return currentFrontierNode;
    }
    private bool EvaluateFrontierNode(PathEvalNode frontierNode)
    {
        List<PathFindingNode> frontierNeighbours = frontierNode.GetNodeNeighbours();

        //Debug.Log("Frontier node has " + frontierNeighbours.Count + " neighbours");

        foreach(PathFindingNode node in frontierNeighbours)
        {
            if(node == null)
            {
                Debug.Log("Skipped");
                //This is not actually a thing we can check, we on the mpa edge
                continue;
            }

            //if(!node.GetIfNodeWalkable())
            //{
            //    //node cannot be travesed so ignore
            //    continue;
            //}

            PathEvalNode neighbour = null;

            if(activeNodeDict.ContainsKey(node))
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

            float hueristic = GenerateHueristicValue(neighbour);

            float distanceToThisNodeFromBase = frontierNode.GetGValue();

            float newGValue = hueristic + distanceToThisNodeFromBase;

            if(newGValue < neighbour.GetGValue())
            {
                neighbour.SetNodeGValue(newGValue, frontierNode);
            }

            if (neighbour == endNodeRef)
            {
                Debug.Log("we have reached the endNode");
                return true;
            }

            continue;
        }

        frontierNode.SetNodeState(PathingEvalStates.CLOSED);
        closedNodes.Add(frontierNode);
        openNodes.Remove(frontierNode);

        Debug.Log("Node Evaluated");
        return false;
    }
    private float GenerateHueristicValue(PathEvalNode node)
    {
        float EuclidianDistanceToEnd = (node.GetNodeWorldPosition() - endNodeRef.GetNodeWorldPosition()).magnitude;

        return EuclidianDistanceToEnd;
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
