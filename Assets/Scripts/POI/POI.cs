using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POI
{
    #region Event Dispatchers
    public delegate void POIRemoveEvent();
    public POIRemoveEvent OnPOIRemove;

    public delegate void InteractableAddedEvent(BaseInteractable added);
    public InteractableAddedEvent OnInteractableAdded;
    #endregion

    public Vector2Int connectedGridPos = Vector2Int.zero;
    public Vector3 POIworldPos = Vector3Int.zero;

    public POICategories category = POICategories.Default;
    public POIModelList model = POIModelList.NULL;

    public string POIName = "POI";

    public List<BaseInteractable> interactables;

    #region Init Functions
    public POI(Vector2Int gridPOs)
    {
        connectedGridPos = gridPOs;
        interactables = new List<BaseInteractable>();

        //find connected worldpos
    }
    public POI(Vector3 worldPos)
    {
        POIworldPos = worldPos;

        //find connected gridpos
    }
    public POI(Vector2Int gridPos, Vector3 worldPos)
    {
        connectedGridPos = gridPos;
        POIworldPos = worldPos;
    }
    #endregion

    #region Lifecycle
    public void RemvoePOI()
    {


        OnPOIRemove?.Invoke();
    }
    #endregion

    #region Distance
    public float FindDistanceToWorldPos(Vector3 worldPos)
    {
        float dist;

        dist = (worldPos - POIworldPos).magnitude;

        return dist;
    }
    #endregion

    #region Interactables
    public void AddInteractable(Vector3 spawnPosition, InteractableTypes type)
    {
        BaseInteractable newInteractable = null;

        switch(type)
        {
            case InteractableTypes.None:
                Debug.LogError("ERROR - Can not make an interactable of type None");
                return;
            case InteractableTypes.Resource:
                newInteractable = new InteractableResource(spawnPosition);
                break;
            case InteractableTypes.Station:
                newInteractable = new InteractableStation(spawnPosition);
                break;
        }

        if(newInteractable == null)
        {
            return;
        }

        interactables.Add(newInteractable);

        OnInteractableAdded?.Invoke(newInteractable);
    }
    public void RemoveInteractable()
    {

    }
    #endregion
}
