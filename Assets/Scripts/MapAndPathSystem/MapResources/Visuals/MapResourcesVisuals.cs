using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapResourcesVisuals : MonoBehaviour
{
    #region Event Dispactchers
    public delegate void ResourceVisualDisconnectedEvent(MapResourcesVisuals visuals);
    public ResourceVisualDisconnectedEvent OnResourceVisualDisconneted;
    #endregion

    MapResource connectedResource = null;
    GameObject resourceOBJ = null;

    #region Init Functions
    public void ConnectResourceNode(MapResource resource)
    {
        if(connectedResource != null)
        {
            DisconnectEvents();

            connectedResource = null;
        }        

        connectedResource = resource;

        ConnectEvents();
    }
    public void DisconnetFromResourceNode()
    {
        if(connectedResource == null)
        {
            Debug.LogError("ERROR - Could not disconnect from resource as resource is already null.");
            return;
        }

        DisconnectEvents();

        connectedResource = null;

        OnResourceVisualDisconneted?.Invoke(this);
    }
    private void ConnectEvents()
    {
        if(connectedResource == null)
        {
            Debug.LogError("ERROR - Could not connect events as connected resource is null");
            return;
        }

        connectedResource.OnResourceDestroy += ConnectedResourceDestory;

    }
    private void DisconnectEvents()
    {
        if(connectedResource == null)
        {
            Debug.LogError("ERROR - Could not disconnect events as the connected resource was null");
            return;
        }

        connectedResource.OnResourceDestroy -= ConnectedResourceDestory;
    }
    #endregion

    #region LifeCycle
    private void ConnectedResourceDestory()
    {
        DisconnectEvents();

        connectedResource = null;

        OnResourceVisualDisconneted?.Invoke(this);
    }
    #endregion

    #region Visuals
    public void CreateVisuals()
    {
        if(resourceOBJ != null)
        {
            DestroyVisuals();
        }

        GameObject resourceModel = new GameObject(); // we need to get the model

        resourceOBJ = Instantiate(resourceModel);
        resourceOBJ.transform.SetParent(transform);
    }
    public void DestroyVisuals()
    {
        if(resourceOBJ == null)
        {
            Debug.LogError("ERROR - Could not destroy resource visuals as the visuals object is null");
            return;
        }

        Destroy(resourceOBJ);
        resourceOBJ = null;
    }
    #endregion
}
