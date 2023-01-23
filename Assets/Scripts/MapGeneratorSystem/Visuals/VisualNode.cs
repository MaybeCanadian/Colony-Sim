using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualNode : MonoBehaviour
{
    Vector3 nodePosition;
    GameObject tileObject;

    MapNode connectedMapNode;

    public VisualNode()
    {
        nodePosition = Vector3.zero;
        tileObject = null;
        connectedMapNode = null;
    }

    #region Visual Node Events
    private void ConnectNodeEvent()
    {
        connectedMapNode.OnNodeVisualsChangedEvent += OnNodeVisualsChanged;
        connectedMapNode.OnNodeDestroyEvent += OnConnectedNodeDestroy;
    }
    private void DisconnectNodeEvents()
    {
        connectedMapNode.OnNodeVisualsChangedEvent -= OnNodeVisualsChanged;
        connectedMapNode.OnNodeDestroyEvent -= OnConnectedNodeDestroy;
    }
    #endregion

    #region Visuals Show and Hide
    public void ShowNodeVisuals()
    {
        if(connectedMapNode == null)
        {
            Debug.LogError("ERROR - Cannot show visuals, no mapNode is connected");
            return;
        }

        if(tileObject != null)
        {
            tileObject.SetActive(true);
            return;
        }

        CreateTile();
        return;


    }
    private void CreateTile()
    {
        GameObject TileAsset = TileAssets.GetInstance().GetTileAsset(connectedMapNode.GetTileType());
        tileObject = Instantiate(TileAsset, connectedMapNode.GetNodePosition(), connectedMapNode.GetTileOrientation(), transform);
    }
    public void HideNodeVisuals()
    {
        if(tileObject == null)
        {
            Debug.LogError("ERROR - cannot hide visuals as tile Object does not exist");
            return;
        }

        tileObject.SetActive(false);
    }
    public void DestroyMapVisualS()
    {
        if(tileObject == null)
        {
            Debug.LogError("ERROR - cannot destroy the map visuals as tile Object does not exist");
            return;
        }

        Destroy(tileObject);

        tileObject = null;
    }
    public void DestroyMapVisualNode()
    {
        DisconnectNodeEvents();

        Destroy(gameObject);
    }
    #endregion

    #region Visual Node Info Functions
    public void ConnectMapNode(MapNode connected)
    {
        connectedMapNode = connected;

        ConnectNodeEvent();
    }
    public void SetNodePosition(Vector3 input)
    {
        nodePosition = input;
    }
    #endregion

    #region Connected Node Events
    private void OnConnectedNodeDestroy()
    {
        DisconnectNodeEvents();

        connectedMapNode = null;
    }
    private void OnNodeVisualsChanged()
    {
        if(tileObject == null)
        {
            return;
        }

        Destroy(tileObject);

        CreateTile();
    }
    #endregion
}
