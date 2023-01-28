using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public static class PathingSystem
{
    #region Path Determination
    public static PathingPath GetPathFromNodeToNode(PathFindingNode start, PathFindingNode end)
    {
        PathingPath path = new PathingPath();

        MapObject map = MapGenerator.GetMap();

        if (start == end)
        {
            path.AddNodeToPath(end);
        }

        return path;
    }
    public static PathingPath GetPathFromPosToNode(Vector3 start, PathFindingNode end)
    {
        PathingPath path = new PathingPath();

        MapObject map = MapGenerator.GetMap();

        PathFindingNode startNode = map.GetNodeByPos(start).GetPathNode();

        if (startNode == end)
        {
            path.AddNodeToPath(end);
        }

        return path;
    }
    public static PathingPath GetPathFromNodeToPos(PathFindingNode start, Vector3 end)
    {
        PathingPath path = new PathingPath();

        MapObject map = MapGenerator.GetMap();

        PathFindingNode endNode = map.GetNodeByPos(end).GetPathNode();

        if (start == endNode)
        {
            path.AddNodeToPath(endNode);
        }

        return path;
    }
    public static PathingPath GetPathFromPosToPos(Vector3 start, Vector3 end)
    {
        PathingPath path = new PathingPath();

        MapObject map = MapGenerator.GetMap();

        PathFindingNode endNode = map.GetNodeByPos(end).GetPathNode();

        PathFindingNode startNode = map.GetNodeByPos(start).GetPathNode();

        if (startNode == endNode)
        {
            path.AddNodeToPath(endNode);
        }

        return path;
    }
    #endregion
}
