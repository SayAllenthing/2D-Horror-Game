using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ItemCreator : EditorWindow 
{
	string ItemName = "New Item";
	ItemData.eItemType ItemType = ItemData.eItemType.NONE;
	Sprite ItemSprite = null;

	[MenuItem ("Window/Item Creator")]

	public static void SHowWindow() {
		EditorWindow.GetWindow(typeof(ItemCreator));
	}

	void OnGUI()
	{
		GUILayout.Label ("New Item", EditorStyles.boldLabel);

		ItemName = EditorGUILayout.TextField("Item Name", ItemName);
		ItemType = (ItemData.eItemType)EditorGUILayout.EnumPopup("Type", ItemType);
		ItemSprite = EditorGUILayout.ObjectField("Sprite", ItemSprite, typeof(Sprite)) as Sprite;


		if(GUILayout.Button("Create"))
		{
			ItemData i = ScriptableObjectUtility.CreateAsset<ItemData>(ItemName) as ItemData;
			i.Name = ItemName;
			i.Type = ItemType;
			i.Sprite = ItemSprite;
		}
	}
}
