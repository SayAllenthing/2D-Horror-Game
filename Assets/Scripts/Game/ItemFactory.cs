using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemFactory : MonoBehaviour 
{
	public static ItemFactory Instance;

	public GameObject MapObjectPrefab;
	public ItemDatabase Database;

	public void Awake()
	{
		if(Instance != null)
		{
			DestroyImmediate(Instance);
		}

		Instance = this;
	}

	public GameObject SpawnItem(string item)
	{
		ItemData data = Database.GetItem(item);
		GameObject ret = GameObject.Instantiate(MapObjectPrefab, Vector3.zero, Quaternion.identity) as GameObject;        

		ret.GetComponent<MapObject>().SetData(data);

		OnSpawnObject(ret, data);

		return ret;
	}

	public void OnLocalObjectCreated(GameObject g, string item)
	{
		ItemData data = Database.GetItem(item);

        //Debug.Log("RPC? " + g + " " + item + " " + data);

        g.GetComponent<MapObject>().SetData(data);
		OnSpawnObject(g, data);
	}

	void OnSpawnObject(GameObject obj, ItemData data)
	{
		foreach(ItemAttribute ia in data.attributes)
		{
			ia.OnSpawned(obj);
		}
	}

	public ItemData GetItem(string item)
	{
		return Database.GetItem(item);
	}

	public Sprite GetItemSprite(string item)
	{		
		return Database.GetItem(item).Sprite;
	}


}
