using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POIVisuals : MonoBehaviour
{
    #region Event Dispatchers
    public delegate void POIVisualsRemovedEvent(POIVisuals visuals);
    public POIVisualsRemovedEvent OnPOIVisualsRemoved;
    #endregion

    #region Member Variable
    private POI connectedPOI = null;

    private GameObject visualOBJ = null;
    #endregion

    #region Init Functions
    public void ConnectPOI(POI poi)
    {
        connectedPOI = poi;
    }
    public void ConnectEvents()
    {
        if(connectedPOI == null)
        {
            Debug.LogError("ERROR - Connected POI was null, could not connect events.");
            return;
        }
    }
    public void DisconnectEvents()
    {
        if(connectedPOI == null)
        {
            Debug.LogError("ERROR - Connected POI was null, could not disconnect events.");
            return;
        }
    }
    #endregion

    #region Lifecycle
    public void RemoveVisuals()
    {
        if(connectedPOI == null)
        {
            Debug.LogError("ERROR - Connected POI was null, could not remove");
            return;
        }

        DisconnectEvents();

        OnPOIVisualsRemoved?.Invoke(this);

        return;
    }
    #endregion

    #region Visuals Control
    public void CreateVisuals()
    {
        if(connectedPOI == null)
        {
            Debug.LogError("ERROR - Could not make visuals, connected poi is null");
            return;
        }

        if(visualOBJ != null)
        {
            DestroyVisuals();
        }

        GameObject model = POIDataBase.GetModel(connectedPOI.model);

        visualOBJ = Instantiate(model);

        visualOBJ.transform.SetParent(transform);
        visualOBJ.name = connectedPOI.POIName;
    }
    public void DestroyVisuals()
    {
        if(visualOBJ != null)
        {
            Destroy(visualOBJ);
        }

        visualOBJ = null;

        return;
    }
    #endregion
}
