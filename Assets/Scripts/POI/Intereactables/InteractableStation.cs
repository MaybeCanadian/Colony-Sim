using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableStation : BaseInteractable
{
    public InteractableStation(Vector3 worldPos)
    {
        type = InteractableTypes.Station;
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
