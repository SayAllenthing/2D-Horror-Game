using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MapObject : NetworkBehaviour {

	public int X = 0;
	public int Y = 0;

	public override void OnStartClient()
	{
		GameMapData.Instance.GetNodeFromXY(X,Y).SetObject(this.gameObject);
	}

	public void SetTile(int _x, int _y)
	{
		X = _x;
		Y = _y;
	}
}
