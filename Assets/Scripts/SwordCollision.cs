using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Units.Types;

public class SwordCollision : MonoBehaviour
{
    #region Fields
    PlayerUnit player;
    Attack_SO damage;
    [HideInInspector] public bool isCollide;
    #endregion

    #region Methods
    private void Start()
    {
        player = FindObjectOfType<PlayerUnit>();
        damage = GetComponentInParent<Enemy>().attack_SO;
        isCollide = false;        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.GotShot(damage.AttackDamage);
            isCollide = true;           
        }        
    }
    #endregion
}
