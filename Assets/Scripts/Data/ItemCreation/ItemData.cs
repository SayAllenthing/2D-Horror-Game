using UnityEngine;
using System;
using System.Collections.Generic;

public class ItemData : ScriptableObject {

	public string Name = "New Item";
	public Sprite Sprite = null;
	public eItemType Type = eItemType.NONE;

	public List<ItemAttribute> attributes = new List<ItemAttribute>();

	public enum eItemType
	{
		NONE,
		PLACEABLE,
		CONSUMEABLE,
		WEAPON,
        MATERIAL
	}

	public ItemData Clone(ItemData id)
	{
		ItemData ret = new ItemData();

		ret.Name = id.Name;
		ret.attributes = id.attributes;

		return ret;
	}

    public ItemAttribute GetAttribute<T>()
    {
        foreach(ItemAttribute i in attributes)
        {
            if(i.GetType() == typeof(T))
            {
                Debug.Log("Returning Type " + typeof(T).ToString());
                return i;
            }
        }

        Debug.Log("Returning Bullocks");

        return null;
    }
}
