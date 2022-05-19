using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum AttackStates { Shoot, OnShootReset, Cooldown }

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
    public override void Init(Transform _ownerPos, Transform _target)
    {
        base.Init(_ownerPos, _target);        
        attackState = AttackStates.Shoot;
    }

    public override void Refresh(Animator _anim)
    {
        switch (attackState)
        {
            case AttackStates.Shoot:
                Attack(_anim);
                break;
            case AttackStates.OnShootReset:
                AttackReset(_anim);
                break;
            case AttackStates.Cooldown:
                Cooldown(_anim);
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

        attackState = AttackStates.OnShootReset;
    }
    #endregion

    #region Cooldown
    protected override void AttackReset(Animator _anim)
    {
        _anim.SetFloat("Speed", 0);
        base.AttackReset(_anim);
        if (isAnimSetted)
            _anim.SetTrigger(movementAnimState);

        isAnimSetted = false;
        attackState = AttackStates.Cooldown;
    }

    void Cooldown(Animator _anim)
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
