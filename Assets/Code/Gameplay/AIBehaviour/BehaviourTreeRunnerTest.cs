using BehaviourTreeSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTreeRunnerTest : MonoBehaviour
{
    #region VARIABLES

    [SerializeField] private BehaviourTree behaviourTree;
    [SerializeField] private bool isRunning;

    private BehaviourTree tree;

    #endregion

    #region PROPERTIES

    private BehaviourTree Tree
    {
        get
        {
            if (tree == null)
                tree = behaviourTree.Clone();
            return tree;
        }
    }

    #endregion

    #region METHODS

    private void Update()
    {
        if (isRunning)
            Tree.Update();
    }

    #endregion
}
