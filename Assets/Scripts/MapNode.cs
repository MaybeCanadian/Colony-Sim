using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapNode
{
    #region Members
    //private Tile mapVisuals;
    private Vector3 position;
    private bool walkable = true;

    //Conencted Nodes
    private MapNode[] ConnectedNodes;
    
    private Dictionary<NodeDirections, MapNode> ConnectedNodeDict;

    #endregion

    #region Methods

    public MapNode(Vector3 inPos)
    {
        position = inPos;

        ConnectedNodes = new MapNode[8]
        {
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null
        };

        GenerateNodeDictionary();
    }

    #region Getters and Setters
    public Vector3 GetPosition()
    {
        return position;
    }
    public void SetPosition(Vector3 input)
    {
        position = input;
    }
    public void SetPosition(float x, float y, float z)
    {
        position = new Vector3(x, y, z);
    }
    public void SetPosition(Vector2 input)
    {
        position = input;
    }
    public bool GetWalkable()
    {
        return walkable;
    }
    public void SetWalkable(bool input)
    {
        walkable = input;
    }

    #endregion

    #region NodeFunctions
    private void GenerateNodeDictionary()
    {
        ConnectedNodeDict = new Dictionary<NodeDirections, MapNode>();
        ConnectedNodeDict.Add(NodeDirections.UP, ConnectedNodes[0]);
        ConnectedNodeDict.Add(NodeDirections.DOWN, ConnectedNodes[1]);
        ConnectedNodeDict.Add(NodeDirections.LEFT, ConnectedNodes[2]);
        ConnectedNodeDict.Add(NodeDirections.RIGHT, ConnectedNodes[3]);
        ConnectedNodeDict.Add(NodeDirections.UP_LEFT, ConnectedNodes[4]);
        ConnectedNodeDict.Add(NodeDirections.UP_RIGHT, ConnectedNodes[5]);
        ConnectedNodeDict.Add(NodeDirections.DOWN_LEFT, ConnectedNodes[6]);
        ConnectedNodeDict.Add(NodeDirections.DOWN_RIGHT, ConnectedNodes[7]);
    }
    public void RemoveAllConnections()
    {
        foreach(MapNode node in ConnectedNodes)
        {
            node?.DisconnectNode(this);
        }

        //reset all nodes back to null to allow for garbage collection
        ConnectedNodes[0] = null;
        ConnectedNodes[1] = null;
        ConnectedNodes[2] = null;
        ConnectedNodes[3] = null;
        ConnectedNodes[4] = null;
        ConnectedNodes[5] = null;
        ConnectedNodes[6] = null;
        ConnectedNodes[7] = null;

    }
    public void ConnectNode(NodeDirections connectedDirection, MapNode connectingNode)
    {
        ConnectedNodeDict[connectedDirection] = connectingNode;
    }
    public MapNode GetConnectedNode(NodeDirections direction)
    {
        return ConnectedNodeDict[direction];
    }
    public void DisconnectNode(MapNode disconnectingNode)
    {
        for(int i = 0; i < ConnectedNodes.Length; i++)
        {
            if (ConnectedNodes[i] == disconnectingNode)
            {
                ConnectedNodes[i] = null;
                return;
            }
        }

        return;
    }
    public void ClearNode()
    {
        RemoveAllConnections();

        //add any other clean up code
    }

    #endregion

    #endregion
}