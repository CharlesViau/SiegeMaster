using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;
using Managers;
namespace Units.Types
{
    public class CatapulTower : Tower
    {

        private const float towerXrotation = 45; // fixed value for the rotation to shoot bullet

        public override void Fire(Transform target)
        {

            Vector3 catapultPosition = new Vector3(transform.position.x, target.transform.position.y, transform.position.z);
            Vector3 pointToObject = (target.position - transform.position).normalized;
            head.forward = pointToObject;
            Vector3 vector32 = head.eulerAngles;
            head.eulerAngles = new Vector3(towerXrotation, vector32.y, vector32.z);
            float distanceTotarget = Vector3.Distance(catapultPosition, target.position);
            Vector3 finalvelocity = distanceTotarget * Mathf.Sqrt(-Physics.gravity.y / (barrel.position.y - target.transform.position.y + distanceTotarget)) * barrel.transform.forward;

            ProjectileManager.Instance.Create(projectiletype, new Projectile.Args(barrel.position, projectiletype, target, 0, 0, finalvelocity));

            base.Fire(target);


        }

        //

    }
}