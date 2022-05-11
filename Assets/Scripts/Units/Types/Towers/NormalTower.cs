using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;
using Managers;
using UnityEngine.AI;
namespace Units.Types
{
    public class NormalTower : Tower    
    {
        public float projectileSpeed;
        // If you want want to use this predict bool you have to give the prediction projectile 
        // have to set a projectile that has PredictionMovement_SO, 
        public bool predict;
        private Vector3 ProjectileVlocity = Vector3.zero;

        public override void Init()
        {
            base.Init();
        #if UNITY_EDITOR
            if (projectiletype != ProjectileType.Proj_PredictionArrow)
            {
                Debug.LogError("If you want to predict , you should use the prediction Arrow");
                    projectiletype = ProjectileType.Proj_PredictionArrow;
            }

         #endif

        }
        public override void Fire(Transform target)
        {
            if (predict)
            {

                    //this is the math for predict the intercept with between two object wqith different speed 
                    Vector3 targetMovementDirection = target.GetComponent<NavMeshAgent>().velocity;
                    float targetMovementVlovity = target.GetComponent<NavMeshAgent>().speed;
                    float AncleTargetToPlayer = Vector3.Angle(targetMovementDirection.normalized, (head.position - target.position).normalized);
                    float TowerAngleFinalRotation = Mathf.Asin((Mathf.Sin(AncleTargetToPlayer * Mathf.Deg2Rad) * targetMovementVlovity) / projectileSpeed) * Mathf.Rad2Deg;

                    Vector3 dir = (target.position - head.position).normalized;
                    Vector3 left = Vector3.Cross(dir, targetMovementDirection.normalized);
                    head.LookAt(target.position, left);
                    if (!float.IsNaN(TowerAngleFinalRotation))
                    {
                        head.RotateAround(head.transform.position, head.transform.up, TowerAngleFinalRotation);
                    }
                    ProjectileVlocity = head.forward * projectileSpeed;
                //calculate the prediction
            }
            ProjectileManager.Instance.Create(projectiletype, new Projectile.Args(head.position, projectiletype, target, projectileSpeed, projectileDamage, ProjectileVlocity));
            base.Fire(target);
        }

  
        public override void ExtrabehaviorBeforFire()
        {
            if (target)
            {
                head.forward = (target.position - head.position).normalized;
            }
           
        }
    }
    
}