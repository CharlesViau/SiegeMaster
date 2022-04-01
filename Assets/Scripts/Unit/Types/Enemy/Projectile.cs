using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour, IPoolable, Managers.Template.IUpdaptable
{
    public void Depool()
    {
    }

    public void FixedRefresh()
    {
    }

    public virtual void Init()
    {
    }

    public void Pool()
    {
    }

    public virtual void PostInit()
    {
    }

    public void Refresh()
    {
    }
}
