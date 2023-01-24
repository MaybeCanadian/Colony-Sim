using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class MapNode
{
    [SerializeField]
    PathFindingNode pathFindingNode;
    [SerializeField]
    float tileRotation;
    [SerializeField]
    private TileType tileType;

    [SerializeField]
    int nodeID;

    #region Event Dispatchers

    public delegate void NodeVisualValueChangedEvent();
    public NodeVisualValueChangedEvent OnNodeVisualsChangedEvent;

    public delegate void NodeRotationChangedEvent();
    public NodeRotationChangedEvent OnNodeRotationChangedEvent;

    public delegate void NodeCreateEvent();
    public NodeCreateEvent OnNodeCreateEvent;

    public delegate void NodeDestroyEvent();
    public NodeDestroyEvent OnNodeDestroyEvent;

    #endregion

    #region Node LifeCycle
    public MapNode(GridType nodeType, Vector3 nodePosition, int nodeID)
    {
        OnNodeCreateEvent?.Invoke();

        pathFindingNode = new PathFindingNode(nodeType, nodePosition);
        this.nodeID = nodeID;
    }

    public void DestroyNode()
    {
        OnNodeDestroyEvent?.Invoke();

        pathFindingNode.DestroyNode();
    }

    #endregion

    #region Node Data Functions

    public PathFindingNode GetPathNode()
    {
        return pathFindingNode;
    }
    public Vector3 GetNodePosition()
    {
        return pathFindingNode.GetNodePosition();
    }
    public TileType GetTileType()
    {
        return tileType;
    }
    public void SetTileType(TileType type, int weight)
    {
        tileType = type;

        pathFindingNode.SetTileWeight(weight);

        OnNodeVisualsChangedEvent?.Invoke();
    }
    public void SetTileRotation(float angle)
    {
        tileRotation = angle;

        OnNodeRotationChangedEvent?.Invoke();
    }
    public float GetTileRotation()
    {
        return tileRotation;
    }
    public int GetNodeID()
    {
        return nodeID;
    }
    public void SetNodeID(int input)
    {
        nodeID = input;
    } 

    #endregion
}
