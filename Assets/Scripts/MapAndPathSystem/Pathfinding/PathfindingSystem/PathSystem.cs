using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public static class PathSystem
{
   public static bool FindPathBetweenTwoNodes(PathFindingNode start, PathFindingNode end, out PathRoute route)
    {
        route = null;

        if(start == end)
        {
            Debug.LogError("ERROR - Given start and end are the same");
            return false;
        }

        PathOperation newPath = new PathOperation(-1, start, end);

        if(!newPath.StartPathingOperation(out route))
        {
            return false;
        }

        route.PrintPath();
        return true;
    }
}
