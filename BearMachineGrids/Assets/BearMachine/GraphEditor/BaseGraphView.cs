using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace BearMachine
{
    public class BaseGraphView : GraphView
    {
        public BaseGraphEditorWindow editorWindow;

        public MyGraph graph;
        public BaseGraphView(BaseGraphEditorWindow editorWindow, MyGraph graph)
        {
            this.editorWindow = editorWindow;
            this.graph = graph;
            //Make the view zoomable
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

            //Make the components Selectable
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            //Draw Grid
            var grid = new MyGridBackground();
            Insert(index: 0, grid);
            grid.StretchToParentSize();


            LoadGraph();
            AddSearchWindow();
        }

        private void AddSearchWindow()
        {
            BaseNodeSearchWindow searchWindow = BaseNodeSearchWindow.GetInstance(this);
            nodeCreationRequest = context => SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), searchWindow);
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            var compatiblePorts = new List<Port>();
            ports.ForEach(funcCall: (port) =>
            {
                if (startPort != port && startPort.node != port.node)
                {
                    compatiblePorts.Add(port);
                }
            });

            return compatiblePorts;
        }

        public void LoadGraph() {
            graphViewChanged -= OnGraphViewChanged;
            graphViewChanged += OnGraphViewChanged;

            Dictionary<MyNode, BaseNodeView> dic = new Dictionary<MyNode, BaseNodeView>();

            foreach (MyNode node in graph.graph) {
                dic.Add(node, CreateNodeView(node));
            }

           // Debug.Log("There are +"+dic.Count+" nodes");

            foreach (MyNode node1 in graph.graph) {
                if (dic.ContainsKey(node1))
                {
                    BaseNodeView view1 = dic[node1];
                    foreach (MyNode node2 in node1.kids)
                    {
                        if (dic.ContainsKey(node2))
                        {
                            BaseNodeView view2 = dic[node2];
                            Link(view1,view2);
                        }
                    }
                }
               
            }
        }

        private void Link(BaseNodeView view1, BaseNodeView view2) {
            Port output = view1.outputContainer[0] as Port;
            Port input = view2.inputContainer[0] as Port;
            Edge edge = output.ConnectTo(input);

            


            //Debug.Log("There is a edge");
            AddElement(edge);
        }

        private BaseNodeView CreateNodeView(MyNode node) {
            Vector2 pos = node.pos;
            BaseNodeView nodeView = new BaseNodeView(node,pos);
            AddElement(nodeView);

            return nodeView;
        }

        public void AddNodeView(BaseNodeView node) {
            AddElement(node);
            graph.AddNode(node.node);
        }
        
        private GraphViewChange OnGraphViewChanged(GraphViewChange change) {
            if (change.elementsToRemove != null) {
                foreach (GraphElement elem in change.elementsToRemove) {
                    BaseNodeView view = elem as BaseNodeView;
                    if (view!=null)
                    {
                        //Debug.Log("Detected removed Node");
                        graph.RemoveNode(view.node);
                    }

                    Edge edge = elem as Edge;
                    if (edge != null) {
                        BaseNodeView view1 = edge.output.node as BaseNodeView;
                        BaseNodeView view2 = edge.input.node as BaseNodeView;

                        MyNode node1 = view1.node;
                        MyNode node2 = view2.node;

                        graph.Delink(node1, node2);
                    }

                    
                    
                }
            }

            if (change.edgesToCreate != null) {
                foreach (Edge edge in change.edgesToCreate)
                {
                    BaseNodeView view1 = edge.output.node as BaseNodeView;
                    BaseNodeView view2 = edge.input.node as BaseNodeView;

                    MyNode node1 = view1.node;
                    MyNode node2 = view2.node;

                    graph.Link(node1, node2);
                }
            }
            
            return change;
        }
        

        public class MyGridBackground : GridBackground { }
    }
}