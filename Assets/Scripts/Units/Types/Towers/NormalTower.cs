using UnityEngine;
using UnityEngine.AI;
namespace Units.Types
{
    public class NormalTower : Tower    
    {
        public float projectileSpeed;
        // If you want want to use this predict bool you have to give the prediction projectile 
        // have to set a projectile that has PredictionMovement_SO, 
        public bool predict;

        private Vector3 _projectileVelocity = Vector3.zero;

        public override void Init()
        {
            base.Init();
#if UNITY_EDITOR
            if (projectileType != ProjectileType.Proj_PredictionArrow)
            {
                Debug.Log("If you want to predict , you should use the prediction Arrow");
            }

         #endif

        }
        public override void Fire(Transform targetTransform)
        {
            if (predict)
            {

                    //this is the math for predict the intercept with between two object wqith different speed 
                    Vector3 targetMovementDirection = target.GetComponent<NavMeshAgent>().velocity;
                    float targetMovementVlovity = target.GetComponent<NavMeshAgent>().speed;
                    float AncleTargetToPlayer = Vector3.Angle(targetMovementDirection.normalized, (head.position - target.position).normalized);
                    float towerAngleFinalRotation = Mathf.Asin((Mathf.Sin(AncleTargetToPlayer * Mathf.Deg2Rad) * targetMovementVlovity) / projectileSpeed) * Mathf.Rad2Deg;

                var dir = (targetTransform.position - head.position).normalized;
                var left = Vector3.Cross(dir, targetMovementDirection.normalized);
                head.LookAt(targetTransform.position, left);
                if (!float.IsNaN(towerAngleFinalRotation))
                {
                    head.RotateAround(head.transform.position, head.transform.up, towerAngleFinalRotation);
                }

                _projectileVelocity = head.forward * projectileSpeed;
                //calculate the prediction
            }

            ProjectileManager.Instance.Create(projectileType,
                new Projectile.Args(head.position, projectileType, targetTransform, projectileSpeed, projectileDamage,
                    _projectileVelocity));
            base.Fire(targetTransform);
        }

  
        public override void ExtraBehaviorBeforeFire()
        {
            if (target)
            {
                head.forward = (target.position - head.position).normalized;
            }
           
        }
    }
    
}