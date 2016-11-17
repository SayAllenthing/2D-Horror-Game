using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NetworkHelper : NetworkBehaviour
{
	public static NetworkHelper Instance;

	public override void OnStartLocalPlayer()
	{
		Debug.Log("Instance Created");

		if(Instance != null)
		{
			Destroy(Instance);
			return;
		}

		Instance = this;
	}

	public void SpawnObject(GameObject g)
	{
		CmdSpawnObject(g, g.transform.position);
	}

	[Command]
	void CmdSpawnObject(GameObject g, Vector3 pos)
	{		
		if(this.isServer)
		{
			GameObject pref = ItemPrefabManager.Instance.GetItemPrefab(Item.eItemType.LAMP);
			GameObject obj = GameObject.Instantiate(pref, pos, Quaternion.identity) as GameObject;
			NetworkServer.Spawn(obj);
		}
	}
}
