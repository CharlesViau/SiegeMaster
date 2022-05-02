using General;
using Managers;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Nexus : MonoBehaviour, IUpdatable, IPoolable, ICreatable<Nexus.Args>
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
        throw new System.NotImplementedException();
    }

    public void Pool()
    {
        
    }

    public void Depool()
    {
        
    }

    public void Construct(Args constructionArgs)
    {
        transform.position = constructionArgs.spawningPosition;
    }

    public class Args : ConstructionArgs
    {        
        public Args(Vector3 _spawningPosition) : base(_spawningPosition)
        {
            
        }
    }
}
