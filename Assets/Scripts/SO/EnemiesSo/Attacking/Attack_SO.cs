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
    public float AttackDamage { get { return attackDamage; } }
    #endregion

    #region Info from Attacker
    protected Transform ownerPos;
    protected Transform target;
    protected NavMeshAgent ownerNavMesh;
    #endregion

    #region Game Flow Control
    [HideInInspector] public bool isAnimSetted;
    protected float timer;
    protected static readonly int Speed = Animator.StringToHash("Speed");
    #endregion
    #endregion

    #region Methods
    #region Game Flow
    public virtual void Init(NavMeshAgent _ownerNavMesh, Transform _ownerPos, Transform _target)
    {
        ownerNavMesh = _ownerNavMesh;
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
    protected virtual void Cooldown(Animator _anim)
    {
        if (isAnimSetted)
            _anim.ResetTrigger(attackAnimState);
    }
    #endregion

    #region Saftey Check
    public virtual void ResetBehaviors(Animator _anim)
    {
        _anim.SetFloat(Speed, 10);
        _anim.ResetTrigger(attackAnimState);
        _anim.SetTrigger(movementAnimState);
        ownerNavMesh.isStopped = false;
        isAnimSetted = false;


        //_enemyAgent.isStopped = false;
    }
    #endregion
    #endregion
}
