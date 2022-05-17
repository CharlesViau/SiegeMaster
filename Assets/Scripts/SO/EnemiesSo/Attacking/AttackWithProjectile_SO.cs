using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Projectile", menuName = "ScriptableObjects/Attack/WithProjectile")]
public class AttackWithProjectile_SO : Attack_SO
{
    // this one needs to stop after one shooting.
    [SerializeField] ProjectileType projectileType;
    [SerializeField] public float projectileDamage; // ??
    [SerializeField] public float projectileSpeed;

    public override void Init(Vector3 _ownerPos, Transform _target, float _damage)
    {
        base.Init(_ownerPos, _target, _damage);
    }

    public override void Attack(Animator _anim)
    {
        base.Attack(_anim);
    }
    protected override void Shoot()
    {
        base.Shoot();
        InstantiateProjectile(ownerPos, target);
    }

    void InstantiateProjectile(Vector3 spawnPos, Transform target)
    {
        ProjectileManager.Instance.Create(projectileType,
            new Projectile.Args(spawnPos, projectileType,
            target, projectileSpeed, projectileDamage, Vector3.zero, false));
    }
}
