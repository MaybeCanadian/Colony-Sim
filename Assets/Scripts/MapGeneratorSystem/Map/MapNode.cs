using System.Collections;
using System.Collections.Generic;
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
    private Quaternion tileOrientation;

    #region Event Dispatchers

    public delegate void VisualValueChanged();
    public VisualValueChanged OnVisualValueChanged;

    public delegate void NodeCreateEvent();
    public NodeCreateEvent OnNodeCreateEvent;

    public delegate void NodeDestroyEvent();
    public NodeDestroyEvent OnNodeDestroyEvent;

    #endregion

    #region Node LifeCycle
    public MapNode(GridType nodeType, Vector3 nodePosition)
    {
        OnNodeCreateEvent?.Invoke();

        pathFindingNode = new PathFindingNode(nodeType, nodePosition);
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
    public float GetTileRotation()
    {
        return tileRotation;
    }
    public TileType GetTileType()
    {
        return tileType;
    }
    public Quaternion GetTileOrientation()
    {
        return tileOrientation;
    }

    #endregion
}
