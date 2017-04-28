using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Inventory : MonoBehaviour {

	public struct InventoryObject
	{
		public string item;
		public Sprite sprite;
		public int amount;

		public InventoryObject(string _item)
		{
			this.item = _item;
			this.amount = -1;
			this.sprite = null;
		}
	}

	public List<InventoryObject> MyInventory = new List<InventoryObject>();

	int MaxItems = 4;

	public void Init(int _maxItems)
	{
		MaxItems = _maxItems;
	}

	public void AddItem(string item, int amount)
	{
		int index;
		InventoryObject obj = GetItem(item, out index);

		if(obj.amount > 0)
		{
			obj.amount += amount;
			MyInventory[index] = obj;
			//MyInventory[item] += amount;
		}
		else
		{
			obj.amount = amount;
			obj.sprite = ItemPrefabManager.Instance.GetItemSprite(item);

			MyInventory.Add(obj);
		}

		//Debug.Log(MyInventory.Count);

		UpdateInventory();
	}

	void UpdateInventory()
	{
		List<InventoryObject> list = new List<InventoryObject>();

		for(int i = 0; i < MyInventory.Count; i++)
		{
			list.Add(MyInventory[i]);
		}

		int empty = MaxItems - MyInventory.Count;
		for(int i = 0; i < empty; i++)
		{
			InventoryObject io = new InventoryObject("Empty");

			list.Add(io);
		}

		GameUIManager.Instance.SetInventory(list);
	}

	public bool HasItem(string item)
	{
		for(int i = 0; i < MyInventory.Count; i++)
		{
			if(MyInventory[i].item == item)
				return true;
		}

		return false;
	}

	public InventoryObject GetItem(string item, out int index)
	{
		index = -1;
		for(int i = 0; i < MyInventory.Count; i++)
		{
			if(MyInventory[i].item == item)
			{
				index = i;
				return MyInventory[i];
			}
		}

		return new InventoryObject(item);
	}
}
