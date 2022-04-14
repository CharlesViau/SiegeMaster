using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Movement", menuName = "ScriptableObjects/Movement/Enemy")]
public class EnemyMovement_SO : ScriptableObject, General.IUpdatable
{
    protected GameObject unit;
    protected Rigidbody rb;
    protected float speed;
    protected Transform target;

    public void Init()
    {
        rb = unit.GetComponent<Rigidbody>();
    }

    public void PostInit()
    {

    }

    public void FixedRefresh()
    {

    }

    public void Refresh()
    {

    }

    public void MoveToPoint(Transform target, float speed)
    {
        target = target;
        speed = speed;
                
        Debug.Log("Moving");
    }
}
