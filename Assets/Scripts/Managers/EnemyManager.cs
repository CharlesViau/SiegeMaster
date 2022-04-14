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

}
