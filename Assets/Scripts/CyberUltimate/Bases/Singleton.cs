using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Collections;
namespace Cyberultimate
{
    /// <summary>
    /// Singleton template.
    /// </summary>
    /// <typeparam name="SELF">Your class</typeparam>
    public abstract class Singleton<SELF>
        where SELF : Singleton<SELF>, new()
    {
        protected static Lazy<SELF> instance = new Lazy<SELF>(() => new SELF());
        public static SELF Instance
        {
            get => instance.Value;
        }

    }
}
