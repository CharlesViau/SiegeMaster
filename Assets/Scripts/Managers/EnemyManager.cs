using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;
using Unit.Types;
public class EnemyManager : General.Manager<Enemy, EnemyType, Enemy.Args, EnemyManager>
{
    protected override string PrefabLocation => "Prefabs/Enemies/";
}
