using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Collections;
using UnityEditor;

namespace Cyberultimate.Editor
{
    public static class SerializedPropertyHelper
    {
        public static IEnumerable<SerializedProperty> GetChildren(this SerializedProperty property)
        {
            var copy= property.Copy();
           foreach(var item in copy)
            {
                yield return item as SerializedProperty;
            }
        }
        public static IEnumerable<SerializedProperty> GetArrayElements(this SerializedProperty property)
        {
            for(int x=0;x<property.arraySize;x++)
            {
                yield return property.GetArrayElementAtIndex(x);
            }
        }
    }
}
