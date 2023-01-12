using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    #region Members
    MapNode ParentNode;

    #endregion

    #region Methods
    public void ConnectToParent(MapNode node)
    {
        ParentNode = node;
    }
    #endregion
}
