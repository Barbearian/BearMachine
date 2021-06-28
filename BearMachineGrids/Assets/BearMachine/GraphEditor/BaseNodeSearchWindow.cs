using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;

namespace BearMachine{ 
    public class BaseNodeSearchWindow :ScriptableObject, ISearchWindowProvider
    {
        public BaseGraphView view;
        public BaseGraphEditorWindow window;

        public static BaseNodeSearchWindow instance;

        public void Init(BaseGraphView view) {
            this.view = view;
            this.window = view.editorWindow;
        }

        public static BaseNodeSearchWindow GetInstance(BaseGraphView view) {
            if (!instance)
            {
                instance = ScriptableObject.CreateInstance<BaseNodeSearchWindow>();
            }

            instance.Init(view);
            return instance;
        }

        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            var tree = new List<SearchTreeEntry> {
                new SearchTreeGroupEntry(new GUIContent(text: "Create"), level: 0),
                new SearchTreeGroupEntry(new GUIContent(text: "Node"), level:1),
                new SearchTreeEntry(new GUIContent(text: "Basic Node")){
                    userData = NodeType.BasicNode, level = 2
                },
                new SearchTreeEntry(new GUIContent(text: "State Node")){
                    userData = NodeType.StateNode, level = 2
                },
            };
            return tree;
        }

        public bool OnSelectEntry(SearchTreeEntry searchTreeEntry, SearchWindowContext context)
        {
            Vector2 position = GetMousePosition(context);
            switch (searchTreeEntry.userData)
            {
                case NodeType.BasicNode:
                    {
                        //Debug.Log("Hellow Basic Node");
                        Vector2 pos = GetMousePosition(context);
                        MyNode node = ScriptableObject.CreateInstance<MyNode>();
                        BaseNodeView baseNodeView = new BaseNodeView(node, pos) {
                            title = "Node"
                        };
                        view.AddNodeView(baseNodeView);
                    }
                    break;

               


                default:
                    break;
            }
            return true;
        }

        public Vector2 GetMousePosition(SearchWindowContext context)
        {
            VisualElement dest = window.rootVisualElement.parent;
            Vector2 pos = context.screenMousePosition - window.position.position;
            Vector2 worldMousePosition = window.rootVisualElement.ChangeCoordinatesTo(dest, pos);
            Vector2 localMousePosition = view.contentViewContainer.WorldToLocal(worldMousePosition);
            return localMousePosition;
        }

        public enum NodeType
        {
            BasicNode,
            StateNode
        }
    }
}