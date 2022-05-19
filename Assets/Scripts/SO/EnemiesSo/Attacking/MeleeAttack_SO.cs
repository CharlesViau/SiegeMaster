using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

enum MeleeStates { CollisionOn, CollisionOff }

[CreateAssetMenu(fileName = "Melee", menuName = "ScriptableObjects/Attack/Melee Attack")]
public class MeleeAttack_SO : Attack_SO
{
    #region Fields
    MeleeStates meleeState;
    SwordCollision sword;
    Collider swordCollider;
    #endregion

    #region Methods
    #region Game flow
    public override void Init(NavMeshAgent _ownerNavMesh, Transform _ownerPos, Transform _target)
    {
        base.Init(_ownerNavMesh, _ownerPos, _target);

        sword = FindObjectOfType<SwordCollision>();
        swordCollider = sword.GetComponent<Collider>();
        sword.Init(attackDamage, target);

        meleeState = MeleeStates.CollisionOn;
    }

    public override void Refresh(Animator _anim)
    {
        base.Refresh(_anim);
        switch (meleeState)
        {
            case MeleeStates.CollisionOn:
                Attack(_anim);
                if (Vector3.Distance(ownerNavMesh.transform.position, target.position) < 5)
                    ownerNavMesh.isStopped = true;
                if (Vector3.Distance(ownerNavMesh.transform.position, target.position) > 5)
                {
                    ownerNavMesh.isStopped = false;
                    //_anim.SetFloat("Speed", 0.2f);
                }
                break;
            case MeleeStates.CollisionOff:
                Cooldown(_anim);
                break;
            default:
                break;
        }
    }
    #endregion

    #region Attack
    protected override void Attack(Animator _anim)
    {
        base.Attack(_anim);


        if (swordCollider.enabled == false)
            meleeState = MeleeStates.CollisionOff;
    }
    #endregion

    #region Cooldown
    protected override void Cooldown(Animator _anim)
    {
        base.Cooldown(_anim);
        timer += Time.deltaTime;
        isAnimSetted = false;

        if (timer > cooldownTimer)
        {
            timer = 0;
            sword.ToggleColliderActive(true);
            meleeState = MeleeStates.CollisionOn;
        }
    }
    #endregion
    #endregion
}
