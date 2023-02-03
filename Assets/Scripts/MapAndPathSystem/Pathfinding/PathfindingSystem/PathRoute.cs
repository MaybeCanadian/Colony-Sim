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
    public void PrintPath()
    {
        string path = "Full Path is: ";

        for(int i = routeNodes.Count - 1; i >= 0; i--)
        {
            path += "x: " + routeNodes[i].getNodeGridPos().x + " y: " + routeNodes[i].getNodeGridPos().y + " - ";
        }

        Debug.Log(path);
    }
}
