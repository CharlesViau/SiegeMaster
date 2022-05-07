using General;
using Managers;
using UnityEngine;
using Units.Types;
using System.Linq;

public class SpawnEnemyManager : MonoBehaviour
{
    public Transform enemiesParent;
    public EnemyType[] enemyType;
    public Transform[] spawnPositions;
    public float delayTimeToSpawn;
    public int nbToSpawnPerWave;
    int nbToSpawnPerType;
    int wave = 1;
    float timer = 0;
    bool levelIsOver;

    private void Awake()
    {
        nbToSpawnPerType = nbToSpawnPerWave / enemyType.Length;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > delayTimeToSpawn)
        {
            if (nbToSpawnPerWave > 0)
            {
                for (int i = 0; i < nbToSpawnPerType; i++)
                {
                    SpawnEnemies(EnemyType.ArcherEnemy);
                    SpawnEnemies(EnemyType.SneakyEnemy);
                }
            }
            timer = 0;
            levelIsOver = CheckAliveEnemies();
        }

        Debug.Log(levelIsOver);
        Debug.Log(wave);
        if (levelIsOver && wave > 0)
        {
            IncreaseWave();
            nbToSpawnPerWave = (nbToSpawnPerType * enemyType.Length) * wave;
            nbToSpawnPerType = nbToSpawnPerWave / enemyType.Length;
            levelIsOver = false;
        }
    }

    public void SpawnEnemies(EnemyType enemyType)
    {
        int random = Random.Range(0, spawnPositions.Length);
        EnemyManager.Instance.Create(enemyType, new Enemy.Args(spawnPositions[random].position, enemiesParent));
        nbToSpawnPerWave--;
    }

    public void IncreaseWave()
    {
        wave++;
        if (wave > 3)
        {
            wave = 0;
        }
    }

    // Method to check if there's any active children in the enemiesparents object to levelup.
    // needs to modifing
    bool CheckAliveEnemies()
    {
        foreach (Transform child in enemiesParent)
        {
            if (child.gameObject.activeInHierarchy) continue;

            return true;
        }
        return false;

        //levelIsOver = enemiesParent.Cast<Transform>().All(child => child.gameObject.activeInHierarchy);
        //return levelIsOver;
    }
}
