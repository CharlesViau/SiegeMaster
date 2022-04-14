using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : General.Manager<Projectile, ProjectileType, Projectile.Args, ProjectileManager>
{
    protected override string PrefabLocation => "Prefabs/Enemies/";
}
