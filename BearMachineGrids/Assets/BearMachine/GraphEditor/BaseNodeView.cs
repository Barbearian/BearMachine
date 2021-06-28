using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using UnityEngine.UI;

namespace BearMachine
{
    public class BaseNodeView : Node
    {
        public MyNode node;
        public System.Guid GUID;
        public Vector2 defaultNodeSize = new Vector2(x: 150, y: 200);

        public BaseNodeView(MyNode node, Vector2 position) {
            this.node = node;

            Init();
            MoveTo(position);
        }

        private void Init() {
            GUID = System.Guid.NewGuid();
            title = node.nodeName;
            PortInit();

            SetPosition(new Rect(position: Vector2.zero, defaultNodeSize));

            ExtensionInit();


            //Add editor
            Foldout foldout = new Foldout();
            
            Editor editor = Editor.CreateEditor(node);
            IMGUIContainer container = new IMGUIContainer(() => editor.OnInspectorGUI());
            foldout.Add(container);

            extensionContainer.Add(foldout);
            //Add editor

            RefreshExpandedState();
            RefreshPorts();



        }

        private void ExtensionInit() {
            //ExposeSelf
            ObjectField self = new ObjectField()
            {
                value = node,
                //pickingMode = PickingMode.Position,
                allowSceneObjects = false,

            };
            //self.SetEnabled(false);
            extensionContainer.Add(self);

            SerializedObject obj = new SerializedObject(node);
            TextField field = new TextField() {
                bindingPath = "nodeName"

            };

            field.Bind(obj);

            titleContainer.Clear();
            titleContainer.Add(field);


            
            

        }

        private void PortInit() {

            Port inputPort = GeneratePort(Direction.Input, Port.Capacity.Multi);
            Port outputPort = GeneratePort(Direction.Output, Port.Capacity.Multi);
            inputPort.portName = "Input";
            outputPort.portName = "Output";

            inputPort.portColor = Color.red;
            outputPort.portColor = Color.blue;

            outputContainer.Add(outputPort);
            inputContainer.Add(inputPort);
        }

        private Port GeneratePort(Direction direction, Port.Capacity capacity = Port.Capacity.Multi)
        {
            return InstantiatePort(Orientation.Horizontal, direction, capacity, typeof(bool));
        }

        public void MoveTo(Vector2 position)
        {
            Rect pos = GetPosition();
            pos.position = position;
            SetPosition(pos);
        }

        public override void SetPosition(Rect newPos)
        {
            base.SetPosition(newPos);
            node.pos.x = newPos.xMin;
            node.pos.y = newPos.yMin;
        }








    }
}