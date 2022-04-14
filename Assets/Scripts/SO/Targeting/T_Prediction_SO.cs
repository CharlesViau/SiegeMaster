using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_Prediction_SO : Targeting_SO
{
    Rigidbody rb;
    public override void Init(GameObject _unit)
    {
        base.Init(_unit);
        rb = _unit.GetComponent<Rigidbody>();
    }
    public Vector3 GetBulletVlocirtDirection(Transform target, Vector3 targetVlocity, float bulletSpeed, Transform barrel, Transform head)
    {
        Vector3 vlocityDirection;
        //transform temp   

        float a = Vector3.Angle(targetVlocity.normalized, (barrel.position - target.position).normalized);
        Debug.Log(Vector3.Magnitude(targetVlocity));
        float b = Mathf.Asin(Mathf.Sin(a * Mathf.Deg2Rad) / (bulletSpeed / (Vector3.Magnitude(targetVlocity)))) * Mathf.Rad2Deg;

        head.forward = (target.position - head.position).normalized;
        Vector3 angl = head.eulerAngles;
        Debug.Log(b);
        vlocityDirection = new Vector3(45, angl.y - b, angl.z);
        head.eulerAngles = vlocityDirection;

        return head.forward * bulletSpeed;
        
    }

}
