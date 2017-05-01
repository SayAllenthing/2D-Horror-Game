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

	public void SetInventory(Dictionary<string, int> items, int max)
	{
		for(int i = 0; i < InventoryGameObjects.Count; i++)
		{
			DestroyImmediate(InventoryGameObjects[i]);
		}

		InventoryGameObjects.Clear();

		InventoryPanel.sizeDelta = new Vector2(10 + (max*50), 60);

		foreach(KeyValuePair<string, int> i in items)
		{
			if(i.Value > 0)
			{
				Debug.Log("Adding item: " + i.Key);

				GameObject g = GameObject.Instantiate(InventoryItemPrefab, InventoryPanel.transform) as GameObject;
				g.GetComponent<InventoryUIObject>().SetObject(ItemFactory.Instance.GetItem(i.Key), i.Value);
				InventoryGameObjects.Add(g);
			}
		}

		int emptyNum = max - InventoryGameObjects.Count;

		for(int i = 0; i < emptyNum; i++)
		{
			GameObject g = GameObject.Instantiate(InventoryItemPrefab, InventoryPanel.transform) as GameObject;
			g.GetComponent<InventoryUIObject>().SetObject(null, 0);
			InventoryGameObjects.Add(g);
		}
	}

	public void OnItemClicked(string item)
	{
		MyCharacter.inventory.AddItem(item, -1);
		NetworkHelper.Instance.SpawnObject(MyCharacter.transform.position, item);
	}
}
