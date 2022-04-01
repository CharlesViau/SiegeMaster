using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bulle_SO : ScriptableObject
{
    protected Unit.Unit unit;
    public virtual void Init(Unit.Unit _unit)
    {
        unit = _unit;
    }

    public virtual void Update()
    {
    }
}

