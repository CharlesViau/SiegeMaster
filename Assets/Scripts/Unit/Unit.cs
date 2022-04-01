using Managers.Template;
using UnityEngine;

namespace Unit
{
    public abstract class Unit : MonoBehaviour, IUpdaptable, IPoolable
    {
        public virtual void Init()
        {
        }

        public virtual void PostInit()
        {
        }

        public virtual void Refresh()
        {
        }

        public virtual void FixedRefresh()
        {
        }

        public virtual void Pool()
        {
        }

        public virtual void Depool()
        {
        }
    }
}
