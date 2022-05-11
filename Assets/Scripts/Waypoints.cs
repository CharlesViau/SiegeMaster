using General;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour, IUpdatable, ICreatable<Waypoints.Args>
{
    public void Init()
    {
        
    }
    public void PostInit()
    {
        
    }
    
    public void Refresh()
    {
        
    }

    public void FixedRefresh()
    {
        
    }

    public void LateRefresh()
    {
        
    }
    
    public void Construct(Args constructionArgs)
    {
        
    }

    public class Args : ConstructionArgs
    {
        public Args(Vector3 _spawningPosition) : base(_spawningPosition)
        {

        }
    }
}
