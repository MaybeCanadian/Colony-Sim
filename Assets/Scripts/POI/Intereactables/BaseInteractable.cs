using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseInteractable
{
    #region Event Dispatchers
    public delegate void InteractableRemovedEvent();
    public InteractableRemovedEvent OnInteractableRemoved;
    #endregion

    public InteractableTypes type;
    public Vector3 Pos = Vector3.zero;

    public virtual void Interact() { }

    public virtual void RemoveInteractable()
    {
        OnInteractableRemoved?.Invoke();
    }
}
