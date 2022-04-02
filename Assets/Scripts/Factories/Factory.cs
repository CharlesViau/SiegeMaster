using System;
using System.Collections.Generic;
using System.Linq;
using Managers.Template;
using UnityEngine;
using Object = UnityEngine.Object;

// ReSharper disable InconsistentNaming

namespace Factories
{
    /// <summary>
    /// Factory Class made to Instantiate Unity Prefabs.
    /// <list type="bullet"><item>
    /// <description>Prefabs MUST be named the Same way as they are in the Enum.</description></item><item>
    /// <description>'T' Must have an internal public class that implements 'IArgs'.
    /// it will contain the construction Args for the create function.</description></item></list>
    /// Example of inheritance:
    ///<list type="bullet"><item>
    /// <description>EnemyFactory : &lt;Enemy, EnemyTypes, Enemy.ConstructionArgs&gt;</description></item></list>
    /// </summary>
    /// <typeparam name="T">Type of the object it creates</typeparam>
    /// <typeparam name="E">Enum of the types of thing it can instantiate</typeparam>
    /// <typeparam name="A">Argument Class for the create method</typeparam>
    public class Factory<T, E, A> : IFactory<T, E, A>
        where T : ICreatable<A>, IUpdaptable, IPoolable
        where E : Enum
        where A : IArgs
    {
        public Factory(string prefabLocation)
        {
            _prefabDictionary = new Dictionary<Type, GameObject>();
            PrefabLocation = prefabLocation;
        }

        #region Properties and Variables

        /// <summary>
        /// It will start Loading automatically from "Resources/" root. Write the rest of the path.
        /// </summary>
        private readonly string PrefabLocation;

        private readonly Dictionary<Type, GameObject> _prefabDictionary;
        
        #endregion

        #region Public Methods

        public void Init()
        {
            //Get all Prefab types from the enum and put them in an Array.
            var allPrefabTypes = Enum.GetValues(typeof(E)).Cast<Type>().ToArray();

            //Load all Prefabs to the Dictionary
            foreach (var element in allPrefabTypes)
            {
                _prefabDictionary.Add(element, Resources.Load<GameObject>(PrefabLocation + element));
            }
        }

        /// <summary>
        /// Attempt to get the object from the object pool and &quot;depool&quot; it.
        /// If it doesn't exist, instantiate a new one.
        /// </summary>
        /// <param name="type">Enum Value</param>
        /// <param name="constructionArgs">Args needed for the initialization</param>
        /// <returns></returns>
        public T Create(Type type, A constructionArgs)
        {
            var obj = (T) ObjectPool.Instance.Depool(type);

            if (obj == null)
            {
                obj = Object.Instantiate(_prefabDictionary[type]).GetComponent<T>();
                obj.Init();
            }

            obj.Construct(constructionArgs);

            return obj;
        }

        #endregion
    }

    // ReSharper disable once UnusedTypeParameter
    public interface IFactory<out T, in E, in A>
        where T : ICreatable<A>, IUpdaptable, IPoolable
        where E : Enum
        where A : IArgs
    {
        public void Init();
        public T Create(Type type, A constructionArgs);
    }

    public interface IArgs
    {
    }

    public interface ICreatable<in A> where A : IArgs
    {
        void Construct(A constructionArgs);
    }
}