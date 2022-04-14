using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Movenemt", menuName = "ScriptableObjects/Movement/Seeking")]

public class M_Seeking_SO : Movement_SO
{
    public override void Refresh()
    {
        base.Refresh();
        rb.velocity = (target.position - gameobject.transform.position).normalized * initialSpeed;

        gameobject.transform.forward = rb.velocity.normalized;
    }

}
