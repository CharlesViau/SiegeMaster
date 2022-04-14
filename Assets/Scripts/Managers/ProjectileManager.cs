using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProjectileType {Arrow,Sphere}
public class ProjectileManager : General.Manager<Projectile, ProjectileType, Projectile.Args, ProjectileManager>
{
    protected override string PrefabLocation => "Prefabs/Projectiles/";


}
