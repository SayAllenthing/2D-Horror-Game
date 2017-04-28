using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using System.Linq;

[CustomEditor(typeof(Item))]
public class ItemEditor : Editor
{

    // Add a menu item to create Item ScriptableObjects:
    [MenuItem("Assets/Create/Item")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<Item>();
    }

    // Holds ItemAttribute types for popup:
    private string[] m_attributeTypeNames = new string[0];
    private int m_attributeTypeIndex = -1;

    private void OnEnable()
    {
        // Fill the popup list:
        Type[] types = Assembly.GetAssembly(typeof(ItemAttribute)).GetTypes();
        m_attributeTypeNames = (from Type type in types where type.IsSubclassOf(typeof(ItemAttribute)) select type.FullName).ToArray();
    }

    public override void OnInspectorGUI()
    {
        var item = target as Item;

		item.Name = EditorGUILayout.TextField("Name", item.Name);

        // Draw attributes with a delete button below each one:
        int indexToDelete = -1;
        for (int i = 0; i < item.attributes.Count; i++)
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
			if (item.attributes[i] != null) item.attributes[i].DoLayout();
            if (GUILayout.Button("Delete")) indexToDelete = i;
            EditorGUILayout.EndVertical();
        }
        if (indexToDelete > -1) item.attributes.RemoveAt(indexToDelete);

        // Draw a popup and button to add a new attribute:
        EditorGUILayout.BeginHorizontal();
        m_attributeTypeIndex = EditorGUILayout.Popup(m_attributeTypeIndex, m_attributeTypeNames);
        if (GUILayout.Button("Add"))
        {
            // A little tricky because we need to record it in the asset database, too:
            var newAttribute = CreateInstance(m_attributeTypeNames[m_attributeTypeIndex]) as ItemAttribute;
            newAttribute.hideFlags = HideFlags.HideInHierarchy;
            AssetDatabase.AddObjectToAsset(newAttribute, item);
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(newAttribute));
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            item.attributes.Add(newAttribute);
        }
        EditorGUILayout.EndHorizontal();

        if (GUI.changed) EditorUtility.SetDirty(item);
    }

}
