using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PathFindingNode
{
    [SerializeField]
    Vector3 nodePosition;

    [SerializeField]
    bool isWalkable;

    [SerializeField]
    GridType nodeType;

    PathFindingNode[] connectedNodes;
    Dictionary<NodeConnectionDirections, PathFindingNode> connectionsDictionary;

    #region Init Functions
    public PathFindingNode(GridType nodeType, Vector3 nodePosition, bool isWalkable = true)
    {
        this.nodeType = nodeType;
        this.nodePosition = nodePosition;
        this.isWalkable = isWalkable;

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
        connectedNodes = new PathFindingNode[6];

        connectionsDictionary = new Dictionary<NodeConnectionDirections, PathFindingNode>();
        connectionsDictionary.Add(NodeConnectionDirections.UP, connectedNodes[0]);
        connectionsDictionary.Add(NodeConnectionDirections.UP_RIGHT, connectedNodes[1]);
        connectionsDictionary.Add(NodeConnectionDirections.DOWN_RIGHT, connectedNodes[2]);
        connectionsDictionary.Add(NodeConnectionDirections.DOWN, connectedNodes[3]);
        connectionsDictionary.Add(NodeConnectionDirections.DOWN_LEFT, connectedNodes[4]);
        connectionsDictionary.Add(NodeConnectionDirections.UP_LEFT, connectedNodes[5]);
    }
    private void SetUpSquareNode()
    {
        connectedNodes = new PathFindingNode[4];

        connectionsDictionary = new Dictionary<NodeConnectionDirections, PathFindingNode>();
        connectionsDictionary.Add(NodeConnectionDirections.UP, connectedNodes[0]);
        connectionsDictionary.Add(NodeConnectionDirections.RIGHT, connectedNodes[1]);
        connectionsDictionary.Add(NodeConnectionDirections.DOWN, connectedNodes[2]);
        connectionsDictionary.Add(NodeConnectionDirections.LEFT, connectedNodes[3]);
    }

    #endregion

    #region Node Data Functions

    public bool GetIfNodeWalkable()
    {
        return isWalkable;
    }
    public GridType GetNodeType()
    {
        return nodeType;
    }
    public Vector3 GetNodePosition()
    {
        return nodePosition;
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

        connectionsDictionary[side] = node;
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

    #endregion

    #region Node LifeCycle
    public void DestroyNode()
    {

    }

    #endregion
}
