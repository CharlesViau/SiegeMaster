using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "Enemy Movement", menuName = "ScriptableObjects/Movement/Enemy")]
public class EnemyMovement_SO : ScriptableObject
{
    protected GameObject unit;
    //protected Rigidbody rb;
    protected float speed;
    protected NavMeshAgent agent;
    protected Transform target;

    public void Init(GameObject _unit, Transform _target, float _speed)
    {
        unit = _unit;
        speed = _speed;
        target = _target;
        //rb = _unit.GetComponent<Rigidbody>();
        agent = _unit.GetComponent<NavMeshAgent>();
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

    public void MoveToPoint(Vector3 target)
    {
        agent.SetDestination(target);
        //rb.velocity = speed * (target - unit.transform.position).normalized;
    }
}
