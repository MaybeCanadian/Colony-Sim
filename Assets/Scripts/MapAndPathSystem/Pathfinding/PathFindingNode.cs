using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class PathFindingNode
{
    [SerializeField]
    Vector3 nodeWorldPosition;
    Vector2 nodeGridPosition;

    [SerializeField]
    bool isWalkable;
    [SerializeField]
    int tileWeight;

    [SerializeField]
    GridType nodeType;

    [SerializeField]
    int numConnectedNodes;

    //PathFindingNode[] connectedNodes;
    Dictionary<NodeConnectionDirections, PathFindingNode> connectionsDictionary;

    #region Init Functions
    public PathFindingNode(GridType nodeType, Vector3 nodePosition, Vector2 gridPosition, bool isWalkable = true)
    {
        this.nodeType = nodeType;
        this.nodeWorldPosition = nodePosition;
        this.isWalkable = isWalkable;
        this.nodeGridPosition = gridPosition;

        numConnectedNodes = 0;

        switch (nodeType)
        {
            case GridType.HEX:
                SetUpHexNode();
                break;
            case GridType.SQUARE:
                SetUpSquareNode();
                break;
        }
    }
    private void SetUpHexNode()
    {

        connectionsDictionary = new Dictionary<NodeConnectionDirections, PathFindingNode>
        {
            { NodeConnectionDirections.LEFT, null },
            { NodeConnectionDirections.UP_LEFT, null },
            { NodeConnectionDirections.UP_RIGHT, null },
            { NodeConnectionDirections.RIGHT, null },
            { NodeConnectionDirections.DOWN_RIGHT, null },
            { NodeConnectionDirections.DOWN_LEFT, null }
        };
    }
    private void SetUpSquareNode()
    {

        connectionsDictionary = new Dictionary<NodeConnectionDirections, PathFindingNode>
        {
            { NodeConnectionDirections.UP, null },
            { NodeConnectionDirections.RIGHT, null },
            { NodeConnectionDirections.DOWN, null },
            { NodeConnectionDirections.LEFT, null }
        };
    }

    #endregion

    #region Node Data Functions

    public bool GetIfNodeWalkable()
    {
        return isWalkable;
    }
    public int GetTileWeight()
    {
        return tileWeight;
    }
    public void SetTileWeight(int input)
    {
        tileWeight = input;

        if(tileWeight < 0)
        {
            isWalkable = false;
        }
    }
    public GridType GetNodeType()
    {
        return nodeType;
    }
    public Vector3 GetNodePosition()
    {
        return nodeWorldPosition;
    }

    #endregion

    #region Node Connection Functions
    public void ConnectNodeOnSide(NodeConnectionDirections side, PathFindingNode node)
    {
        if(!connectionsDictionary.ContainsKey(side))
        {
            Debug.LogError("ERROR - Node Type does not contain equested side");
            return;
        }

        numConnectedNodes++;
        connectionsDictionary[side] = node;

        if (connectionsDictionary[side] != null)
        {
            //edge
        }
    }
    public PathFindingNode GetConnectedNodeOnSide(NodeConnectionDirections side)
    {
        if(!connectionsDictionary.ContainsKey(side))
        {
            Debug.LogError("ERROR - Node Type does not contain requested side.");
            return null;
        }

        return connectionsDictionary[side];
    }
    public bool GetIfCanWalkToConnectedNode(NodeConnectionDirections side)
    {
        if(!connectionsDictionary.ContainsKey(side))
        {
            Debug.LogError("ERROR - Node type does not contain requested side.");
            return false;
        }

        if (connectionsDictionary[side] == null)
        {
            Debug.LogError("ERROR - No node registered as connected to given side");
            return false;
        }

        return connectionsDictionary[side].GetIfNodeWalkable();
    }
    public List<PathFindingNode> GetAllConnectedNodes()
    {
        List<PathFindingNode> tempConnectedList = new List<PathFindingNode>();

        foreach (int dir in Enum.GetValues(typeof(NodeConnectionDirections)))
        {
            if (!connectionsDictionary.ContainsKey((NodeConnectionDirections)dir))
            {
                //Debug.Log("Skipping " + (NodeConnectionDirections)dir);
                continue;
            }

            tempConnectedList.Add(connectionsDictionary[(NodeConnectionDirections)dir]);
        }

        return tempConnectedList;
    }
    public Vector2 getNodeGridPos()
    {
        return nodeGridPosition;
    }

    #endregion

    #region Node LifeCycle
    public void DestroyNode()
    {
        connectionsDictionary.Clear();
    }

    #endregion

    #region Debug Functions
    public void PrintNodeType()
    {
        Debug.Log("My node type is " + nodeType);
    }
    public void PrintNeightbourPos()
    {
        string fullList = "All neighbours are: ";

        foreach (int dir in Enum.GetValues(typeof(NodeConnectionDirections)))
        {
            if(!connectionsDictionary.ContainsKey((NodeConnectionDirections)dir))
            {
                //Debug.Log("Skipping " + (NodeConnectionDirections)dir);
                continue;
            }

            fullList += (NodeConnectionDirections)dir + ": is " + connectionsDictionary[(NodeConnectionDirections)dir].GetNodePosition();
        }

        Debug.Log(fullList);
    }
    public void PrintSidesMissingNodes()
    {
        string message = "Node ID is " + nodeGridPosition + "Sides are ";

        foreach (int dir in Enum.GetValues(typeof(NodeConnectionDirections)))
        {
            if (!connectionsDictionary.ContainsKey((NodeConnectionDirections)dir))
            {
                //Debug.Log("Skipping " + (NodeConnectionDirections)dir);
                continue;
            }

            if (connectionsDictionary[(NodeConnectionDirections)dir] == null)
            {
                message += (NodeConnectionDirections)dir + " ";
            }
        }

        Debug.Log(message);
    }
    #endregion
}
