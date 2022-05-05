using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRatcast : MonoBehaviour
{
  
    private RaycastHit hit;
    [HideInInspector]
    public Vector3 hitPoint;
    public float maxDistanceForRay;
    void FixedUpdate()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, fwd, out hit, maxDistanceForRay))
            hitPoint =hit.point;
        else
        {
            // if player look at sky we send the maxDistancePosition 
            hitPoint = transform.forward * maxDistanceForRay;
        }
    }
}
