using System.Collections;
using System.Collections.Generic;
using Units.Types;
using UnityEngine;

public class FakeEntry : MonoBehaviour
{
    public Transform spawnPos;
    void Start()
    {
        for (int i = 0; i < 20; i++)
        { 
            EnemyManager.Instance.Create(EnemyType.Archer, new Enemy.Args(spawnPos.position));
        }
    }

    void Update()
    {

    }
}
