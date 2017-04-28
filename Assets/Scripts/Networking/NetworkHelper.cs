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

	public void SpawnObject(Vector3 pos)
	{
		CmdSpawnObject(pos);
	}

	[Command]
	void CmdSpawnObject(Vector3 pos)
	{		
		if(this.isServer)
		{
			Node n = GameMapData.Instance.GetNodeFromWorldPoint(pos);

			if(n.CanPlaceObject())
			{
				Vector3 nodePos = n.GetObjectPosition();

				GameObject pref = null; //ItemPrefabManager.Instance.GetItemPrefab(Item.eItemType.LAMP);
				GameObject obj = GameObject.Instantiate(pref, nodePos, Quaternion.identity) as GameObject;

				obj.GetComponent<MapObject>().SetTile(n.X, n.Y);

				NetworkServer.Spawn(obj);

				//Cut out RPC Call, let the object spawn with the information
				//RpcSetObject(obj.GetComponent<NetworkIdentity>().netId, n.X, n.Y);
			}
		}
	}

	[ClientRpc]
	void RpcSetObject(NetworkInstanceId id, int x, int y)
	{
		if(!this.isServer)
		{
			GameObject obj = ClientScene.FindLocalObject(id) as GameObject;
			GameMapData.Instance.GetNodeFromXY(x,y).SetObject(obj);
		}
	}
}
