#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Ingredient
{
    public Ingredient()
    {
        item = null;
        amount = 0;
    }

    [SerializeField]
    public ItemData item;

    [SerializeField]
    public int amount;
}

public class RecipeAttribute : ItemAttribute
{
    public List<Ingredient> Ingredients = new List<Ingredient>();    

#if UNITY_EDITOR
    public override void DoLayout()
    {
        bIsDirty = false;

        for(int i = 0; i < Ingredients.Count; i++)
        {
            EditorGUILayout.LabelField("Indgredient " + (i+1).ToString());

            Ingredients[i].item = EditorGUILayout.ObjectField("Prefab", Ingredients[i].item, typeof(ItemData)) as ItemData;
            Ingredients[i].amount = EditorGUILayout.IntField("Amount", Ingredients[i].amount);
        }

        if (GUILayout.Button("Add Ingredient"))
        {
            Ingredients.Add(new Ingredient());
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            bIsDirty = true;
        }

        if (GUILayout.Button("Print"))
        {
            Debug.Log(Ingredients.Count);            
        }
    }
#endif

    public override string GetName()
    {
        return "Recipe Attribute";
    }
}
