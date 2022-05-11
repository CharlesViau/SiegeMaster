using System.Linq;
using General;
using Units.Types;
using UnityEngine;

namespace Managers
{
    public class WaypointsManager : Manager<Waypoints, WaypointsManager>
    {
        Waypoints waypoint;
        public Transform GetTransform { get { return waypoint.transform; } }
        public override void Init()
        {
            foreach (var waypoint in Object.FindObjectsOfType<Waypoints>().ToList())
            {
                Add(waypoint);
            }

            base.Init();
        }
    }
}
