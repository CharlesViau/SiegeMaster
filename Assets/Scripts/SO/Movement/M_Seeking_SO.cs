using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Units.Types;
using System.Reflection;

using General;
[CreateAssetMenu(fileName = "Movenemt", menuName = "ScriptableObjects/Movement/Seeking")]

public class M_Seeking_SO : Movement_SO
{
    System.Type targetType;
    public override void Refresh()
    {
        if (target == null || target.gameObject.GetComponent<Enemy>().alive == false )
        {
            // if target is destroid looking for other object 
            target = Helper.GetClosetInRange(typeof(EnemyManager), gameobject.transform, 1000);
            if (target )
            {
                // if cant find another enmy goes to pool;
                IPoolable p = gameobject.GetComponent<IPoolable>();
                ObjectPool.Instance.Pool(type, p);
                return;
            }
        }
    
        rb.velocity = (target.position - gameobject.transform.position).normalized * initialSpeed;
        gameobject.transform.forward = rb.velocity.normalized;

    }

}
