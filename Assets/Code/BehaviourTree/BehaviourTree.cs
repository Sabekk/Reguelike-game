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

            Undo.RecordObject(this, "Behaviour tree (CreateNode)");
            nodes.Add(node);

            AssetDatabase.AddObjectToAsset(node, this);
            Undo.RegisterCreatedObjectUndo(node, "Behaviour tree (CreateNode)");
            AssetDatabase.SaveAssets();

            return node;
        }

        public void DeleteNode(Node node)
        {
            Undo.RecordObject(this, "Behaviour tree (DeleteNode)");
            nodes.Remove(node);

            Undo.DestroyObjectImmediate(node);
            AssetDatabase.SaveAssets();
        }

        public void AddChild(Node parent, Node child)
        {
            if (parent is RootNode root)
            {
                Undo.RecordObject(root, "Behaviour tree (AddChild)");
                root.SetChild(child);
                EditorUtility.SetDirty(root);
            }

            if (parent is DecoratorNode decorator)
            {
                Undo.RecordObject(decorator, "Behaviour tree (AddChild)");
                decorator.SetChild(child);
                EditorUtility.SetDirty(decorator);
            }

            if (parent is CompositeNode composite)
            {
                Undo.RecordObject(composite, "Behaviour tree (AddChild)");
                composite.AddChild(child);
                EditorUtility.SetDirty(composite);
            }
        }

        public void RemoveChild(Node parent, Node child)
        {
            if (parent is RootNode root)
            {
                Undo.RecordObject(root, "Behaviour tree (RemoveChild)");
                root.SetChild(null);
                EditorUtility.SetDirty(root);
            }

            if (parent is DecoratorNode decorator)
            {
                Undo.RecordObject(decorator, "Behaviour tree (RemoveChild)");
                decorator.SetChild(null);
                EditorUtility.SetDirty(decorator);
            }

            if (parent is CompositeNode composite)
            {
                Undo.RecordObject(composite, "Behaviour tree (RemoveChild)");
                composite.RemoveChild(child);
                EditorUtility.SetDirty(composite);
            }
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
                return composite.Children;
            }

            return list;
        }

        public BehaviourTree Clone()
        {
            BehaviourTree tree = Instantiate(this);
            tree.rootNode = tree.RootNode.Clone();
            return tree;
        }

        #endregion
    }
}
