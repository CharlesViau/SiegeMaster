using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "FireDamageSO", menuName = "ScriptableObjects/Damage/Fire")]

public class Dmg_Fire_SO : DamageSO
{
    
    public override void Refresh()
    {
        //FireLogic
        //rb.AddForce(new Vector3(1, 2, 3));

    }

    public override void OnEnterCollision(Vector3 position)
    {
        
        ParticleSystemManager.Instance.Create(onCollitionParticleType, new ParticleSystemScript.Args(position));
    }
}
