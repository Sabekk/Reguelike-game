using BehaviourTreeSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class BehaviourTreeView : GraphView
{
    #region VARIABLES

    public Action<NodeView> OnNodeSelected;
    private BehaviourTree tree;

    #endregion

    #region PROPERTIES

    #endregion

    #region CONSTRUCTORS

    public BehaviourTreeView()
    {
        Insert(0, new GridBackground());

        this.AddManipulator(new ContentZoomer());
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Code/Editor/BehaviourTreeEditor/BehaviourTreeEditor.uss");
        styleSheets.Add(styleSheet);
    }

    #endregion

    #region CLASSES

    public new class UxmlFactory : UxmlFactory<BehaviourTreeView, UxmlTraits> { }

    #endregion

    #region METHODS

    public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
    {
        base.BuildContextualMenu(evt);

        Vector2 mousePosition = (evt.localMousePosition - new Vector2(viewTransform.position.x, viewTransform.position.y)) / scale;
        {
            var actionTypes = TypeCache.GetTypesDerivedFrom<ActionNode>();
            foreach (var type in actionTypes)
                evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name}", (a) => CreateNode(type, mousePosition));

            var decoratorTypes = TypeCache.GetTypesDerivedFrom<DecoratorNode>();
            foreach (var type in decoratorTypes)
                evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name}", (a) => CreateNode(type, mousePosition));

            var compositeTypes = TypeCache.GetTypesDerivedFrom<CompositeNode>();
            foreach (var type in compositeTypes)
                evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name}", (a) => CreateNode(type, mousePosition));
        }
    }

    internal void PopulateView(BehaviourTree tree)
    {
        this.tree = tree;

        graphViewChanged -= OnGraphViewChanged;

        DeleteElements(graphElements);

        graphViewChanged += OnGraphViewChanged;

        // Create node view
        foreach (var node in tree.Nodes)
            CreateNodeView(node);

        // Create edges

        foreach (var node in tree.Nodes)
        {
            var children = tree.GetChildren(node);

            foreach (var child in children)
            {
                NodeView parentView =  FindNodeView(node);
                NodeView childView =  FindNodeView(child);

                Edge edge = parentView.Output.ConnectTo(childView.Input);
                AddElement(edge);
            }
        }
    }
    
    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        return ports.ToList().Where(endPort => endPort.direction != startPort.direction && endPort.node != startPort.node).ToList();
    }

    private void CreateNode(System.Type type)
    {
        BehaviourTreeSystem.Node node = tree.CreateNode(type);
        CreateNodeView(node);
    }

    private void CreateNode(System.Type type, Vector2 position)
    {
        BehaviourTreeSystem.Node node = tree.CreateNode(type);
        node.SetPosition(position);
        CreateNodeView(node);
    }

    private void CreateNodeView(BehaviourTreeSystem.Node node)
    {
        NodeView nodeView = new NodeView(node);
        nodeView.OnNodeSelected = OnNodeSelected;
        AddElement(nodeView);
    }

    private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
    {
        if (graphViewChange.elementsToRemove != null)
        {
            graphViewChange.elementsToRemove.ForEach(e =>
            {
                NodeView nodeView = e as NodeView;
                if (nodeView != null)
                    tree.DeleteNode(nodeView.Node);

                Edge edge = e as Edge;
                if (edge != null)
                {
                    NodeView parentView = edge.output.node as NodeView;
                    NodeView childView = edge.input.node as NodeView;
                    tree.RemoveChild(parentView.Node, childView.Node);
                }

            });
        }

        if (graphViewChange.edgesToCreate != null)
        {
            graphViewChange.edgesToCreate.ForEach(e =>
            {
                NodeView parentView = e.output.node as NodeView;
                NodeView childView = e.input.node as NodeView;
                tree.AddChild(parentView.Node, childView.Node);
            });
        }

        return graphViewChange;
    }

    private NodeView FindNodeView(BehaviourTreeSystem.Node node)
    {
        return GetNodeByGuid(node.Guid) as NodeView;
    }

    #endregion
}
