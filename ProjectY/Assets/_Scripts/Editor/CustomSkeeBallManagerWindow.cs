using UnityEditor;

namespace SkeeBall
{
    [CustomEditor(typeof(SkeeBallManager))]
    public class CustomSkeeBallManagerWindow : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var skeeBallManager = (SkeeBallManager)target;
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Custom Editor", EditorStyles.boldLabel);
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.IntField("Ball Count", skeeBallManager.BallCount);
        }
    }
}
