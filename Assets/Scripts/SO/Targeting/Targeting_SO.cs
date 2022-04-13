using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
