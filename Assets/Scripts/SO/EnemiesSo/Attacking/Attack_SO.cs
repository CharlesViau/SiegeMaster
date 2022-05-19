using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Attack_SO : ScriptableObject
{
    #region Fields
    #region Set Attack Type
    [SerializeField] protected string attackAnimState;
    [SerializeField] protected string movementAnimState;
    [SerializeField] protected float attackDamage;
    [SerializeField] protected float cooldownTimer;
    #endregion

    #region Info from Attacker
    protected Transform ownerPos;
    protected Transform target;
    #endregion

    #region Game Flow Control
    protected bool isAnimSetted;
    protected float timer;
    #endregion

    public float AttackDamage { get { return attackDamage; } }
    #endregion

    #region Methods
    #region Game Flow
    public virtual void Init(Transform _ownerPos, Transform _target)
    {
        ownerPos = _ownerPos;
        target = _target;
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

    #region CoolDown
    protected virtual void AttackReset(Animator _anim)
    {
        if (isAnimSetted)
            _anim.ResetTrigger(attackAnimState);
    }
    #endregion
    #endregion
}
