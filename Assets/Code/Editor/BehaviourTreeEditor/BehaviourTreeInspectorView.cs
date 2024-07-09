using UnityEngine.UIElements;
using Sirenix.OdinInspector.Editor;

public class BehaviourTreeInspectorView : VisualElement
{
    #region VARIABLES

    private OdinEditor editor;

    #endregion

    #region PROPERTIES

    #endregion

    #region CONSTRUCTORS

    public BehaviourTreeInspectorView()
    {

    }

    #endregion

    #region CLASSES

    public new class UxmlFactory : UxmlFactory<BehaviourTreeInspectorView, UxmlTraits> { }

    #endregion

    #region METHODS

    internal void UpdateSelection(NodeView nodeView)
    {
        ClearSelection();

        editor = (OdinEditor)OdinEditor.CreateEditor(nodeView.Node);
        IMGUIContainer container = new IMGUIContainer(() => { editor.OnInspectorGUI(); });
        Add(container);
    }

    internal void ClearSelection()
    {
        Clear();

        UnityEngine.Object.DestroyImmediate(editor);
    }

    #endregion
}
