using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableResource : BaseInteractable
{
    public InteractableResource()
    {
        type = InteractableTypes.Resource;
    }

    public override void Interact()
    {
        base.Interact();
    }
}
