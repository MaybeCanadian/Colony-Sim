using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableStation : BaseInteractable
{
    public InteractableStation()
    {
        type = InteractableTypes.Station;
    }
    public override void Interact()
    {
        base.Interact();
    }
}
