using General;
using Managers;
using UnityEngine;
using Units.Types;

public class SpawnEnemyManager : MonoBehaviour
{
    #region Fields
    #region Set info of enemies
    public Transform enemiesParent;
    public EnemyType[] enemyType;
    public Transform[] spawnPositions;
    #endregion

    #region Waves manage
    public int maxAmountsOfWaves;
    public float delayToSartWave;
    public int nbToSpawnPerWave;
    int nbToSpawnPerType;
    int firstSpawningAmount;
    int nbDeadEnemies;
    float timer = 0;
    int wave = 1;
    bool levelIsOver;
    #endregion
    #endregion

    #region Methods
    #region Unity Methods
    private void Awake()
    {
        nbToSpawnPerType = nbToSpawnPerWave / enemyType.Length;
        firstSpawningAmount = nbToSpawnPerWave;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > delayToSartWave)
        {
            if (nbToSpawnPerWave > 0)
            {
                for (int i = 0; i < nbToSpawnPerType; i++)
                {
                    SpawnEnemies(EnemyType.ArcherEnemy);
                    SpawnEnemies(EnemyType.SneakyEnemy);
                }
            }
            levelIsOver = CheckAliveEnemies();
            timer = 0;
        }
        //DebugTool();
        LevelUp();
    }


    #endregion

    #region Spawning enemy per wave
    public void SpawnEnemies(EnemyType enemyType)
    {
        int random = Random.Range(0, spawnPositions.Length);
        EnemyManager.Instance.Create(enemyType, new Enemy.Args(spawnPositions[random].position, enemiesParent));
        nbToSpawnPerWave--;
    }
    #endregion

    #region Waves manage
    public void IncreaseWave(int maxWaves)
    {
        wave++;
        if (wave > maxWaves)
        {
            wave = 0;
        }
    }

    //Method to check if there's any active children in the enemiesparents object to levelup.
    bool CheckAliveEnemies()
    {
        nbDeadEnemies = 0;
        foreach (Transform child in enemiesParent)
        {
            if (child.gameObject.activeInHierarchy) continue;

            nbDeadEnemies++;
        }
        if (nbDeadEnemies == enemiesParent.childCount)
            return true;
        else
            return false;
    }

    void LevelUp()
    {
        if (levelIsOver && wave > 0)
        {
            IncreaseWave(maxAmountsOfWaves);
            nbToSpawnPerWave = firstSpawningAmount * wave;
            nbToSpawnPerType = nbToSpawnPerWave / enemyType.Length;
            levelIsOver = false;
        }
    }
    #endregion

    #region Debug Tool
    void DebugTool()
    {
        //Debug.Log("Time to spawn: " + timer);
        Debug.Log(levelIsOver);
        Debug.Log(wave);
    }
    #endregion
    #endregion
}
