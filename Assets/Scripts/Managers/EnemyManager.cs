using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;
using System.Linq;
using Unit.Types;
public class EnemyManager : General.Manager<Enemy, EnemyType, Enemy.Args, EnemyManager>
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
