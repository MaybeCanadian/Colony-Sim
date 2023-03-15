using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POIVisualsManager : MonoBehaviour
{
    #region Event Dispatchers

    #endregion

    #region Member Variables
    public static POIVisualsManager instance;

    private GameObject POIParent = null;
    private List<POIVisuals> poiVisualList = null;
    #endregion

    #region Init Functions
    private void Awake()
    {
        if (instance != this && instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            Init();
        }
    }
    private void Init()
    {
        SetUpVisualsList();
        SetUpParentOBJ();

        ConnectEvents();
    }
    private void SetUpVisualsList()
    {
        poiVisualList = new List<POIVisuals>();
    }
    private void SetUpParentOBJ()
    {
        POIParent = new GameObject();
        POIParent.name = "[POI]";
    }
    private void ConnectEvents()
    {
        POIManager.OnPOIAdded += OnPOIAdded;
    }
    private void DisconnectEvents()
    {
        POIManager.OnPOIAdded -= OnPOIAdded;
    }
    #endregion

    #region EventRecievers
    private void OnPOIAdded(POI poi)
    {
        GameObject visualsOBJ = new GameObject();

        visualsOBJ.transform.SetParent(POIParent.transform);

        POIVisuals visuals = visualsOBJ.AddComponent<POIVisuals>();

        visuals.OnPOIVisualsRemoved += OnPOIVisualsRemoved;

        poiVisualList.Add(visuals);
    }
    private void OnPOIVisualsRemoved(POIVisuals visuals)
    {
        visuals.OnPOIVisualsRemoved -= OnPOIVisualsRemoved;

        poiVisualList.Remove(visuals);
    }
    #endregion

    #region Visuals Control
    public void CreatePOIVisuals()
    {
        foreach(POIVisuals visuals in poiVisualList)
        {
            visuals.CreateVisuals();
        }
    }
    public void DestroyPOIVisuals(bool fullDestory = false)
    {
        foreach(POIVisuals visuals in poiVisualList)
        {
            visuals.DestroyVisuals();

            if(fullDestory)
            {
                Destroy(visuals);
            }
        }
    }
    #endregion
}
