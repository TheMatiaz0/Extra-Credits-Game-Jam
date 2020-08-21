using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Text.RegularExpressions;
using Cyberultimate.Unity;
using UnityEditorInternal;
using Cyberultimate;
namespace Cyberultimate.Editor
{
    [CustomPropertyDrawer(typeof(EditableSerializeReferenceAttribute))]
    public class EditableSerializeReferenceDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {

            return reordable?.GetHeight() ?? 0; 
        }


      
        private bool changed = false;
        int selectIndex=-1;
        private ReorderableList reordable;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            string GenerateUnityTypeName(Type type)
            {
                Regex regex = new Regex(".*?,");
                string result = regex.Match(type.Assembly.ToString()).Value;
                result = result.Remove(result.Length - 1);
                return $"{result} {type.FullName.Replace('+', '/')}";
            }


            EditableSerializeReferenceAttribute trueSerializeReference = attribute as EditableSerializeReferenceAttribute;
            var types = trueSerializeReference.AvailableTypes;
             selectIndex = (types.Index(item =>
            {
                var unityName = GenerateUnityTypeName(item);
                return unityName == property.managedReferenceFullTypename;
                //i tried to remember select index to save up time, but unity does sth weird with drawers and that's not work
            }) ) ?? -1;
            if (reordable is null)
            {
                reordable = new ReorderableList(new int[] { 0, 1 }, typeof(int), false, true, false, false);
                reordable.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
                 {
                     switch (index)
                     {
                         case 0:
                             var before = selectIndex;
                             
                             EditorGUI.LabelField(new Rect(rect) { width = rect.width / 2 }, "Type:");
                             Rect r2 = new Rect(rect) { width = rect.width / 2, x = rect.x + rect.width / 2 };
                        
                             GenericMenu menu = new GenericMenu();
                             var content = types.Select(item => new GUIContent(item.FullName)).ToArray();
                             int indx = 0;
                             menu.AddItem(new GUIContent("Null"), false, () =>
                             {
                                 property.managedReferenceValue = null;
                                 property.serializedObject.ApplyModifiedProperties();
                                 });
                             foreach (var element in content)
                             {
                                 var curIndx = indx++;
                                 menu.AddItem(element,false,(userData)=>
                                 {
                                     property.managedReferenceValue = Activator.CreateInstance((Type)userData);
                                     property.serializedObject.ApplyModifiedProperties();
                                 },types[curIndx]);
                                 
                             }

                             GUIContent dropContent = selectIndex == -1 ? new GUIContent("Null") : content[selectIndex];
                             if(EditorGUI.DropdownButton(r2, dropContent, FocusType.Passive))
                             {
                                 menu.ShowAsContext();
                             }
                             break;
                         case 1:
                             if (property.GetChildren().Any() == false)
                                 return;
                             EditorGUI.PropertyField(new Rect(rect) { height = EditorGUI.GetPropertyHeight(property) }, property, new GUIContent("Content"), true);
                             break;


                     }

                 };
                reordable.elementHeightCallback = (int index) =>
                {
                    switch (index)
                    {
                        case 0: return EditorGUIUtility.singleLineHeight;
                        case 1:
                            if(property.GetChildren().Any()==false)
                            {
                                return 0;
                            }
                            return EditorGUI.GetPropertyHeight(property);
                        default: return -1;
                    }
                };
                reordable.drawHeaderCallback += (Rect rect) =>
                  {
                      EditorGUI.LabelField(rect, property.displayName);
                  };
              
            }
            reordable.DoList(position);





        }
    }
}
