using General;
using Managers;
using UnityEngine;
using Units.Types;

public class GameFlowManager : MonoBehaviour
{
    #region Fields
    #region Set Hierarchy info of enemies
    public Transform enemiesParent;
    public Transform[] spawnPositions;
    #endregion

    #region Waves manage
    public Waves_SO[] waves_SO;
    public float delayToSartWave;
    int maxAmountsOfWaves;
    int currentWave;
    float timer;
    bool isSpawningTime;
    bool isManagerCollectionUpdated;
    #endregion
    #endregion

    #region Methods
    #region Unity Methods
    void Awake()
    {
        isSpawningTime = true;
        maxAmountsOfWaves = waves_SO.Length;
        
        foreach (Waves_SO wave_SO in waves_SO)
        {
            wave_SO.Init(enemiesParent, spawnPositions); 
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > delayToSartWave)
        {
            SpawnEnemiesPerWave();
            //CheckAliveEnemies();
            AliveEnemiesCheck();
            //DebugTool();
            timer = 0;
        }
    }
    #endregion

    #region Spawn enemies
    void SpawnEnemiesPerWave()
    {
        if (isSpawningTime)
        {
            waves_SO[currentWave].CreateEnemies();
            isSpawningTime = false;
        }
    }
    #endregion

    #region Waves manage
    void CheckAliveEnemies()
    {
        if (EnemyManager.Instance.Count == waves_SO[currentWave].NbToSpawnPerWave)
            isManagerCollectionUpdated = true;

        if (isManagerCollectionUpdated && EnemyManager.Instance.Count == 0)
        {
            LevelUp();
            isManagerCollectionUpdated = false;
        }
        else
            return;
    }

    void LevelUp()
    {
        if (currentWave <= maxAmountsOfWaves)
        {
            currentWave++;
            isSpawningTime = true;
        }
    }
    #endregion

    #region Debug Tool
    void DebugTool()
    {
        //Debug.Log("Time to spawn: " + timer);
        Debug.Log("wave " + currentWave);
        //Debug.Log("Sneaky " + waves_SO[currentWave].NbOfSneakyEnemies);
        //Debug.Log("Archer " + waves_SO[currentWave].NbOfArcherEnemies);
        //Debug.Log("Warrior " + waves_SO[currentWave].NbOfWarriorEnemies);
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
