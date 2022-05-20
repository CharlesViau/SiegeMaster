using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

enum AttackStates { Shoot, Cooldown, CheackIfReadyToAttack }

[CreateAssetMenu(fileName = "Projectile", menuName = "ScriptableObjects/Attack/Projectile Attack")]
public class ProjectileAttack_SO : Attack_SO
{
    #region Fields
    #region Set Projectile Type
    [SerializeField] ProjectileType projectileType;
    [SerializeField] float projectileSpeed;
    [SerializeField] float AttackAnimationLengh;
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
            case AttackStates.CheackIfReadyToAttack:
                CheackIfReadyToAttack(_anim);
                break;
            default:
                break;
        }
    }
    #endregion

    #region Shoot
    protected override void Attack(Animator _anim)
    {
        timer += Time.deltaTime;
        base.Attack(_anim);
        if (timer > AttackAnimationLengh)
        {
            timer = 0;
            InstantiateAProjectile();
        }
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
        timer += Time.deltaTime;
        _anim.SetFloat(Speed, 0);
        base.Cooldown(_anim);
        _anim.SetTrigger(movementAnimState);
        isAnimSetted = false;
        if (timer > 2)
            attackState = AttackStates.CheackIfReadyToAttack;
    }

    void CheackIfReadyToAttack(Animator _anim)
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

    public override void ResetBehaviors(Animator _anim)
    {
        base.ResetBehaviors(_anim);
    }
    #endregion
}
