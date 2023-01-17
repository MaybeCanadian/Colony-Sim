using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualNode : MonoBehaviour
{
    [SerializeField]
    MapNode mapNode;

    [SerializeField]
    GameObject NodeTile;

    #region Event Handlers
    public void ConnectEvents(MapNode node)
    {
        mapNode = node;

        node.OnVisualValueChanged += OnVisualValueChangedEvent;
        node.OnNodeCreateEvent += OnMapNodeCreateEvent;
        node.OnNodeDestroyEvent += OnMapNodeDestroyEvent;
    }
    public void DisconnectEvents()
    {
        if (mapNode != null)
        {
            mapNode.OnVisualValueChanged -= OnVisualValueChangedEvent;
            mapNode.OnNodeCreateEvent -= OnMapNodeCreateEvent;
            mapNode.OnNodeDestroyEvent -= OnMapNodeDestroyEvent;
        }

        mapNode = null;
    }
    public void OnVisualValueChangedEvent()
    {

    }
    public void OnMapNodeDestroyEvent()
    {

    }
    public void OnMapNodeCreateEvent()
    {

    }

    #endregion

    #region Node LifeCycle
    public void DestroyNode()
    {
        Destroy(NodeTile);
        mapNode = null;
        Destroy(gameObject);
    }

    #endregion
}
