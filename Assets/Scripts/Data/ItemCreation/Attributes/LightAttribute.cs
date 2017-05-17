#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

public class LightAttribute : ItemAttribute
{
    public GameObject prefab;
	public float Brightness = 5;
	public int Raycasts = 180;

#if UNITY_EDITOR
    public override void DoLayout()
    {
		prefab = EditorGUILayout.ObjectField("Prefab", prefab, typeof(GameObject)) as GameObject;
		Brightness = EditorGUILayout.FloatField("Brightness", Brightness);
		Raycasts = EditorGUILayout.IntField("Raycasts", Raycasts);
    }
#endif

	public override void OnSpawned(GameObject g)
	{
		GameObject light = GameObject.Instantiate(prefab, g.transform) as GameObject;
		light.transform.localPosition = Vector3.zero;
	}

    public override string GetName()
    {
        return "Light Attribute";
    }
}
