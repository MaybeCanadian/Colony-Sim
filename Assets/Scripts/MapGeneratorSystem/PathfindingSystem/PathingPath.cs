using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PathingPath
{
    List<PathFindingNode> path;

    public PathingPath()
    {
        path = new List<PathFindingNode>();
    }

    #region Path Add / Remove Functions
    public void AddNodeToPath(PathFindingNode node)
    {
        path.Add(node);
    }
    public void RemoveNodeFromPath(PathFindingNode node)
    {
        if(!path.Contains(node))
        {
            Debug.LogError("ERROR - Path does not contain node to remove");
            return;
        }

        path.Remove(node);
        return;
    }
    #endregion

    #region Path Info Functions
    public int GetPathLength()
    {
        return path.Count;
    }
    #endregion
}
