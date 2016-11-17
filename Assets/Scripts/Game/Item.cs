using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

	public enum eItemType
	{
		NONE = 0,
		LAMP
	}

	public eItemType Type = eItemType.NONE;
}
