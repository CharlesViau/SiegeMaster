using System.Linq;
using General;
using Units.Types;
using UnityEngine;

namespace Managers
{
    public class NexusManager : Manager<PlayerUnit, NexusManager>
    {
        Transform nexusTransform;
        public Transform GetTransform { get { return nexusTransform; } }
        public override void Init()
        {
            foreach (var nexus in Object.FindObjectsOfType<PlayerUnit>().ToList())
            {
                Add(nexus);
                nexusTransform = nexus.transform;
            }

            base.Init();
        }
    }
}
