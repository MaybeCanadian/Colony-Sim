using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualNode : MonoBehaviour
{
    [SerializeField]
    MapNode mapNode;

    [SerializeField]
    GameObject NodeTile;

    public void ConnectEvents(MapNode node)
    {
        mapNode = node;

        node.OnVisualValueChanged += OnVisualValueChanged;
    }
    public void DisconnectEvents()
    {
        if (mapNode != null)
        {
            mapNode.OnVisualValueChanged -= OnVisualValueChanged;
        }

        mapNode = null;
    }
    public void OnVisualValueChanged()
    {

    }
}
