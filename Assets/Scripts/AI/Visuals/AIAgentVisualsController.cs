using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAgentVisualsController : MonoBehaviour
{
    public static AIAgentVisualsController instance;

    [Header("Parents")]
    public GameObject AIParent = null;

    #region Init Functions
    private void Awake()
    {
        if(instance != this && instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;

            Init();
        }
    }
    private void Init()
    {
        SetUpAIParentOBJ();
    }
    private void SetUpAIParentOBJ()
    {
        if(AIParent != null)
        {
            return;
        }

        AIParent = new GameObject();
        AIParent.name = "[AI]";
    }
    #endregion
}
