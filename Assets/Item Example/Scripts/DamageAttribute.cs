#if UNITY_EDITOR
using UnityEditor;
#endif

public class DamageAttribute : ItemAttribute
{

    public float damage;

#if UNITY_EDITOR
    public override void DoLayout()
    {
        damage = EditorGUILayout.FloatField("Damage", damage);
    }
#endif
}
