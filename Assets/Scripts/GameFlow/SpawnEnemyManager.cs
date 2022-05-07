using General;
using Managers;
using UnityEngine;
using Units.Types;
using System.Linq;
public class SpawnEnemyManager : MonoBehaviour
{
    public Transform enemiesParent;
    public Transform[] spawnPositions;
    public EnemyType[] enemyType;

    public float timeToSpawn;
    float timer = 0;

    private void Start()
    {

    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > timeToSpawn)
        {
            int random = Random.Range(0, spawnPositions.Length);
            for (int i = 0; i < enemyType.Length; i++)
            {
                EnemyManager.Instance.Create(enemyType[i], new Enemy.Args(spawnPositions[random].position, enemiesParent));
            }
            timer = 0;
        }
    }
}
