using General;
using Managers;
using UnityEngine;
using Units.Types;

public class GameFlowManager : MonoBehaviour
{
    #region Fields
    #region Set info of enemies
    public Transform enemiesParent;
    public Transform[] spawnPositions;
    #endregion

    #region Waves manage
    public Waves_SO[] waves_SO;
    public float delayToSartWave;
    float timer;
    int wave;
    bool isSpawningDone;
    private int maxAmountsOfWaves;
    #endregion
    #endregion

    #region Methods
    #region Unity Methods

    private void Awake()
    {
        maxAmountsOfWaves = waves_SO.Length;
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > delayToSartWave)
        {
            SpawnEnemiesPerWave();
            CheckAliveEnemies();
            timer = 0;
            //DebugTool();
        }
    }
    #endregion

    #region Spawn enemies
    void SpawnEnemiesPerWave()
    {
        if (!isSpawningDone)
        {
            isSpawningDone = true;
            SpawnEnemies(EnemyType.ArcherEnemy, waves_SO[wave].NbOfArcherEnemies);
            SpawnEnemies(EnemyType.SneakyEnemy, waves_SO[wave].NbOfSneakyEnemies);
            SpawnEnemies(EnemyType.WarriorEnemy, waves_SO[wave].NbOfWarriorEnemies);
        }
    }

    void SpawnEnemies(EnemyType enemyType, int nbToSpawn)
    {
        for (int i = 0; i < nbToSpawn; i++)
        {
            int random = Random.Range(0, spawnPositions.Length);
            EnemyManager.Instance.Create(enemyType, new Enemy.Args(spawnPositions[random].position, enemiesParent));
        }
    }
    #endregion

    bool x;
    #region Waves manage
    void CheckAliveEnemies()
    {
        if (EnemyManager.Instance.Count == waves_SO[wave].NbToSpawnPerWave)
            x = true;

        if (x && EnemyManager.Instance.Count == 0 && wave > -1)
        {
            LevelUp();
            x = false;
        }

        else
            return;
    }
    int nbDeadEnemies;
    //void CheckAliveEnemies()
    //{
    //    nbDeadEnemies = 0;
    //    foreach (Transform child in enemiesParent)
    //    {
    //        if (child.gameObject.activeInHierarchy) continue;

    //        nbDeadEnemies++;
    //    }
    //    if (nbDeadEnemies == enemiesParent.childCount)
    //        LevelUp() ;
    //    else
    //        return ;
    //}

    void LevelUp()
    {
        wave++;
        if (wave > maxAmountsOfWaves)
        {
            wave = -1;
        }
        isSpawningDone = false;
    }

    #endregion

    #region Debug Tool
    void DebugTool()
    {
        //Debug.Log("Time to spawn: " + timer);
        Debug.Log("wave " + wave);
        Debug.Log("Sneaky " + waves_SO[wave].NbOfSneakyEnemies);
        Debug.Log("Archer " + waves_SO[wave].NbOfArcherEnemies);
        Debug.Log("Warrior " + waves_SO[wave].NbOfWarriorEnemies);
    }
    #endregion
    #endregion
}
