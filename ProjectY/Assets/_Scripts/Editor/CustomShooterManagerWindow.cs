using UnityEditor;

namespace Shooter
{
    [CustomEditor(typeof(ShooterManager))]
    public class CustomShooterManagerWindow : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var shooterManager = (ShooterManager)target;
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Custom Editor", EditorStyles.boldLabel);
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.IntField("Target Count", shooterManager.TargetCount);
        }
    }
}
