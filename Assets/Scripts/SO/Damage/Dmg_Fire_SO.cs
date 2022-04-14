using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;
[CreateAssetMenu(fileName = "FireDamageSO", menuName = "ScriptableObjects/Damage/Fire")]

public class Dmg_Fire_SO : DamageSO
{
    
    public override void Refresh()
    {
        // MAKE A sphire cast to deal damage around the Bullet;
        
        RaycastHit[] hits = Physics.SphereCastAll(gameObject.transform.position,5, gameObject.transform.forward,0, myLayerMask);
        for (int i = 0; i < hits.Length; i++)
        {
            hits[i].collider.gameObject.GetComponent<IHittable>().GotShot(damage);
        }
       
    }

}
