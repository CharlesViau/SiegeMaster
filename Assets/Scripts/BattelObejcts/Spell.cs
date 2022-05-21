using General;
using Managers;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Spell : MonoBehaviour, IUpdatable, IPoolable, ICreatable<Spell.Args>
{
    

    public void PostInit()
    {
    }

    public void Init()
    {

    }

    public void FixedRefresh()
    {
    }

    public void LateRefresh()
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
        transform.position = constructionArgs.spawningPosition;
    }


    public class Args : ConstructionArgs
    {
        public Transform parent;
        public Args(Vector3 _spawningPosition) : base(_spawningPosition)
        {
        }
    }
}
