using General;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Collections.Generic;
using static puQUEST;

namespace Units.Types
{
    public class Cell : Unit, ICreatable<Cell.Args>, IHittable
    {
        public float Maxforce;
        public float Minforce;
       [HideInInspector] public Body body;

        public float maxDistance;
        public float minDistance;
        Rigidbody rigidbody;
        
        public float calculatedForce;
        float randForce =1;
        public Transform cellPosition;

        protected override Vector3 AimedPosition => throw new System.NotImplementedException();

        public override void Init()
        {
            calculatedForce = 0;
            base.Init();
           
            rigidbody = GetComponent<Rigidbody>();

        }
        public void Construct(Args constructionArgs)
        {
            cellPosition = constructionArgs.parent;
            transform.parent = constructionArgs.parent;
            body = constructionArgs.body;
            transform.SetParent(null);

            //Calculate Random Force 
            randForce = Random.Range(Minforce, Maxforce);
            float randDis = Random.Range(minDistance, maxDistance);
            calculatedForce = Mathf.Clamp(Vector3.Distance(cellPosition.position, transform.position), 0, 5);
        }
        public override void FixedRefresh()
        {
            
            rigidbody.AddForce( (cellPosition.position - transform.position).normalized * calculatedForce*randForce) ;
          //  Debug.Log("hk");
            base.Refresh();
        }

        public void ForceCalculation()
        {

        }

        public void GotShot(float damage)
        {
            
        }
        public override void Pool()
        {
            gameObject.SetActive(false);
            base.Pool();
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag =="Target")
            {
               
                body.CellDeath(this);
                CellManager.Instance.Pool(CellType.Normal, this);
            }
            
        }
        public class Args : ConstructionArgs
        {
            public Transform parent;
            public Body body;
            public Args(Vector3 _spawningPosition, Transform _parent,Body _body) : base(_spawningPosition)
            {
                parent = _parent;
                body= _body;

            }
        }
    }
}