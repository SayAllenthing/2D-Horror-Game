#if UNITY_EDITOR
using UnityEditor;
#endif

public class ArmorAttribute : ItemAttribute
{

    public float armor;

#if UNITY_EDITOR
    public override void DoLayout()
    {
        armor = EditorGUILayout.FloatField("Armor", armor);
    }
#endif
}
