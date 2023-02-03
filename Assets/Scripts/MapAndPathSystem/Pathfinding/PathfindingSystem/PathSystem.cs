using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public static class PathSystem
{
   public static void FindPathBetweenTwoNodes(PathFindingNode start, PathFindingNode end)
    {
        PathOperation newPath = new PathOperation(-1, start, end);

        newPath.StartPathingOperation(out PathRoute route);
    }
}
