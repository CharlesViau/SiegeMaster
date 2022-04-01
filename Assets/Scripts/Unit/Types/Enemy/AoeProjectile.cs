using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeProjectile : Projectile
{
    public AoeProjectileScriptable scriptable;
    MeshFilter meshFilter;

    private void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = scriptable.mesh;
      
    }
    public override void Init()
    {
        base.Init();
       
    }
    public override void PostInit()

    {
        base.PostInit();
      

    }
}
