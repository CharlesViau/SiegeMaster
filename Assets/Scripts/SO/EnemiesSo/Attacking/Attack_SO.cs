using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "Sword", menuName = "ScriptableObjects/Attack/Without projectile")]
public class Attack_SO : ScriptableObject
{
    [SerializeField] protected string animState;
    [SerializeField] protected float attackDamage;
    protected Vector3 ownerPos;
    protected Transform target;
    protected float attackRange;
    protected bool isAnimSetted;
    // oncollision so or particle system

    public virtual void Init(Vector3 _ownerPos, Transform _target, float _attackRange)
    {
        ownerPos = _ownerPos;
        target = _target;
        attackRange = _attackRange;
    }

    public virtual void Attack(Animator _anim)
    {
        if (!isAnimSetted)
            _anim.SetTrigger(animState);
        isAnimSetted = true;
        Shoot();
    }

    public virtual void StayAway(Animator _anim)
    {
        if (isAnimSetted)
            _anim.ResetTrigger(animState);
        isAnimSetted = false;
    }

    protected virtual void Shoot()
    {
    }
}
