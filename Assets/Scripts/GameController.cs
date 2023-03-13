using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    #region Event Dispatchers
    public delegate void GameUpdate(float deltaTime);
    public static GameUpdate OnGameUpdate;

    public delegate void GameFixedUpdate(float fixedDeltaTime);
    public static GameFixedUpdate OnGameFixedUpdate;

    public delegate void GameLateUpdate(float deltaTime);
    public static GameLateUpdate OnGameLateUpdate;
    #endregion

    #region Event Senders
    private void Update()
    {
        OnGameUpdate?.Invoke(Time.deltaTime);
    }
    private void FixedUpdate()
    {
        OnGameFixedUpdate?.Invoke(Time.fixedDeltaTime);
    }
    private void LateUpdate()
    {
        OnGameLateUpdate?.Invoke(Time.deltaTime);
    }
    #endregion

    [SerializeField]
    private MapObject map = null;

    [SerializeField]
    private int mapWidth = 10;
    [SerializeField]
    private int mapHeight = 10;

    [SerializeField]
    private float XTileOffset = 1.0f;
    [SerializeField]
    private float YTileOffset = 1.0f;

    [SerializeField]
    private GridType tileGridType = GridType.HEX;
    [SerializeField]
    private GridType chunkGridType = GridType.SQUARE;

    public int agentsToMake = 100;

    private void Start()
    {
        MapGenerator.GenerateMap(tileGridType, chunkGridType, mapWidth, mapHeight, XTileOffset, YTileOffset);
        map = MapGenerator.GetMap();

        Debug.Log(map.GetNodeCount());

        MapVisualsController.instance.CreateMapVisuals();

        for(int i = 0; i < agentsToMake; i++)
        {
            Vector2Int pos = Vector2Int.zero;
            pos.x = Random.Range(0, map.GetMapWidth());
            pos.y = Random.Range(0, map.GetMapHeight());

            AIAgentManager.SpawnNewAgentAtNodePos(pos);
        }
        

        AIAgentVisualsController.instance.CreateAIVisuals();

        AIAgentManager.AllRandomPath();

        Debug.Log(AIAgentManager.GetAgentCount());
    }
}
