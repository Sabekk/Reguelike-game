using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BehaviourTreeSystem
{
    [CreateAssetMenu()]
    public class BehaviourTree : ScriptableObject
    {
        #region VARIABLES

        [SerializeField, HideInInspector] private Node node;
        [SerializeField, HideInInspector] private NodeState nodeState;
        [SerializeField] private Node rootNode;
        [SerializeField] private List<Node> nodes;

        #endregion

        #region PROPERTIES

        public List<Node> Nodes => nodes;
        public Node RootNode => rootNode;

        #endregion

        #region METHODS

        public NodeState Update()
        {
            if (node.State == NodeState.RUNNING)
                nodeState = node.Update();

            return nodeState;
        }

        public Node CreateRootNode()
        {
            rootNode = CreateNode(typeof(RootNode)) as RootNode;
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
            return rootNode;
        }

        public Node CreateNode(System.Type type)
        {
            Node node = ScriptableObject.CreateInstance(type) as Node;
            node.name = type.Name;
            node.Guid = GUID.Generate().ToString();

            nodes.Add(node);

            AssetDatabase.AddObjectToAsset(node, this);
            AssetDatabase.SaveAssets();

            return node;
        }

        public void DeleteNode(Node node)
        {
            nodes.Remove(node);
            AssetDatabase.RemoveObjectFromAsset(node);
            AssetDatabase.SaveAssets();
        }

        public void AddChild(Node parent, Node child)
        {
            if (parent is RootNode root)
                root.SetChild(null);

            if (parent is DecoratorNode decorator)
                decorator.SetChild(child);

            if (parent is CompositeNode composite)
                composite.AddChild(child);
        }

        public void RemoveChild(Node parent, Node child)
        {
            if (parent is DecoratorNode decorator)
                decorator.SetChild(null);

            if (parent is CompositeNode composite)
                composite.RemoveChild(child);
        }

        public List<Node> GetChildren(Node parent)
        {
            List<Node> list = new();

            if (parent is RootNode root)
                if (root.Child != null)
                    list.Add(root.Child);

            if (parent is DecoratorNode decorator)
                if (decorator.Child != null)
                    list.Add(decorator.Child);

            if (parent is CompositeNode composite)
            {
                return composite.Childs;
            }

            return list;
        }

        #endregion
    }
}
