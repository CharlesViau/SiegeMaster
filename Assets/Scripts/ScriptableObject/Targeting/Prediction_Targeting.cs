using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Targeting", menuName = "ScriptableObjects/Targeting/Prediction")]
public class Prediction_Targeting : Targeting_SO
{
    public GameObject Bullet;
    public override void Init(Unit.Unit _unit)
    {
        base.Init(_unit);

       
    }
    public override void GetTargetLocation(Transform target, Vector3 targetVlocity,float bulletSpeed , Transform barrel,Transform head )
    {
        Vector3 angle;

        float a = Vector3.Angle(targetVlocity.normalized, (barrel.position - target.position).normalized);
        Debug.Log(Vector3.Magnitude(targetVlocity));
        float b = Mathf.Asin(Mathf.Sin(a * Mathf.Deg2Rad) / (bulletSpeed / (Vector3.Magnitude(targetVlocity)))) * Mathf.Rad2Deg;

        head.forward = (target.position - head.position).normalized;
        Vector3 angl = head.eulerAngles;
        Debug.Log(b);
        angle = new Vector3(45, angl.y - b, angl.z);
        head.eulerAngles = angle;


        GameObject Newww = Instantiate(Bullet, barrel.position, Quaternion.identity);
        Newww.GetComponent<Rigidbody>().velocity = head.forward * bulletSpeed;

    }




    #region
    //public Transform GetTarget()
    //{

    //    var enemy = GetClosest(EnemyManager.Instance.Collection, unit.transform.position) ;
    //}

    //1) SSwitch case

    //public static HashSet<T> GetCollectionFromManager(AcceptableTargets at)
    //{
    //    switch (at)
    //    {
    //        case AcceptableTargets.Enemy:
    //            break;
    //        case AcceptableTargets.Tower:
    //            break;
    //        case AcceptableTargets.Player:
    //            break;
    //        default:
    //            Debug.LogError("Unhandled type: " + at);
    //            break;
    //    }
    //}

    //2




    //public static T GetClosest<T>(IEnumerable<T> collection,Vector3 pos) where T : MonoBehaviour
    //{

    //}
    //void FireBullet(Vector3 targetPos, Vector3 objectVlocity)
    //{


    //    float a = Vector3.Angle(objectVlocity.normalized, (Barrel.position - targetPos).normalized);
    //    Debug.Log(Vector3.Magnitude(objectVlocity));
    //    float b = Mathf.Asin(Mathf.Sin(a * Mathf.Deg2Rad) / (speed / (Vector3.Magnitude(objectVlocity)))) * Mathf.Rad2Deg;

    //    neck.forward = (targetPos - neck.position).normalized;
    //    Vector3 angl = neck.eulerAngles;
    //    Debug.Log(b);
    //    Vector3 newAnc = new Vector3(angl.x, angl.y - b, angl.z);
    //    neck.eulerAngles = newAnc;


    //    GameObject Newww = Instantiate(Bullet, Barrel.position, Quaternion.identity);
    //    Newww.GetComponent<Rigidbody>().velocity = neck.forward * speed;

    //}


    #endregion

}
