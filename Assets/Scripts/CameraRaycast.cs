using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycast : MonoBehaviour
{
    public Vector3 RayCast(float maxDistanceForRay)
    {

        RaycastHit hit;
        Vector3 hitPoint;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, fwd, out hit, maxDistanceForRay)) 
        
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
