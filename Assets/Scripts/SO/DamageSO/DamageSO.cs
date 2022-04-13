using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSO : ScriptableObject
{
    protected GameObject unit;
    protected Rigidbody rb;
    public float damage;
    public GameObject particleEffect;
   
    public void FixedRefresh()
    {
    }

    public virtual void Init(GameObject _unit)
    {
        unit = _unit;
        rb = _unit.GetComponent<Rigidbody>();
    }

    public virtual void Refresh()
    {

    }
    public virtual void OnCollisionEnter()
    {

    }
}
