using General;
using Managers;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Collections.Generic;

public class HP : MonoBehaviour,IUpdatable, IPoolable, ICreatable<HP.Args>
{
    public HPType type;

    public void PostInit()
    {
    }

    public void Init()
    {
    }

    public void FixedRefresh()
    {
    }

    public void Refresh()
    {
       
    }

    public void Pool()
    {
        gameObject.SetActive(false);
    }
    
    public void Depool()
    {
        gameObject.SetActive(true);
    }

    public void Construct(Args constructionArgs)
    {
        transform.SetParent(constructionArgs.parent);
        transform.position = constructionArgs.spawningPosition;
    }

    public class Args : ConstructionArgs
    {
        public Transform parent;
        public Args(Vector3 _spawningPosition,Transform _parent) : base(_spawningPosition)
        {
            parent = _parent;
        }
    }
}
