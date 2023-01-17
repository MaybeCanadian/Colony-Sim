using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapNode
{
    [SerializeField]
    VisualNode visualsNode;
    [SerializeField]
    PathFindingNode pathFindingNode;

    [SerializeField]
    float tileRotation;

    [SerializeField]
    MapChunk chunk;

    #region Event Dispatchers

    public delegate void VisualValueChanged();
    public VisualValueChanged OnVisualValueChanged;

    public delegate void NodeCreateEvent();
    public NodeCreateEvent OnNodeCreateEvent;

    public delegate void NodeDestroyEvent();
    public NodeDestroyEvent OnNodeDestroyEvent;

    #endregion

    #region Node LifeCycle
    public MapNode(MapChunk chunk, Vector3 nodePosition)
    {
        this.chunk = chunk;

        OnNodeCreateEvent?.Invoke();

        pathFindingNode = new PathFindingNode(chunk.GetTileGridType(), nodePosition);
    }

    public void DestroyNode()
    {
        OnNodeDestroyEvent?.Invoke();

        visualsNode.DestroyNode();
        pathFindingNode.DestroyNode();
    }

    #endregion

    #region Node Data Functions

    public PathFindingNode GetPathNode()
    {
        return pathFindingNode;
    }
    public VisualNode GetVisualNode()
    {
        return visualsNode;
    }
    public Vector3 GetNodePosition()
    {
        return pathFindingNode.GetNodePosition();
    }
    public float GetTileRotation()
    {
        return tileRotation;
    }
    public GridType GetNodeGridType()
    {
        return chunk.GetTileGridType();
    }

    #endregion
}
