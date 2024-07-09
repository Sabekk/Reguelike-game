using BehaviourTreeSystem;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Callbacks;

public class BehaviourTreeEditor : EditorWindow
{
    #region VARIABLES

    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;

    private BehaviourTreeInspectorView inspectorView;
    private BehaviourTreeView treeView;

    #endregion

    #region METHODS

    [MenuItem("Tools/BehaviourTreeEditor")]
    public static void ShowExample()
    {
        BehaviourTreeEditor wnd = GetWindow<BehaviourTreeEditor>();
        wnd.titleContent = new GUIContent("BehaviourTreeEditor");
    }

    #endregion

    #region UNITY_METHODS

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // Instantiate UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Code/Editor/BehaviourTreeEditor/BehaviourTreeEditor.uxml");
        visualTree.CloneTree(root);

        inspectorView = root.Q<BehaviourTreeInspectorView>();
        treeView = root.Q<BehaviourTreeView>();
        treeView.OnNodeSelected = OnNodeSelectionChanged;
        treeView.OnNodeDeleted = OnNodeDeleted;

        OnSelectionChange();
    }

    private void OnSelectionChange()
    {
        BehaviourTree tree = Selection.activeObject as BehaviourTree;

        if (tree && AssetDatabase.CanOpenAssetInEditor(tree.GetInstanceID()))
        {
            treeView.PopulateView(tree);
        }
    }

    #endregion

    #region METHODS

    [OnOpenAsset]
    public static bool OnOpenAsset(int instanceId, int line)
    {
        if(Selection.activeObject is BehaviourTree)
        {
            ShowExample();
            return true;
        }
        return false;
    }

    private void OnNodeSelectionChanged(NodeView node)
    {
        inspectorView.UpdateSelection(node);
    }

    private void OnNodeDeleted(NodeView node)
    {
        inspectorView.ClearSelection();
    }

    #endregion
}
