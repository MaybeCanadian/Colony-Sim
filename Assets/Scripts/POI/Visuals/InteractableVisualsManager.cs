using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableVisualsManager : MonoBehaviour
{
    #region Member Variables
    public static InteractableVisualsManager instance;

    private GameObject InteractableParent = null;
    private List<InteractableVisuals> interActableVisuals = null;
    #endregion

    #region Init Functions
    private void Awake()
    {
        if (instance != this && instance != null)
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
        
    }
    #endregion
}
