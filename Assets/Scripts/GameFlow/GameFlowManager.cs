using General;
using Managers;
using UnityEngine;
using Units.Types;

public enum GameState { WaitToSpawn, Spawn, CheckAliveEnemies, LevelUp }
public class GameFlowManager : MonoBehaviour
{
    #region Fields
    #region Set Hierarchy info of enemies
    public Transform enemiesParent;
    public Transform[] spawnPositions;
    #endregion

    #region Waves manage
    GameState gameState;
    public Waves_SO[] waves_SO;
    public float delayToSartWave;
    int maxAmountsOfWaves;
    int currentWave;
    float timer;
    #endregion
    #endregion
    
    #region Methods
    #region Unity Methods
    void Awake()
    {
        maxAmountsOfWaves = waves_SO.Length;
        gameState = GameState.WaitToSpawn;

        foreach (Waves_SO wave_SO in waves_SO)
        {
            wave_SO.Init(enemiesParent, spawnPositions);
        }
    }

    void Update()
    {
        switch (gameState)
        {
            case GameState.Spawn:
                SpawnEnemies();
                break;
            case GameState.CheckAliveEnemies:
                CheckAliveEnemies();
                break;
            case GameState.WaitToSpawn:
                Breakdown();
                break;
            case GameState.LevelUp:
                LevelUp();
                break;
            default:
                break;
        }
        //DebugTool();
    }
    #endregion

    #region Waves Manage
    void Breakdown()
    {
        timer += Time.deltaTime;
        if (timer > delayToSartWave)
        {
            timer = 0;
            gameState = GameState.Spawn;
        }
    }

    void SpawnEnemies()
    {
        waves_SO[currentWave].CreateEnemies();
        gameState = GameState.CheckAliveEnemies;
    }

    void CheckAliveEnemies()
    {
        if (EnemyManager.Instance.Count == 0)
            gameState = GameState.LevelUp;
    }

    void LevelUp()
    {
        if (currentWave <= maxAmountsOfWaves)
        {
            currentWave++;
            gameState = GameState.WaitToSpawn;
        }
    }
    #endregion

    #region Debug Tool
    void DebugTool()
    {
        //Debug.Log("time to spawn: " + timer);
        Debug.Log("wave " + currentWave);        
    }

    int nbDeadEnemies;
    void AliveEnemiesCheck()
    {
        nbDeadEnemies = 0;
        foreach (Transform child in enemiesParent)
        {
            if (child.gameObject.activeInHierarchy) continue;

            nbDeadEnemies++;
        }
        if (nbDeadEnemies == enemiesParent.childCount)
            LevelUp();
        else
            return;
    }
    #endregion
    #endregion
}
