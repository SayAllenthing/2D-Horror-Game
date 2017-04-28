using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameUIManager : MonoBehaviour {

	public static GameUIManager Instance;

	public RectTransform InventoryPanel;

	public GameObject InventoryItemPrefab;

	public List<GameObject> InventoryGameObjects = new List<GameObject>();

	public Character MyCharacter;

	void Awake()
	{
		if(Instance != null)
		{
			Destroy(Instance);
		}

		Instance = this;
	}

	public void RegisterLocalPlayer(Character c)
	{
		MyCharacter = c;
	}

	public void SetInventory(List<Inventory.InventoryObject> items)
	{
		for(int i = 0; i < InventoryGameObjects.Count; i++)
		{
			DestroyImmediate(InventoryGameObjects[i]);
		}

		InventoryGameObjects.Clear();

		InventoryPanel.sizeDelta = new Vector2(10 + (items.Count*50), 60);

		for(int i = 0; i < items.Count; i++)
		{
			GameObject g = GameObject.Instantiate(InventoryItemPrefab, InventoryPanel.transform) as GameObject;
			g.GetComponent<InventoryUIObject>().SetObject(items[i]);
			InventoryGameObjects.Add(g);
		}
	}

	public void OnItemClicked(string item)
	{
		
	}
}
