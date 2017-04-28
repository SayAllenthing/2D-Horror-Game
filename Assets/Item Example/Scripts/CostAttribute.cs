#if UNITY_EDITOR
using UnityEditor;
#endif

public class CostAttribute : ItemAttribute
{

    public int cost;

#if UNITY_EDITOR
    public override void DoLayout()
    {
        cost = EditorGUILayout.IntField("Cost", cost);
    }
#endif
}
