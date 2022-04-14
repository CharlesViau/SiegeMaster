using Managers;
using UnityEngine;
using Unit.Types;

public class FakeEntry : MonoBehaviour
{
    public Transform spawnPos;
    void Start()
    {
        for (int i = 0; i < 2; i++)
        { 
            EnemyManager.Instance.Create(EnemyType.Archer, new Enemy.Args(spawnPos.position));
            EnemyManager.Instance.Create(EnemyType.Sneaky, new Enemy.Args(spawnPos.position));
        }
    }
}
