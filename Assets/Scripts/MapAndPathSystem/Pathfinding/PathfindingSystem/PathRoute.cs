using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[System.Serializable]
public class PathRoute
{
    List<PathFindingNode> routeNodes;

    public PathRoute()
    {
        routeNodes = new List<PathFindingNode>();
    }

    public void AddPathPoint(PathFindingNode node)
    {
        routeNodes.Add(node);
    }
    public void FlipPath()
    {
        routeNodes.Reverse();
    }
    public void PrintPath()
    {
        string path = "Full Path is: ";

        foreach(PathFindingNode node in routeNodes) 
        { 
            path += "x: " + node.getNodeGridPos().x + " y: " + node.getNodeGridPos().y + " - ";
        }

        Debug.Log(path);
    }
    public bool CheckIndexInRange(int index)
    {
        if (routeNodes.Count > index)
        {
            return true;
        }

        return false;
    }
    public Vector3 GetPosOfRouteIndex(int index)
    {
        if (routeNodes.Count > index)
        {
            return routeNodes[index].GetNodeWorldPosition();
        }

        Debug.LogError("ERROR - Index is out of route range");
        return Vector3.zero;
    }
}
