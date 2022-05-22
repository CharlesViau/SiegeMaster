using General;
using Units.Interfaces;
using Units.Types;
using UnityEngine;
using static PuQUEST;

namespace Units.BossEnemy
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
            cellPosition = constructionArgs.cellPosition;
            transform.parent = constructionArgs.cellPosition;
            body = constructionArgs.body;
            transform.SetParent(constructionArgs.parentTransfrom);

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

        public override void GotShot(float damage)
        {
            body.CellDeath(this);
            CellManager.Instance.Pool(CellType.Normal, this);
        }
        public override void Pool()
        {
            gameObject.SetActive(false);
            base.Pool();
        }

        public class Args : ConstructionArgs
        {
            public Transform cellPosition;
            public Transform parentTransfrom;
            public Body body;
            
            public Args(Vector3 _spawningPosition, Transform _cellPosition, Body _body, Transform _parentTransfrom) : base(_spawningPosition)
            {
                cellPosition = _cellPosition;
                parentTransfrom = _parentTransfrom;
                body= _body;

            }
        }
    }
}