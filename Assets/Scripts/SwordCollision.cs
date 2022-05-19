using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Units.Types;

public class SwordCollision : MonoBehaviour
{
    #region Fields
    PlayerUnit target;
    float damage;
    [HideInInspector] public bool isCollide;
    #endregion

    #region Methods
    public void Init(float _damage, Transform _target)
    {
        damage = _damage;
        target = _target.GetComponent<PlayerUnit>();
        isCollide = false;        
    }

    public void ToggleActive(bool setActive)
    {
        this.enabled = setActive;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            target.GotShot(damage);
            isCollide = true;           
        }        
    }
    #endregion
}
