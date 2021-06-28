using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace BearMachine
{
    public class MyNode : ScriptableObject
    {
        [HideInInspector]
        public Vector2 pos = Vector2.zero;
        [HideInInspector]
        public List<MyNode> kids;
        [HideInInspector]
        public List<MyNode> parent;
        //[HideInInspector]
        //public int size = 0;


        //[SerializeField] private BaseNode self { private set; }
        [HideInInspector] public string nodeName;


        public MyNode()
        {
            kids = new List<MyNode>();
            parent = new List<MyNode>();
            //self = this;
        }

        public void Link(MyNode node)
        {
            if (kids.Contains(node))
            {
                return;
            }

            kids.Add(node);
            node.parent.Add(this);

            EditorUtility.SetDirty(this);
            EditorUtility.SetDirty(node);
        }

        public void Delink(MyNode node)
        {
            if (!kids.Contains(node))
            {
                return;
            }

            //size -= 1;
            kids.Remove(node);
            node.parent.Remove(this);

            EditorUtility.SetDirty(this);
            EditorUtility.SetDirty(node);
        }

        public void DelinkAll()
        {
            foreach (MyNode node in parent)
            {
                node.Delink(this);
            }

            foreach (MyNode node in kids)
            {
                Delink(node);
            }
        }
    }
}