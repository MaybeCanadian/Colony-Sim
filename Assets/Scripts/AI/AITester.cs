using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AITester : MonoBehaviour
{
    public void OnPathfindingTest(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            AIAgentManager.AllRandomPath();
        }
    }
}
