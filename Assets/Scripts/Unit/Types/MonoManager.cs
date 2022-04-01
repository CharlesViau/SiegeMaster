using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers.Template;

public class MonoCollectManager<T,T1> : CollectionManager<T, T1>  where T : class, new() where T1 : MonoBehaviour, IUpdaptable
{

    //public T1 GetClosestToPoint(Vector3 v3)
    //{
    //    foreach(T1 t in Collection)
    //    {
            
    //    }
    //    return 
    //}

}
