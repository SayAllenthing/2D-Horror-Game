using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : ScriptableObject {

	public List<ItemData> Items = new List<ItemData>();

	public ItemData GetItem(string i)
	{
		ItemData ret = Items.Find(x => x.Name == i);

		return ret;
	}
}
