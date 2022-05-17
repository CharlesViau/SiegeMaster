using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Projectile", menuName = "ScriptableObjects/Attack/WithProjectile")]
public class AttackWithProjectile_SO : Attack_SO
{
    // this one needs to stop after one shooting.
    [SerializeField] ProjectileType projectileType;
    [SerializeField] float projectileSpeed;

    public override void Init(Vector3 _ownerPos, Transform _target, float _attackRange)
    {
        base.Init(_ownerPos, _target, _attackRange);
    }

    public override void Attack(Animator _anim)
    {
        base.Attack(_anim);
    }
    protected override void Shoot()
    {
        base.Shoot();
        ProjectileManager.Instance.Create(projectileType,
            new Projectile.Args(ownerPos, projectileType,
            target, projectileSpeed, attackDamage, Vector3.zero, false));
    }
}
