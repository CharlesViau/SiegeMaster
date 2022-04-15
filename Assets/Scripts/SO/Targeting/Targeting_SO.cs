using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;
using System;
using System.Reflection;

public class Targeting_SO : ScriptableObject
{
    protected GameObject unit;

    
    public void FixedRefresh()
    {
    }

    public virtual void Init(GameObject _unit)
    {
        unit = _unit;
    }

    public virtual void Refresh()
    {

    }

}
