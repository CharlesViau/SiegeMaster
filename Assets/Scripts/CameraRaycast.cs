using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycast : MonoBehaviour
{
    public Vector3 RayCast(float maxDistanceForRay)
    {
        Vector3 hitPoint;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out var hit, maxDistanceForRay))
        {
            hitPoint = hit.point;
            // Debug.Log(hitPoint);
        }
        else
        {
            // if player look at sky we send the maxDistancePosition 
            hitPoint = transform.forward * maxDistanceForRay;
        }
        return hitPoint;
    }
    
}
