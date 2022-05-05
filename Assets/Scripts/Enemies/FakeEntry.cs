using Managers;
using UnityEngine;
using Units.Types;
using System.Linq;

public class FakeEntry : MonoBehaviour
{
    public Transform spawnPos;

    EnemyType[] enemyTypes;

    public int nbArcherToSpawn = 5;
    public int nbSneakyToSpawn = 5;

    int TotalToSpawn => nbArcherToSpawn + nbSneakyToSpawn;
    public float spawnSpeed = 3;
    float timer;

    void Awake()
    {
        enemyTypes = System.Enum.GetValues(typeof(EnemyType)).Cast<EnemyType>().ToArray();
    }

    // I will reform this code
    private void Update()
    {
        timer += Time.deltaTime;
        float random = Random.Range(5.0f, 20.0f);

        if (timer < spawnSpeed || TotalToSpawn == 0)
            return;

        EnemyType randomType = (EnemyType)Random.Range(0, enemyTypes.Length);

        if (randomType == EnemyType.ArcherEnemy && nbArcherToSpawn == 0)
        {
            randomType = EnemyType.ArcherEnemy;
        }

        if (randomType == EnemyType.SneakyEnemy && nbSneakyToSpawn == 0)
        {
            randomType = EnemyType.SneakyEnemy;
        }

        EnemyManager.Instance.Create(randomType, new Enemy.Args(spawnPos.position));

        switch (randomType)
        {
            case EnemyType.ArcherEnemy:
                nbArcherToSpawn--;
                break;
            case EnemyType.SneakyEnemy:
                nbSneakyToSpawn--;
                break;
        }

        timer = 0;
    }
}
