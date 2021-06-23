﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityHelpers.View;

namespace UnityHelpers
{ 
    [InitializeOnLoad]
    public class ViewStateControllerHierachyUtil
    {
        static GUIContent visibleOnIcon;
            static GUIContent visibleOffIcon;
         static Dictionary<int, ViewStateController> viewStateControllerStates;

         static ViewStateControllerHierachyUtil()
         {
             // Init
             //texture = AssetDatabase.LoadAssetAtPath ("Assets/Images/Testicon.png", typeof(Texture2D)) as Texture2D;
             //EditorApplication.update += OnEditorUpdate;
             viewStateControllerStates = new Dictionary<int, ViewStateController>();
             visibleOnIcon = EditorGUIUtility.IconContent("animationvisibilitytoggleon");
             visibleOffIcon = EditorGUIUtility.IconContent("animationvisibilitytoggleoff");
             EditorApplication.hierarchyWindowItemOnGUI += OnHierachyUpdate;
             EditorApplication.hierarchyWindowChanged += OnHierachyChanged;
             EditorApplication.update += OnUpdate;
             UpdateStatesList();
         }

         private static void OnUpdate()
         {
             UpdateStatesList();
             EditorApplication.update -= OnUpdate;
         }

         private static void OnHierachyChanged()
         {
             UpdateStatesList();
         }

         public static void UpdateStatesList()
         {
             viewStateControllerStates.Clear();
             foreach (var controller in GameObject.FindObjectsOfType<ViewStateController>())
             {
                 if (controller.states == null) return;
                 foreach (var state in controller.states)
                {
                    if (state == null) return;
                    viewStateControllerStates[state.GetInstanceID()] = controller;
                }                     
             }
         }

         static void OnHierachyUpdate(int instanceID, Rect selectionRect)
         {
             Rect r = new Rect();
             r.y = selectionRect.y + 3;
             r.height = selectionRect.height;
             r.x = selectionRect.xMax - 20;
             r.width = 18;
             if (viewStateControllerStates.ContainsKey(instanceID))
             {
                 var obj = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
                 if (obj == null)
                     return;

                 var content = obj.activeSelf ? visibleOnIcon : visibleOffIcon;
                 var b = GUI.Toggle(r, obj.activeSelf, content, GUIStyle.none);
                 if (b != obj.activeSelf)
                 {
                     if(b)
                        viewStateControllerStates[instanceID].SetState(obj);
                 }
             }
         }
    }
}
