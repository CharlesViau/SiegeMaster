using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;
using Managers;

using System;

[CreateAssetMenu(fileName = "Targeting", menuName = "ScriptableObjects/Targeting/T_GetClosetOfManagerType_SO")]

public class T_GetClosetOfManagerType_SO : Targeting_SO
{
    public enum ManagerType {Enemy,Tower,Projectile }
    public ManagerType managerType;
    private System.Type GetManagerType(ManagerType eType)
    {
        System.Type type = null;
        switch (eType)
        {
            case ManagerType.Enemy:
                type = typeof(EnemyManager);
                break;
            case ManagerType.Tower:
                type = typeof(TowerManager);
                break;
            case ManagerType.Projectile:
                type = typeof(ProjectileManager);
                break;
            default:
                Debug.Log("unhandled SwitchCase");
                break;
        }
        return type;
    }
    public override Transform GetTheTarget()
    {
        
        return Helper.GetClosetInRange(GetManagerType(managerType), gameObject.transform,range);
    }

}
