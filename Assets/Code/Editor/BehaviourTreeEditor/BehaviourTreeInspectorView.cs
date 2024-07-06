using UnityEngine.UIElements;
using UnityEditor;

public class BehaviourTreeInspectorView : VisualElement
{
    #region VARIABLES

    private UnityEditor.Editor editor;

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

    internal void UpdateSelection(NodeView nodeView)
    {
        Clear();

        UnityEngine.Object.DestroyImmediate(editor);

        editor = UnityEditor.Editor.CreateEditor(nodeView.Node);
        IMGUIContainer container = new IMGUIContainer(() => { editor.OnInspectorGUI(); });
        Add(container);
    }

    #endregion

    #region METHODS

    #endregion
}
