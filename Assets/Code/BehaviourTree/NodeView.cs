using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTreeSystem;
using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEditor;
public class NodeView : UnityEditor.Experimental.GraphView.Node
{
    #region VARIABLES

    public Action<NodeView> OnNodeSelected;
    public Action<NodeView> OnNodeDeleted;
    private BehaviourTreeSystem.Node node;
    private Port input;
    private Port output;

    #endregion

    #region PROPERTIES

    public BehaviourTreeSystem.Node Node => node;
    public Port Input => input;
    public Port Output => output;

    #endregion

    #region CONSTRUCTORS

    public NodeView(BehaviourTreeSystem.Node node) : base("Assets/Code/Editor/BehaviourTreeEditor/NodeView.uxml")
    {
        this.node = node;
        this.title = node.name;
        this.viewDataKey = node.Guid;

        style.left = node.Position.x;
        style.top = node.Position.y;

        CreateOutputPorst();
        CreateInputPorst();
        SetupClasses();

    }

    private void SetupClasses()
    {
        switch (Node)
        {
            case ActionNode:
                AddToClassList("action");
                break;
            case CompositeNode:
                AddToClassList("composite");
                break;
            case DecoratorNode:
                AddToClassList("decorator");
                break;
            case RootNode:
                AddToClassList("root");
                break;
        }

    }

    private void CreateInputPorst()
    {
        switch (Node)
        {
            case ActionNode:
            case CompositeNode:
            case DecoratorNode:
                input = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
                break;
        }

        if (input != null)
        {
            input.portName = "";
            inputContainer.Add(input);
        }

        inputContainer.style.alignItems = Align.Center;
    }

    private void CreateOutputPorst()
    {
        switch (Node)
        {
            case ActionNode:
                break;
            case CompositeNode:
                output = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Multi, typeof(bool));
                break;
            case DecoratorNode:
            case RootNode:
                output = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Single, typeof(bool));
                break;
        }

        if (output != null)
        {
            output.portName = "";
            outputContainer.Add(output);
        }

        outputContainer.style.alignSelf = Align.Center;
    }

    #endregion

    #region METHODS

    public override void SetPosition(Rect newPos)
    {
        base.SetPosition(newPos);
        Undo.RecordObject(node, "Behaviour Tree (Set Position)");
        Vector2 pos = new Vector2(newPos.xMin, newPos.yMin);
        node.Position = pos;
        EditorUtility.SetDirty(node);
    }

    public override void OnSelected()
    {
        base.OnSelected();
        if (node != null)
            OnNodeSelected?.Invoke(this);
    }

    public override void OnUnselected()
    {
        base.OnUnselected();
        if (node == null)
            OnNodeDeleted?.Invoke(this);
    }

    #endregion
}
