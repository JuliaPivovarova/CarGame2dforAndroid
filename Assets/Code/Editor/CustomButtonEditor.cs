using Code.Tweens;
using UnityEditor;
using UnityEditor.UI;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Code.Editor
{
    [CustomEditor(typeof(CustomButton))]
    public class CustomButtonEditor: ButtonEditor
    {
        private SerializedProperty m_InteractableProperty;

        protected override void OnEnable()
        {
            m_InteractableProperty = serializedObject.FindProperty("m_Interactable");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(m_InteractableProperty);
            EditorGUI.BeginChangeCheck();

            serializedObject.ApplyModifiedProperties();
        }

        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();
            
            var duration = new PropertyField(serializedObject.FindProperty(CustomButton.Duration));
            var strength = new PropertyField(serializedObject.FindProperty(CustomButton.Strength));
            var tweenLabel = new Label("Tweens");
            var interactebleLabel = new Label("Standart Settings");
            
            root.Add(tweenLabel);
            root.Add(duration);
            root.Add(strength);
            root.Add(interactebleLabel);
            root.Add(new IMGUIContainer(OnInspectorGUI));
            
            return root;
        }
    }
}