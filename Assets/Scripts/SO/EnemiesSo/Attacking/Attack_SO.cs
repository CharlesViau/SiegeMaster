using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "Sword", menuName = "ScriptableObjects/Attack/Without projectile")]
public class Attack_SO : ScriptableObject
{
    #region Fields
    #region Set Attack Type
    [SerializeField] protected string attackAnimState;
    [SerializeField] protected string movementAnimState;
    [SerializeField] protected float attackDamage;
    #endregion

    #region Info from Attacker
    protected Vector3 ownerPos;
    protected Transform target;
    protected float attackRange;
    #endregion

    #region Game Flow Control
    protected bool isAnimSetted;
    #endregion
    #endregion

    #region Methods
    #region Game Flow
    public virtual void Init(Vector3 _ownerPos, Transform _target, float _attackRange)
    {
        ownerPos = _ownerPos;
        target = _target;
        attackRange = _attackRange;
        isAnimSetted = false;
    }

    public virtual void Refresh(Animator _anim)
    {
        Attack(_anim);
    }
    #endregion

    #region Attack
    protected virtual void Attack(Animator _anim)
    {
        if (!isAnimSetted)
            _anim.SetTrigger(attackAnimState);
        isAnimSetted = true;
    }
    #endregion

    #region Cooldown
    protected virtual void AttackReset(Animator _anim)
    {
        if (isAnimSetted)
        {
            _anim.ResetTrigger(attackAnimState);
            _anim.SetTrigger(movementAnimState);
        }
        isAnimSetted = false;     
    }
    #endregion
    #endregion
}
