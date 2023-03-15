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

    }
    public void DestroyVisuals()
    {

    }
    #endregion
}
