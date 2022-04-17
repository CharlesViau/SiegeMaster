using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;
using System;
using System.Reflection;
public class Helper 
{
    // GetClosetInRange : variable 
    // type : the object that you want to get 
    // correntTransfrom : get the closest object to this transform 
    // range  : the range to get the closest 
    public static Transform GetClosetInRange(Type type, Transform correntTransfrom, float range) // 
    {
        //using reflextion to find the manager and get the method 
        var manager = type.GetProperty("Instance", BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy)?.GetValue(null);
        
        MethodInfo info = type.GetMethod("GetClosest");
        if (info==null)
        {
            Debug.Log("we couldnt found the Method in manager ");
        }
        return (Transform)info.Invoke(manager, new object[] { correntTransfrom ,range });

    }
}
