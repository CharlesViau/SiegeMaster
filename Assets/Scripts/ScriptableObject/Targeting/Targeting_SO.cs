using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Targeting_SO: ScriptableObject
{
    public Transform targetTransform;
    protected Unit.Unit unit;
    public virtual void Init(Unit.Unit _unit)
    { 
        unit = _unit;
    }


    public virtual void GetTargetLocation(Transform target, Vector3 targetVlocity, float bulletSpeed, Transform barrel, Transform head)
    {
        Debug.Log("not Empliment for this targetin scriptableobject");
    }
}
