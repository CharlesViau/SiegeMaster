using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;
using System;
using System.Reflection;

public class Targeting_SO : ScriptableObject
{
    protected GameObject gameObject;
    protected float range;
    
    public void FixedRefresh()
    {
    }

    public virtual void Init(GameObject _unit,float _range)
    {
        gameObject = _unit;
        range = _range;
    }
    public virtual Transform GetTheTarget()
    {
        return null;
    }
    public virtual void Refresh()
    {

    }

}
