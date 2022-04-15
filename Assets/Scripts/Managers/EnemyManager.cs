using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;
using System.Linq;
using Unit.Types;

public enum EnemyType { Archer, Sneaky }

public class EnemyManager : Manager<Enemy, EnemyType, Enemy.Args, EnemyManager>
{
    protected override string PrefabLocation => "Prefabs/Enemies/";

    public override void Init()
    {
        var hashSet = new HashSet<Enemy>(UnityEngine.Object.FindObjectsOfType<Enemy>().ToList());
        foreach (var item in hashSet)
        {
            Add(item);
        }

        base.Init();    
    }
    public Transform GetClosest(Transform correntPosition)
    {
        Transform transform = null;
        float closest=500;
        foreach (var enemy in manager.Collection)
        {
            float newDistance=Vector3.SqrMagnitude(correntPosition.position-enemy.transform.position);

            if (newDistance < closest)
            {
                closest = newDistance;
                transform = enemy.transform;
            }
        }

        return transform;

    }
}
