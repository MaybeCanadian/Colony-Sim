using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AIPathFindingAgent
{
    #region Event Dispatchers
    public delegate void AIAgentMoveEvent(Vector3 newPos);
    public AIAgentMoveEvent OnAgentMove;
    #endregion

    [Header("Position Data")]
    public Vector2Int currentGridPos = Vector2Int.zero;
    public Vector3 currentWorldPosition = Vector3.zero;

    [Header("Pathfinding")]
    public PathRoute currentRoute = null;
    private int currentRouteIndex = -1;
    private bool hasTarget = false;
    private bool hasRoute = false;
    private Vector3 currentTargetWorldPos = Vector3.zero;
    private float pathfindingTolerance = 0.1f;

    [Header("Physics")]
    private Vector3 velocity = Vector3.zero;
    private float moveForce = 1.0f;
    private float maxSpeed = 10.0f;
    private float deltaTime;
    private float fixedDeltaTime;
    private static float friction = 0.9f;
    private static float movementTollerance = 0.05f;

    #region Lifecycle
    public AIPathFindingAgent(Vector2Int startingGridPos)
    {
        currentGridPos = startingGridPos;
        velocity = Vector3.zero;
        currentTargetWorldPos = Vector3.zero;
        hasTarget = false;
        hasRoute = false;

        deltaTime = 0.0f;
        fixedDeltaTime = 0.0f;

        CalculateWorldPos();
    }
    public void DestoryPathfindingAgent()
    {
        //agent = null;
    }
    #endregion

    #region Position Data
    public Vector3 GetWorldPos()
    {
        return currentWorldPosition;
    }
    private void CalculateWorldPos()
    {
        Vector3 worldPos = Vector3.zero;

        MapNode currentNode = MapGenerator.GetMap().GetMapNode(currentGridPos.x, currentGridPos.y);

        worldPos = currentNode.GetNodePosition();

        worldPos += new Vector3(0.0f, 1.05f, 0.0f);

        currentWorldPosition = worldPos;
    }
    private void FindNextTargetPos()
    {
        if(hasRoute == false)
        {
            Debug.LogError("ERROR - We can't find the next target pos on route as we dont have a target");
            return;
        }

        if (currentRoute == null)
        {
            Debug.LogError("ERROR - Could not get path point as the route is null");
            hasRoute = false;
            return;
        }

        currentRouteIndex++;

        if (currentRoute.CheckIndexInRange(currentRouteIndex) == false)
        {
            Debug.Log("We have arrived");
            hasRoute = false;
            hasTarget = false;
            return;
        }

        currentTargetWorldPos = currentRoute.GetPosOfRouteIndex(currentRouteIndex);
        //Debug.Log("current target pos is " + currentTargetWorldPos);
        hasTarget = true;
        return;
    }
    private void UpdateMovementVelocity()
    {
        if(hasRoute == false)
        {
            Debug.LogError("ERROR - AI does not have a Route.");
            return;
        }

        if (hasTarget == false)
        {
            Debug.LogError("ERROR - AI does not have a Target");
            return;
        }

        Vector3 directionVector = (currentTargetWorldPos - currentWorldPosition).normalized;

        //Debug.Log("distance to target is " + (currentTargetWorldPos - currentWorldPosition).magnitude);

        velocity += directionVector * moveForce * fixedDeltaTime;

        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        return;
    }
    private bool CheckIfAtCurrentTarget()
    {
        if(hasRoute == false)
        {
            Debug.LogError("ERROR - AI agent does not have a route");
            return false;
        }

        if((currentTargetWorldPos - currentWorldPosition).magnitude < pathfindingTolerance == false)
        {
            return false;
        }

        //at target
        hasTarget = false;
        return true;
    }
    private void MoveByVelocity()
    {
        if(velocity.magnitude < movementTollerance)
        {
            return;
        }

        currentWorldPosition += velocity * fixedDeltaTime;

        velocity *= friction;

        OnAgentMove?.Invoke(currentWorldPosition);

        //Debug.Log(currentWorldPosition);

        return;
    }
    #endregion

    #region Update Functions
    public void PathfindingUpdate(float dt)
    {
        deltaTime = dt;
    }
    public void PathfindingFixedUpdate(float fdt)
    {
        //Debug.Log("Tick");

        fixedDeltaTime = fdt;

        if(hasRoute)
        {
            if(CheckIfAtCurrentTarget())
            {
                FindNextTargetPos();
            }

            if (hasTarget)
            {
                UpdateMovementVelocity();
            }
            else
            {
                PathToRandomLocation();
            }
        }

        MoveByVelocity();
    }
    public void PathfindingLateUpdate(float dt)
    {
        deltaTime = dt;
    }
    #endregion

    #region Debug
    public void PathToRandomLocation()
    {
        Debug.Log("Finding random path");

        Vector2Int targetPos = Vector2Int.zero;

        MapObject map = MapGenerator.GetMap();

        targetPos.x = Random.Range(0, map.GetMapWidth());
        targetPos.y = Random.Range(0, map.GetMapHeight());

        PathFindingNode currentNode = map.GetMapNode(currentGridPos).GetPathNode();

        PathFindingNode targetNode = map.GetMapNode(targetPos).GetPathNode();

        if(PathSystem.FindPathBetweenTwoNodes(currentNode, targetNode, out currentRoute) == false)
        {
            Debug.Log("Could not find a path, very sadge");
            return;
        }

        Debug.Log("Pathing to location " + targetPos);
        currentRouteIndex = -1;
        hasRoute = true;

        FindNextTargetPos();
    }
    #endregion
}
