using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace Cyberultimate.Unity
{
    public abstract class MonoSingleton<T> : MonoBehaviour
    where T : MonoSingleton<T>
    {
        public static T Instance { get; private set; }
        protected virtual void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                Debug.LogError($"That MonoSingleton already exists ({((T)this).GetType().Name})");
                return;
            }
            Instance = (T)this;
        }
    }
}
