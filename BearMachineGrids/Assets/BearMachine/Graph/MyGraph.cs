using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BearMachine
{
    [CreateAssetMenu(menuName = "Graph/BaseGraph")]
    public class MyGraph : ScriptableObject
    {
        //public int size = 0;
        public List<MyNode> graph;
        public MyGraph()
        {
            graph = new List<MyNode>();
        }

        public void AddNode(MyNode node)
        {
            if (!graph.Contains(node))
            {
                graph.Add(node);
                //size += 1;
                AssetDatabase.AddObjectToAsset(node, this);

                //EditorUtility.SetDirty(this);
                AssetDatabase.SaveAssets();

            }

        }

        public void Status()
        {
            Debug.Log("I have " + graph.Count + " nodes");
        }

        public void RemoveNode(MyNode node)
        {
            if (graph.Contains(node))
            {
                graph.Remove(node);
                //size -= 1;
                AssetDatabase.RemoveObjectFromAsset(node);
                //EditorUtility.SetDirty(this);
                AssetDatabase.SaveAssets();
                //EditorUtility.SetDirty(this);

            }
        }

        public void Link(MyNode node1, MyNode node2)
        {
            //Debug.Log("Graph is Ready to link");
            node1.Link(node2);
            //EditorUtility.SetDirty(this);
        }

        public void Delink(MyNode node1, MyNode node2)
        {
            node1.Delink(node2);
            //EditorUtility.SetDirty(this);
        }

        public List<MyNode> GetKids(MyNode node)
        {
            return node.kids;
        }
    }
}