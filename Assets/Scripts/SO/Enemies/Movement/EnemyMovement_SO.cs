using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Movement", menuName = "ScriptableObjects/Movement/Enemy")]
public class EnemyMovement_SO : ScriptableObject
{
    protected GameObject unit;
    protected Rigidbody rb;
    protected float speed;
    protected List<Transform> targets;

    public void Init(GameObject _unit, List<Transform> _targets, float _speed)
    {
        unit = _unit;
        speed = _speed;
        targets = _targets;
        rb = _unit.GetComponent<Rigidbody>();
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
        rb.velocity = speed * (target - unit.transform.position).normalized;
    }
}
