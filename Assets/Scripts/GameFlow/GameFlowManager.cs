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
    public int maxAmountsOfWaves;
    public float delayToSartWave;
    float timer;
    int wave;
    bool isSpawningDone;
    #endregion
    #endregion

    #region Methods
    #region Unity Methods
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
            SpawnEnemies(EnemyType.ArcherEnemy, waves_SO[wave].NbOfArcherEnemies);
            SpawnEnemies(EnemyType.SneakyEnemy, waves_SO[wave].NbOfSneakyEnemies);
            SpawnEnemies(EnemyType.WarriorEnemy, waves_SO[wave].NbOfWarriorEnemies);
            isSpawningDone = true;
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

    #region Waves manage
    void CheckAliveEnemies()
    {
        if (EnemyManager.Instance.Count == 0)
            LevelUp();
        else
            return;
    }

    void LevelUp()
    {
        if (wave > -1)
        {
            IncreaseWave(maxAmountsOfWaves);
            isSpawningDone = false;
        }
    }

    void IncreaseWave(int maxWaves)
    {
        wave++;
        if (wave > maxWaves)
        {
            wave = -1;
        }
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
