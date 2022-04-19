using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;
using Managers;
namespace Units.Types
{
    public class Catapul :Tower
{

        private float towerXrotation = 45; // fixed value for the rotation to shoot bullet

        float timer = 0;
        float timeToFire = 5;
        public void Update()
        {
            timer += Time.deltaTime;

            if (timer > timeToFire)
            {
                Fire(Helper.GetClosetInRange(typeof(EnemyManager), this.transform, towerAttackRange));
                timer = 0;
            }
        }
        public void Fire(Transform target)
        {
            Vector3 catapultPosition = new Vector3(transform.position.x, 0, transform.position.z);
            Vector3 pointToObject = (target.position - transform.position).normalized;
            head.forward = pointToObject;

            Vector3 vector32 = head.eulerAngles;


            head.eulerAngles = new Vector3(towerXrotation, vector32.y, vector32.z);
            Vector3 finalvelocity = Vector3.Distance(catapultPosition, target.position) * Mathf.Sqrt(9.81f / (barrel.position.y + Vector3.Distance(catapultPosition, target.position))) * barrel.transform.forward;
            ProjectileManager.Instance.Create(projectiletype, new Projectile.Args(barrel.position, target, 0, 0, finalvelocity));

        }


    }
}