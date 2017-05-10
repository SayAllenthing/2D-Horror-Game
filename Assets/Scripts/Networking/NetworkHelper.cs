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

	public void SpawnObject(Vector3 pos, string itemName)
	{
		CmdSpawnObject(pos, itemName);
	}

	[Command]
	void CmdSpawnObject(Vector3 pos, string itemName)
	{		
		if(this.isServer)
		{
			Node n = GameMapData.Instance.GetNodeFromWorldPoint(pos);

			if(n.CanPlaceObject())
			{
				Vector3 nodePos = n.GetObjectPosition();

				GameObject pref = ItemFactory.Instance.MapObjectPrefab;
				GameObject obj = GameObject.Instantiate(pref, nodePos, Quaternion.identity) as GameObject;                

				obj.GetComponent<MapObject>().SetTile(n.X, n.Y);

				NetworkServer.Spawn(obj);

				//Cut out RPC Call, let the object spawn with the information
				RpcSetObject(obj.GetComponent<NetworkIdentity>().netId, itemName);
			}
		}
	}

    public void SpawnDebugObject()
    {
        Vector3 pos = GameObject.Find("Enemy Spawner").transform.position;
        CmdSpawnDebugObject(pos);
    }

    [Command]
    void CmdSpawnDebugObject(Vector3 pos)
    {
        if (this.isServer)
        {                        
            GameObject e = Instantiate(DebugManager.Instance.SpawnObject, pos, Quaternion.identity);            
            NetworkServer.Spawn(e);
        }
    }

    public void DestroyObject(GameObject g)
    {
        //CmdDestroyObject(g.GetComponent<NetworkIdentity>().netId);
        CmdDestroyObject(g);
    }

    [Command]
    void CmdDestroyObject(GameObject g)
    {
        if (this.isServer)
        {
            NetworkServer.Destroy(g);     
        }
        //NetworkServer.FindLocalObject(id)
    }

    [ClientRpc]
	void RpcSetObject(NetworkInstanceId id, string itemName)
	{
        GameObject obj = ClientScene.FindLocalObject(id) as GameObject;
		ItemFactory.Instance.OnLocalObjectCreated(obj, itemName);
	}
}
