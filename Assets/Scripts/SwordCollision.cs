using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Units.Types;

public class SwordCollision : MonoBehaviour
{
    PlayerUnit player;
    Attack_SO damage;
    [HideInInspector] public bool isCollide;
    
    private void Start()
    {
        player = FindObjectOfType<PlayerUnit>();
        damage = GetComponentInParent<Enemy>().attack_SO;
        isCollide = false;
        Debug.Log("Damage: " + damage.AttackDamage);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.GotShot(damage.AttackDamage);
            isCollide = true;
           
        }        
    }
}
