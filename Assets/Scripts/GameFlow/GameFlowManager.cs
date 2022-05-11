using General;
using Managers;
using UnityEngine;
using Units.Types;

public class GameFlowManager : MonoBehaviour
{
    #region Fields
    #region Set info of enemies
    public Transform enemiesInManagerParent;
    public Transform enemiesInPoolParent;
    public EnemyType[] enemyType;
    public Transform[] spawnPositions;
    #endregion

    #region Waves manage
    Waves_SO[] waves_SO;
    public float delayToSartWave;

    float timer = 0;
    int wave = 1;
    bool levelIsOver;
    #endregion
    #endregion

    #region Methods
    #region Unity Methods
    private void Awake()
    {

    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > delayToSartWave)
        {
            SpawnEnemies(EnemyType.ArcherEnemy);
            SpawnEnemies(EnemyType.SneakyEnemy);
            levelIsOver = CheckAliveEnemies();
            timer = 0;
        }
        //DebugTool();        
    }
    #endregion

    #region Spawning enemy per wave
    public void SpawnEnemies(EnemyType enemyType)
    {
        int random = Random.Range(0, spawnPositions.Length);
        EnemyManager.Instance.Create(enemyType, new Enemy.Args(spawnPositions[random].position, enemiesInManagerParent));
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

    bool CheckAliveEnemies()
    {
        if (EnemyManager.Instance.Count == 0)
            return true;
        else
            return false;
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
