using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemPrefabManager : MonoBehaviour 
{
	public static ItemPrefabManager Instance;

	public List<GameObject> Items = new List<GameObject>();

	public void Awake()
	{
		if(Instance != null)
		{
			DestroyImmediate(Instance);
		}

		Instance = this;
	}

	public GameObject GetItemPrefab()
	{
		GameObject prefab = null;

		for(int i = 0; i < Items.Count; i++)
		{
			
		}

		return prefab;
	}

	public Sprite GetItemSprite()
	{
		Sprite sprite = null;

		for(int i = 0; i < Items.Count; i++)
		{
			//if(Items[i].Type == item)
			{
				//sprite = Items[i].GetComponent<SpriteRenderer>().sprite;
				break;
			}
		}

		return sprite;
	}

	public Sprite GetItemSprite(string item)
	{		
		return GetItemSprite();
	}


}
