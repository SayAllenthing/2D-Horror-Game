using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Inventory : MonoBehaviour {

	public Dictionary<string, int> MyInventory = new Dictionary<string, int>();
	int MaxItems = 4;

	public void Init(int _maxItems)
	{
		MaxItems = _maxItems;
	}

	public void AddItem(string item, int amount)
	{
		if(HasItem(item))
			MyInventory[item] += amount;
		else
			MyInventory[item] = amount;

		UpdateInventory();
	}

	void UpdateInventory()
	{
		GameUIManager.Instance.SetInventory(MyInventory, MaxItems);
	}

	public bool HasItem(string item)
	{
		return MyInventory.ContainsKey(item);
	}

	public ItemData GetItem(string s)
	{
		return ItemFactory.Instance.GetItem(s);
	}
}
