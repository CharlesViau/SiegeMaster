using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycast : MonoBehaviour
{
    //rayCastStartPointDistance : Raycast start point has to have distance from camera 
    public LayerMask layerMask;
    const float maxDistanceMultIfHitNoTarget = 0.1f; //Multiple max distance if no target found
    public Vector3 RayCast(float maxDistanceForRay,float rayCastStartPointDistance)
    {

        RaycastHit hit;
        Vector3 hitPoint;
        Debug.DrawRay(transform.position, transform.forward * maxDistanceForRay, Color.green);
        if (Physics.Raycast(transform.position + transform.forward* rayCastStartPointDistance, transform.forward, out hit, maxDistanceForRay,layerMask)) 
        
        {
            //Debug.Log(hit.collider.transform.gameObject.name);
            hitPoint = hit.point;
            // Debug.Log(hitPoint);
        }
        else
        {
            // if player look at sky we send the maxDistancePosition 
            hitPoint = (transform.forward * maxDistanceForRay) +transform.position;

            
        }
        return hitPoint;
    }
    
}
