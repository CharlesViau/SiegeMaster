using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "Attacking", menuName = "ScriptableObjects/Atack")]
public class Attack_SO : ScriptableObject
{
    [SerializeField] Animator anim;
    float attackRange;
    float damage;
    // oncollision so or particle system

    public void Init(Animator _anim, float _attackRange, float _damage)
    {
        anim = _anim;
        attackRange = _attackRange;
        damage = _damage;
    }

    public void Attack()
    {
        //anim
    }
}
