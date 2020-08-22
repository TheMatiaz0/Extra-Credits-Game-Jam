using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditorInternal;
using Cyberultimate.Unity;

namespace Cyberultimate.Editor
{
    [CustomPropertyDrawer(typeof(ReorderableArray<>))]
    public class ReorderableAttributeDrawer : PropertyDrawer
    {

        private class SerializedPropertyEqualer : IEqualityComparer<SerializedProperty>
        {
            public bool Equals(SerializedProperty x, SerializedProperty y)
            {
                return SerializedProperty.EqualContents(x, y);
            }

            public int GetHashCode(SerializedProperty obj)
            {
                return obj.displayName.GetHashCode();//i kn this is bad.
            }
        }

        private Dictionary<SerializedProperty, ReorderableList> lists = new Dictionary<SerializedProperty, ReorderableList>(new SerializedPropertyEqualer());
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (lists.TryGetValue(property, out var list))
            {
                return list.GetHeight();
            }
            else
                return 0;
        }
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var ar = property.FindPropertyRelative("_Array");
           if(lists.ContainsKey(property)==false)
            {
                var list = new ReorderableList(property.serializedObject, ar, true, true, true, true);
                list.drawHeaderCallback += (Rect rect) =>
                {
                    EditorGUI.LabelField(rect, property.displayName);
                };
                list.drawElementCallback += (Rect rect, int index, bool isActive, bool isFocused) =>
                {
                    float before = EditorGUIUtility.labelWidth;
                    EditorGUIUtility.labelWidth = new GUIStyle("label").CalcSize(new GUIContent($"100")).x;
                    EditorGUI.PropertyField(rect, ar.GetArrayElementAtIndex(index), new GUIContent($"{index}:"),true);
                    EditorGUIUtility.labelWidth = before;
                };
                list.elementHeightCallback += (int index) =>
                {
                    return EditorGUI.GetPropertyHeight(ar.GetArrayElementAtIndex(index));
                };
                lists[property] = list;

            }


            lists[property].DoList(position);
        }
    }
}
