using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using System.Collections;
using System.Collections.ObjectModel;
using System.Reflection;
namespace Cyberultimate.Unity
{
    public class EditableSerializeReferenceAttribute : PropertyAttribute
    {
        public readonly Type Type;

        public ReadOnlyCollection<Type> AvailableTypes => Array.AsReadOnly( staticData[Type]);
        private readonly static Dictionary<Type, Type[]> staticData = new Dictionary<Type, Type[]>();
        public EditableSerializeReferenceAttribute(Type type)
        {
            Type = type;
            if(staticData.ContainsKey(type)==false)
            {
                staticData[type] = AppDomain.CurrentDomain.GetAssemblies().SelectMany(item=>item.GetTypes()).Where(item => type.IsAssignableFrom(item) && item.IsAbstract == false && item.IsInterface == false).ToArray();
            }
          
        }
    }
}
