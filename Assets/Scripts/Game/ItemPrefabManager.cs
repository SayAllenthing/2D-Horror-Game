using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemPrefabManager : MonoBehaviour 
{
	public static ItemPrefabManager Instance;

	public List<Item> Items = new List<Item>();

	public void Awake()
	{
		if(Instance != null)
		{
			DestroyImmediate(Instance);
		}

		Instance = this;
	}

	public GameObject GetItemPrefab(Item.eItemType item)
	{
		GameObject prefab = null;

		for(int i = 0; i < Items.Count; i++)
		{
			if(Items[i].Type == item)
			{
				prefab = Items[i].gameObject;
				break;
			}
		}

		return prefab;
	}
}
