using Managers;
using UnityEngine;
using Units.Types;
using System.Linq;

public class FakeEntry : MonoBehaviour
{
    public Transform spawnPos;

    EnemyType[] enemyTypes;

    int nbArchertoSpawn = 1;
    int nbSneakyToSpawn = 2;

    private int TotalToSpawn => nbArchertoSpawn + nbSneakyToSpawn;
    float spawnSpeed = 3;
    float timer;

    void Awake()
    {
        enemyTypes = System.Enum.GetValues(typeof(EnemyType)).Cast<EnemyType>().ToArray();
    }

    // I will reform this code
    private void Update()
    {
        timer += Time.deltaTime;
        float random = Random.Range(0.0f, 20.0f);

        if (timer < spawnSpeed || TotalToSpawn == 0) 
            return;


        EnemyType randomType = (EnemyType)Random.Range(0, enemyTypes.Length - 1);

        if(randomType == EnemyType.Archer && nbArchertoSpawn == 0)
        {
            randomType = EnemyType.Sneaky;
        }

        if(randomType == EnemyType.Sneaky && nbSneakyToSpawn == 0)
        {
            randomType = EnemyType.Archer;
        }

        EnemyManager.Instance.Create(randomType, new Enemy.Args(random * spawnPos.position));

        switch (randomType)
        {
            case EnemyType.Archer:
                nbArchertoSpawn--;
                break;
            case EnemyType.Sneaky:
                nbSneakyToSpawn--;
                break;
        }

        timer = 0;
    }
}
