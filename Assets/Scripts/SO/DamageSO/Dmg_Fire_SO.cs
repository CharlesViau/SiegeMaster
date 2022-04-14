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

    public override void OnCollisionEnter()
    {
        base.OnCollisionEnter();
        GameObject p = Instantiate(particleEffect, gameObject.transform.position, Quaternion.identity);
    }
}
