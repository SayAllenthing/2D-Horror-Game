using UnityEngine;
using System;
using System.Collections.Generic;

public class Item : ScriptableObject
{
	public string Name = "New Item";

    public List<ItemAttribute> attributes = new List<ItemAttribute>();

	public enum eItemType
	{
		PLACEABLE,
		CONSUMEABLE
	}
}
