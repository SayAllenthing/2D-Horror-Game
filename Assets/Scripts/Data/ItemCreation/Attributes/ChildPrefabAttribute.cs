#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

public class ChildPrefabAttribute : ItemAttribute
{

    public GameObject prefab;

#if UNITY_EDITOR
    public override void DoLayout()
    {
		prefab = EditorGUILayout.ObjectField("Prefab", prefab, typeof(GameObject)) as GameObject;
    }
#endif
}
