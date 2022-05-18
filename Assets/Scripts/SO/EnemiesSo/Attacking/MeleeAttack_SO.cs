using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum MeleeStates { CollisionOn, CollisionOff }

[CreateAssetMenu(fileName = "Melee", menuName = "ScriptableObjects/Attack/Melee Attack")]
public class MeleeAttack_SO : Attack_SO
{
    MeleeStates meleeState;
    SwordCollision script;
    [SerializeField] GameObject sword;

    public override void Init(Vector3 _ownerPos, Transform _target)
    {
        base.Init(_ownerPos, _target);
        meleeState = MeleeStates.CollisionOn;
        script = sword.GetComponent<SwordCollision>();
    }

    public override void Refresh(Animator _anim)
    {
        base.Refresh(_anim);
        switch (meleeState)
        {
            case MeleeStates.CollisionOn:
                Attack(_anim);
                break;
            case MeleeStates.CollisionOff:
                AttackReset(_anim);
                break;
            default:
                break;
        }
    }

    protected override void Attack(Animator _anim)
    {
        base.Attack(_anim);
        IsCollide();
    }

    protected override void AttackReset(Animator _anim)
    {
        base.AttackReset(_anim);
        timer += Time.deltaTime;
        isAnimSetted = false;
        script.enabled = true;

        if (timer > cooldownTimer)
            meleeState = MeleeStates.CollisionOn;
    }

    void IsCollide()
    {
        if (script && script.isCollide)
        {
            script.enabled = false;
            meleeState = MeleeStates.CollisionOff;
        }
    }
}
