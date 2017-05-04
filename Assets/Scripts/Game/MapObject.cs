using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MapObject : NetworkBehaviour {

	[SyncVar]
	public int X = 0;
	[SyncVar]
	public int Y = 0;

	public ItemData Data;

	public void SetData(ItemData _data)
	{
		Data = _data;
		GetComponent<SpriteRenderer>().sprite = Data.Sprite;
	}

	public override void OnStartClient()
	{
		GameMapData.Instance.GetNodeFromXY(X,Y).SetObject(this.gameObject);
	}

    private void OnDestroy()
    {
        GameMapData.Instance.GetNodeFromXY(X, Y).ClearObject();
    }

    public void SetTile(int _x, int _y)
	{
		X = _x;
		Y = _y;
	}
}
