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
    public override void Refresh()
    {

        if (target.GetComponent<Enemy>().alive ==false)
        {

            target = Helper.GetClosetInRange(type, gameobject.transform, 1000);

        }
        base.Refresh();
        rb.velocity = (target.position - gameobject.transform.position).normalized * initialSpeed;
        gameobject.transform.forward = rb.velocity.normalized;

    }

}
