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

    public delegate void VisualValueChanged();
    public VisualValueChanged OnVisualValueChanged;
}
