using BehaviourTreeSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

public class BehaviourTreeView : GraphView
{
    #region VARIABLES

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

        var types = TypeCache.GetTypesDerivedFrom<ActionNode>();
        foreach (var type in types)
            evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name}", (a) => CreateNode(type));
    }

    internal void PopulateView(BehaviourTree tree)
    {
        this.tree = tree;

        graphViewChanged -= OnGraphViewChanged;

        DeleteElements(graphElements);

        graphViewChanged += OnGraphViewChanged;

        foreach (var node in tree.Nodes)
            CreateNodeView(node);
    }

    private void CreateNode(System.Type type)
    {
        BehaviourTreeSystem.Node node = tree.CreateNode(type);
        CreateNodeView(node);
    }

    private void CreateNodeView(BehaviourTreeSystem.Node node)
    {
        NodeView nodeView = new NodeView(node);
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
            });
        }
        return graphViewChange;
    }

    #endregion
}
