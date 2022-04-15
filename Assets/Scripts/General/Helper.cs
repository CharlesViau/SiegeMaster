using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;
using System;
using System.Reflection;
public class Helper 
{
    public static Transform GetCloset(Type type, Transform correntTransfrom)
    {
        var manager = type.GetProperty("Instance", BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy)?.GetValue(null);
        MethodInfo info = type.GetMethod("GetClosest");
        return (Transform)info.Invoke(manager, new object[] { correntTransfrom });

    }
}
