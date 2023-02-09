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

    private void Start()
    {
        
    }
}
