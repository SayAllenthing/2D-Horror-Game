#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

public class PhysicsAttribute : ItemAttribute
{
	public float Radius = 0.25f;

#if UNITY_EDITOR
    public override void DoLayout()
    {
        Radius = EditorGUILayout.FloatField("Radius", Radius);
    }
#endif

	public override void OnSpawned(GameObject g)
	{
        CircleCollider2D col = g.GetComponent<CircleCollider2D>();
        if(col != null)
        {
            col.radius = Radius;
            col.isTrigger = false;
        }
	}
}
