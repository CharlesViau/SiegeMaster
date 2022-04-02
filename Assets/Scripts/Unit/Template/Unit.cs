using Managers.Template;
using UnityEngine;

namespace Unit.Template
{
    public abstract class Unit : MonoBehaviour, IUpdaptable, IPoolable
    {
        public virtual void Init()
        {
            throw new System.NotImplementedException();
        }

        public void PostInit()
        {
            throw new System.NotImplementedException();
        }

        public void Refresh()
        {
            throw new System.NotImplementedException();
        }

        public void FixedRefresh()
        {
            throw new System.NotImplementedException();
        }

        public void Pool()
        {
            throw new System.NotImplementedException();
        }

        public void Depool()
        {
            throw new System.NotImplementedException();
        }
    }
}
