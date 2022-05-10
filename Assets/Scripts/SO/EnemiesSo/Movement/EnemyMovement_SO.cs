using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "Enemy Movement", menuName = "ScriptableObjects/Movement/Enemy")]
public class EnemyMovement_SO : ScriptableObject
{
    protected GameObject unit;
    protected float speed;
    protected NavMeshAgent agent;
    protected Transform target;

    public void Init(GameObject _unit, Transform _target, float _speed)
    {
        unit = _unit;
        speed = _speed;
        target = _target;
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
        //Debug.Log(agent.velocity);
        //Debug.Log(agent.speed);
        //Debug.Log(agent.speed + agent.acceleration);
    }

    // Set active checking as navmesh doesn't deal with object which are not active, as the enemies is died   
    public void MoveToPoint(Vector3 target)
    {
        agent.SetDestination(target);
    }
}