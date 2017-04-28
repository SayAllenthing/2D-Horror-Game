using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ItemAttribute : ScriptableObject
{

#if UNITY_EDITOR
    public virtual void DoLayout() { }
#endif

}
