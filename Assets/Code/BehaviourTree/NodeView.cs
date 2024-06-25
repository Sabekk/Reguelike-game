using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTreeSystem;
using System;
using UnityEditor.Experimental.GraphView;

public class NodeView : UnityEditor.Experimental.GraphView.Node
{
    #region VARIABLES

    private BehaviourTreeSystem.Node node;
    private Port input;
    private Port output;

    #endregion

    #region PROPERTIES

    public BehaviourTreeSystem.Node Node => node;

    #endregion

    #region CONSTRUCTORS

    public NodeView(BehaviourTreeSystem.Node node)
    {
        this.node = node;
        this.title = node.name;
        this.viewDataKey = node.Guid;

        style.left = node.Position.x;
        style.top = node.Position.y;

        CreateInputPorst();
        CreateOutputPorst();

    }

    private void CreateOutputPorst()
    {

    }

    private void CreateInputPorst()
    {

    }

    #endregion

    #region METHODS

    public override void SetPosition(Rect newPos)
    {
        base.SetPosition(newPos);
        Vector2 pos = new Vector2(newPos.xMin, newPos.yMin);
        node.Position = pos;
    }

    #endregion
}
