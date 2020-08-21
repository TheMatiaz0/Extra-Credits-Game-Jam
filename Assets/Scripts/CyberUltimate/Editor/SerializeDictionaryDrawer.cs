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
using Cyberultimate.Editor;
using Cyberultimate;
namespace Cyberultimate.Editor
{
    [CustomPropertyDrawer(typeof(SerializedDictionary<,>))]
    public class SerializeDictionaryDrawer : PropertyDrawer
    {
        private ReorderableList reorderable;
        private SerializedProperty keys, values;
        private bool isCurCorrect = false;
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return reorderable?.GetHeight() ?? 0;
           
        }
        private bool IsCorrect()
        {
          for(int x=0;x<keys.arraySize;x++)
            {
                for (int y = 0; y < keys.arraySize; y++)
                    if (x != y && SerializedProperty.DataEquals(keys.GetArrayElementAtIndex(x), keys.GetArrayElementAtIndex(y)))
                        return false;

            }
            return true;
        }
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (reorderable == null)
                Init(property, label);
            isCurCorrect = IsCorrect();
            reorderable.DoList(position);
        }
        private void Init(SerializedProperty property, GUIContent label)
        {
            const string KEY_WORD = "<b>Key</b>";
            GUIStyle kStyle = new GUIStyle("label") { richText = true };
            float labelSize = kStyle.CalcSize(new GUIContent(KEY_WORD)).x;
            keys = property.FindPropertyRelative("keys");
            values = property.FindPropertyRelative("values");
            reorderable = new ReorderableList(property.serializedObject, keys);
            reorderable.drawElementCallback += (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                var key = keys.GetArrayElementAtIndex(index);
                var val = values.GetArrayElementAtIndex(index);


                float before = EditorGUIUtility.labelWidth;

                GUIStyle style = new GUIStyle("label");
                bool rBefore = EditorStyles.label.richText;
                EditorStyles.label.richText = true;
                EditorGUIUtility.labelWidth = style.CalcSize(new GUIContent("Key")).x;
                EditorGUI.PropertyField(new Rect(rect) { height = EditorGUI.GetPropertyHeight(key), y = rect.y }, key,new GUIContent("<b>Key</b>"));
                EditorGUIUtility.labelWidth = style.CalcSize(new GUIContent("Value")).x;
                EditorGUI.PropertyField(new Rect(rect) { height = EditorGUI.GetPropertyHeight(val), y = rect.y + EditorGUI.GetPropertyHeight(key) }, val, new GUIContent("<b>Value</b>"));
                EditorGUIUtility.labelWidth = before;
                EditorStyles.label.richText = rBefore;

            };
            reorderable.elementHeightCallback += (int index) =>
            {
                return EditorGUI.GetPropertyHeight(keys.GetArrayElementAtIndex(index)) + EditorGUI.GetPropertyHeight(values.GetArrayElementAtIndex(index)) + EditorGUIUtility.singleLineHeight * 0.2f;
            };
            reorderable.elementHeight = EditorGUIUtility.singleLineHeight;
            reorderable.drawHeaderCallback += (Rect rect) =>
            {
                EditorGUI.LabelField(rect,$"{property.displayName}{(isCurCorrect?string.Empty: " (<color=#e6e600><b>Multiple keys!</b></color>)")}",new GUIStyle("label") { richText=true});
            };
            reorderable.onAddCallback += (ReorderableList list) =>
            {
                values.arraySize++;
                keys.arraySize++;
            };
            reorderable.onRemoveCallback += (ReorderableList list) =>
              {
                  values.arraySize--;
                  keys.arraySize--;
              };

            

        }

    }
}

