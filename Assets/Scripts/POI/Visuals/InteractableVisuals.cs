using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableVisuals : MonoBehaviour
{
    public BaseInteractable connectedBaseInteractable = null;
    public GameObject interactableObject = null;

    #region Init Functions
    public void ConnectInteractable(BaseInteractable interactable)
    {
        connectedBaseInteractable = interactable;

        ConnectEvents();
    }
    private void ConnectEvents()
    {
        if(connectedBaseInteractable == null)
        {
            Debug.LogError("ERROR - Could not connect events as connected interactable is null.");
            return; 
        }

        connectedBaseInteractable.OnInteractableRemoved += DestroyVisual;
    }
    private void DisconnectEvents()
    {
        if(connectedBaseInteractable == null)
        {
            Debug.LogError("ERROR - Could not disconnect events as connected interactable is null");
            return;
        }

        connectedBaseInteractable.OnInteractableRemoved-= DestroyVisual;
    }
    #endregion

    #region Visuals
    public void CreateInteractableVisuals()
    {
        if(interactableObject != null)
        {
            DestroyInteractableVisual();
        }

        interactableObject = new GameObject();
        interactableObject.name = connectedBaseInteractable.interactableName + " visual's";
        interactableObject.transform.SetParent(transform);
    }
    public void DestroyInteractableVisual()
    {
        if(interactableObject == null)
        {
            Debug.LogError("ERROR - Could not remove interactable visuals as the object is already null");
            return;
        }

        Destroy(interactableObject);
        interactableObject = null;
    }
    #endregion

    #region Lifecycle
    private void DestroyVisual()
    {
        DisconnectEvents();

        connectedBaseInteractable = null;

        DestroyInteractableVisual();
    }
    #endregion
}
