using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

enum AttackStates { Shoot, Cooldown, ReadyToAttackChecking }

[CreateAssetMenu(fileName = "Projectile", menuName = "ScriptableObjects/Attack/Projectile Attack")]
public class ProjectileAttack_SO : Attack_SO
{
    #region Fields
    #region Set Projectile Type
    [SerializeField] ProjectileType projectileType;
    [SerializeField] float projectileSpeed;
    #endregion

    #region Game Flow Control
    AttackStates attackState;
    #endregion
    #endregion

    #region Methods
    #region Game Flow
    public override void Init(NavMeshAgent _ownerNavMesh, Transform _ownerPos, Transform _target)
    {
        base.Init(_ownerNavMesh, _ownerPos, _target);
        ownerNavMesh.isStopped = true;
        attackState = AttackStates.Shoot;
    }

    public override void Refresh(Animator _anim)
    {
        switch (attackState)
        {
            case AttackStates.Shoot:
                Attack(_anim);
                break;
            case AttackStates.Cooldown:
                Cooldown(_anim);
                break;
            case AttackStates.ReadyToAttackChecking:
                ReadyToAttackChecking(_anim);
                break;
            default:
                break;
        }
    }
    #endregion

    #region Shoot
    protected override void Attack(Animator _anim)
    {        
        base.Attack(_anim);
        
        InstantiateAProjectile();
    }

    void InstantiateAProjectile()
    {
        ProjectileManager.Instance.Create(projectileType,
            new Projectile.Args(ownerPos.position, projectileType,
            target, projectileSpeed, attackDamage, Vector3.zero, false));

        attackState = AttackStates.Cooldown;
    }
    #endregion

    #region Cooldown
    protected override void Cooldown(Animator _anim)
    {
        _anim.SetFloat("Speed", 0);
        base.Cooldown(_anim);
        if (isAnimSetted)
            _anim.SetTrigger(movementAnimState);

        isAnimSetted = false;
        attackState = AttackStates.ReadyToAttackChecking;
    }

    void ReadyToAttackChecking(Animator _anim)
    {        
        timer += Time.deltaTime;
        if (timer > cooldownTimer)
        {
            timer = 0;
            _anim.ResetTrigger(movementAnimState);
            attackState = AttackStates.Shoot;
        }
    }
    #endregion
    #endregion
}
