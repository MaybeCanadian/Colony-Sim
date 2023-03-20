using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableResource : BaseInteractable
{
    public InteractableResource(Vector3 worldPos)
    {
        type = InteractableTypes.Resource;
        Pos = worldPos;
    }

    public override void Interact()
    {
        base.Interact();
    }
    public override void RemoveInteractable()
    {
        base.RemoveInteractable();
    }
}
