using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ItemCreator : EditorWindow 
{
	string ItemName = "New Item";

	Sprite ItemSprite;


	[MenuItem ("Window/Item Creator")]

	public static void SHowWindow() {
		EditorWindow.GetWindow(typeof(ItemCreator));
	}

	void OnGUI()
	{
		GUILayout.Label ("New Item", EditorStyles.boldLabel);

		ItemName = EditorGUILayout.TextField("Item Name", ItemName);

		GUILayout.BeginVertical();
		ItemSprite = EditorGUI.ObjectField(new Rect(10,50,200,200), "Sprite", ItemSprite, typeof(Sprite)) as Sprite;
		GUILayout.Space(210);
		GUILayout.EndVertical();


		if(GUILayout.Button("Create"))
		{
			Item i = ScriptableObjectUtility.CreateAsset<Item>(ItemName) as Item;
			i.Name = ItemName;
		}
	}
}
