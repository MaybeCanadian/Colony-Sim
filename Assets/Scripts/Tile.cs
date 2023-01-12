using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    MapNode ParentNode;

    public void ConnectToParent(MapNode node)
    {
        ParentNode = node;
    }

}
