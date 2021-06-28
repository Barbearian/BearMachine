using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;

namespace BearMachine
{
    public class AssetHandler
    {
        [OnOpenAsset()]
        public static bool OpenEditor(int instanceId, int line)
        {
            MyGraph data = EditorUtility.InstanceIDToObject(instanceId) as MyGraph;
            if (data != null)
            {
                BaseGraphEditorWindow.Open(data);
                return true;
            }

            return false;
        }
    }

    [CustomEditor(typeof(MyGraph))]
    public class BaseGraphCustomEditor : Editor {
        
    }
}