using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;
using Managers;
namespace Units.Types
{
    public class NormalTower : Tower    
    {
        public float projectileSpeed;
        public bool predict;
        public GameObject p;
        private Vector3 ProjectileVlocity =Vector3.zero;
        public override void Fire(Transform target)
        {
            if (predict)
            {
                Vector3 targetMovementDirection = target.gameObject.GetComponent<Rigidbody>().velocity;
                float targetMovementVlovity = Vector3.Magnitude(targetMovementDirection);
                //float distanceBarrelToTarget = Vector3.Magnitude(target.position - barrel.position);
                float AncleTargetToPlayer = Vector3.Angle(targetMovementDirection.normalized, (head.position - target.position).normalized);
                float playerAngle = Mathf.Asin((Mathf.Sin(AncleTargetToPlayer * Mathf.Deg2Rad)* targetMovementVlovity) / projectileSpeed ) * Mathf.Rad2Deg;
                Debug.Log(playerAngle);
                head.up =( target.position- head.position ).normalized;
                head.up = Quaternion.AngleAxis(-playerAngle, Vector3.up) * head.up;

                ProjectileVlocity = head.up * projectileSpeed;
                //calculate the prediction
            }
            ProjectileManager.Instance.Create(projectiletype, new Projectile.Args(head.position, projectiletype, target, projectileSpeed, projectileDamage, ProjectileVlocity));
            base.Fire(target);
        }
        public override void Extrabehavior()
        {
            if (target)
            {
                head.up = (target.position - head.position).normalized;
            }
           
        }
    }
    
}