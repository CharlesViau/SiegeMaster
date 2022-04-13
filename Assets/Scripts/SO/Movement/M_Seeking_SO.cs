using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Movenemt", menuName = "ScriptableObjects/Movement/Seeking")]

public class M_Seeking_SO : Movement_SO
{
    public override void Refresh()
    {
        base.Refresh();
        rb.velocity = (target.position - unit.transform.position).normalized * initialSpeed;

        unit.transform.forward = rb.velocity.normalized;
    }

}
