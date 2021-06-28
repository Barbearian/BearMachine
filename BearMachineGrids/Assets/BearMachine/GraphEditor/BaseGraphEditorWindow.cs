using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

namespace BearMachine
{
    public class BaseGraphEditorWindow : EditorWindow
    {

        private static MyGraph graph;
        private static BaseGraphView graphView;
        private static BaseGraphEditorWindow window;
        public static void Open(MyGraph _data)
        {

            window = GetWindow<BaseGraphEditorWindow>("Graph Editor");
            

            window.Initialize(_data);
        }

        private void Initialize(MyGraph _data)
        {
            graph = _data;
            graph.Status();
            EditorUtility.SetDirty(graph);
            //Clean();
            ConstructGraphView();
            GenerateToolBar();
            //Load();
        }

        private void ConstructGraphView()
        {
            graphView = new BaseGraphView(this, graph);
            graphView.StretchToParentSize();
            rootVisualElement.Add(graphView);

        }

        private void GenerateToolBar()
        {
            //var toolBar = new UnityEditor.UIElements.Toolbar();

            /*
            var saveButton = new Button(clickEvent: () => { Save(); });
            saveButton.text = "Save";
            toolBar.Add(saveButton);
            */

            //rootVisualElement.Add(toolBar);
        }

        public void Save() {

        }

        public void Clean() {
            if (graphView != null)
            {
                rootVisualElement.Remove(graphView);
            }

        }
    }
}