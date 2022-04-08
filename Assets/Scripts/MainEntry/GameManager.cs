using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Managers;
using Managers.Template;
using UnityEngine;

namespace MainEntry
{
    public class GameManager : MonoBehaviour
    {
        private readonly List<WrapperManager> _managers = new List<WrapperManager>();

        #region GameFlow (MainEntry)

        private void Awake()
        {
            AddManagersToList();
            InitManagers();
        }

        private void Start()
        {
            PostInitManagers();
        }

        private void Update()
        {
            RefreshManagers();
        }

        private void FixedUpdate()
        {
            FixedRefreshManagers();
        }

        private void OnDestroy()
        {
            CleanManagers();
        }

        #endregion

        #region Class Methods

        private void InitManagers()
        {
            foreach (var manager in _managers)
            {
                manager.Init();
            }
        }

        private void PostInitManagers()
        {
            foreach (var manager in _managers)
            {
                manager.PostInit();
            }
        }

        private void RefreshManagers()
        {
            foreach (var manager in _managers)
            {
                manager.Refresh();
            }
        }

        private void FixedRefreshManagers()
        {
            foreach (var manager in _managers)
            {
                manager.FixedRefresh();
            }
        }

        private void CleanManagers()
        {
            foreach (var manager in _managers)
            {
                manager.Clean();
            }
        }

        private void AddManagersToList()
        {/*
            var v = Assembly.GetAssembly(typeof(WrapperManager));
            var vv = v.GetTypes();
            var vvv = vv.Where(myType => myType.IsClass);

            foreach (var vvvv in vvv)
            {
                bool isAbstract = vvvv.IsAbstract;
                bool isSubclasses = vvvv.IsSubclassOf(typeof(WrapperManager));

                if (!isAbstract && isSubclasses)
                {
                    var x = vvvv;
                    var xx = x.GetProperty("Instance", BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy); //null

                    var xxx = xx.GetValue(null);
                    var xxxx = (WrapperManager) xxx;
                    _managers.Add(xxxx);
                }
            }
            */
        
            foreach (var type in
                     Assembly.GetAssembly(typeof(WrapperManager)).GetTypes().Where(myType =>
                         myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(WrapperManager))))
            {
                _managers.Add((WrapperManager)type.GetProperty("Instance", BindingFlags.Static|BindingFlags.Public|BindingFlags.FlattenHierarchy)?.GetValue(null));
            }
        }

        #endregion
    }
}