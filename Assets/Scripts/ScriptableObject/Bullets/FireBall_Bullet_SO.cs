using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FireBall", menuName = "ScriptableObjects/Bullet/FireBall")]
public class FireBall_Bullet_SO : Bulle_SO
{
    public override void Init(Unit.Unit _unit)
    {
       base.Init(unit);
    }

    public override void Update()
    {
        base.Update();
        DealDamagToALLEnemyInSphireCast();
    }
    public void DealDamagToALLEnemyInSphireCast()
    {
        Debug.Log("dealing damage");

    }

}
