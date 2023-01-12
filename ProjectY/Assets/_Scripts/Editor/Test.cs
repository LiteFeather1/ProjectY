using UnityEngine;
using UnityEditor;

public static class MenuItemTest
{
    [MenuItem("GameObject/MyCategory/RenameChildren", false, 10)]
    public static void RenameChildrien()
    {
        Transform t = Selection.activeTransform;

        Transform[] children = t.GetComponentsInChildren<Transform>();
        foreach (var child in children)
        {
            if (child == t)
                continue;
            child.name = t.name+ " " + child.name;
        }
    }
}