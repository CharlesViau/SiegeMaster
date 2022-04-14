using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;

public enum EnemyType { Archer, Sneaky }

public class EnemyManager : Manager<Enemy, EnemyType, Enemy.Args, EnemyManager>
{
    protected override string PrefabLocation => "Prefabs/Enemies/";
}
