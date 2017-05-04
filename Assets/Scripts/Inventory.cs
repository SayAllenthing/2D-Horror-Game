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

	public bool AddItem(string item, int amount)
	{
        if (MyInventory.Count >= MaxItems)
            return false;

		if(HasItem(item))
			MyInventory[item] += amount;
		else
			MyInventory[item] = amount;

		UpdateInventory();

        return true;
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
