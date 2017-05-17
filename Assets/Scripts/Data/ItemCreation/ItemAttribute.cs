using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ItemAttribute : ScriptableObject
{

    public virtual string GetName()
    {
       return "None";
    }

#if UNITY_EDITOR
    public virtual void DoLayout() { }
#endif

    public bool bIsDirty = false;
	public virtual void OnSpawned(GameObject g) {}
}
