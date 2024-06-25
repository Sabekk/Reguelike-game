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
        [SerializeField] private List<Node> nodes;

        #endregion

        #region PROPERTIES

        public List<Node> Nodes => nodes;

        #endregion

        #region METHODS

        public NodeState Update()
        {
            if (node.State == NodeState.RUNNING)
                nodeState = node.Update();

            return nodeState;
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

        #endregion
    }
}
