using UnityEngine;
using System.Collections;

public class ItemData : MonoBehaviour {

	public string ItemName = "";
	public Sprite Sprite;

	public enum eItemType
	{
		NONE = 0,
		USABLE,
		PLACEABLE
	}

	public eItemType Type = eItemType.NONE;
}
