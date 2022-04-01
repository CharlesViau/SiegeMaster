using UnityEngine;

public enum AcceptableTargets { Enemy, Tower, Player }
namespace Unit.Types
{
    public class Tower :Unit
    {
        public AcceptableTargets[] scceptableTargets;
        public Targeting_SO targeting;
        public Bulle_SO bulle;
        public float bulletSpeed;
        public Transform barrel;
        public Transform head;

        public Transform target;

        public void Start()
        {
            base.Init();
            bulle.Init(this);
            targeting.Init(this);
        }

        public  void Update()
        {
            base.Refresh();
            bulle.Update();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                targeting.GetTargetLocation(target, new Vector3(1, 0, 0), bulletSpeed, barrel, head);
            }
            
        }
    }
}
